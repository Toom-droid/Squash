using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Squash.WebAPI.Interfaces.Services;
using Squash.WebAPI.Models.DTOs.User;
using Squash.WebAPI.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using JWT.Builder;
using System.Net.Http.Headers;
using Azure.Core;

namespace Squash.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService UserService, IMapper mapper, IConfiguration configuration) : ControllerBase
    {
        private readonly IUserService _userService = UserService;
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _configuration = configuration;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllAsync()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                var usersDTO = _mapper.Map<IEnumerable<UserReadDTO>>(users);
                return Ok(usersDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetByIdAsync(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                var userDTO = _mapper.Map<UserReadDTO>(user);
                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserCreateDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Guid = Guid.NewGuid();
            if (!await _userService.CreateAsync(user)) return NotFound();
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateDTO userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                if (!await _userService.UpdateAsync(user)) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                if (!await _userService.DeleteAsync(id)) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Google Auth
        [HttpGet("auth/google")]
        public IActionResult RedirectToGoogle()
        {
            var googleAuthUrl = "https://accounts.google.com/o/oauth2/v2/auth";
            var clientId = "164650067185-7p2v78at0bl9stg6qa43jdla67egnr4h.apps.googleusercontent.com";
            var redirectUri = Uri.EscapeDataString("http://localhost:5163/api/user/auth/google-callback");
            var scope = "openid email profile";
            var state = Guid.NewGuid().ToString();

            var url = $"{googleAuthUrl}?client_id={clientId}&redirect_uri={redirectUri}&response_type=code&scope={scope}&state={state}";


            Response.Cookies.Append("OAuthState", state);

            return Redirect(url);
        }

        [HttpGet("auth/google-callback")]
        public async Task<IActionResult> GoogleCallback([FromQuery] string code, [FromQuery] string state)
        {
            var savedState = Request.Cookies["OAuthState"];
            if (state != savedState)
            {
                return BadRequest("Estado inválido.");
            }

            var tokenUrl = "https://oauth2.googleapis.com/token";
            var clientId = "164650067185-7p2v78at0bl9stg6qa43jdla67egnr4h.apps.googleusercontent.com";
            var clientSecret = "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";
            var redirectUri = "http://localhost:5163/api/user/auth/google-callback";

            var tokenRequest = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "redirect_uri", redirectUri },
                { "grant_type", "authorization_code" }
            };

            using var httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
            var response = await httpClient.PostAsync(tokenUrl, new FormUrlEncodedContent(tokenRequest)).ConfigureAwait(false);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Error al obtener el token: {responseContent}");
            }

            var tokenResponse = JsonConvert.DeserializeObject<GoogleTokenResponse>(responseContent);

            var userInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
            var userInfoResponse = await httpClient.GetAsync(userInfoUrl);
            var userInfoContent = await userInfoResponse.Content.ReadAsStringAsync();

            if (!userInfoResponse.IsSuccessStatusCode)
            {
                return BadRequest($"Error al obtener la información del usuario: {userInfoContent}");
            }

            var userInfo = JsonConvert.DeserializeObject<GoogleUserInfo>(userInfoContent);

            var userData = new UserCreateDTO
            {
                Email = userInfo.Email,
                Name = userInfo.Name,
                AuthMethod = "google",
                AuthMethodId = userInfo.Id,
            };

            var user = _mapper.Map<User>(userData);

            var userCreated = await _userService.GetOrCreateUserAsync(user);

            var token = GenerateJwtToken(userCreated);

            return Redirect($"https://localhost:4200/auth/callback?token={token}");
        }

        [HttpGet("jwt")]
        public async Task<ActionResult<UserReadDTO>> GetByJWTAsync()
        {
            try
            {
                // Obtener el token JWT de la cabecera de autorización
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized("Token no proporcionado.");
                }

                var userFromToken = GetUserFromToken(token);

                if (userFromToken == null)
                {
                    return Unauthorized("Token inválido.");
                }

                var userData = _mapper.Map<User>(userFromToken);
                var user = await _userService.GetOrCreateUserAsync(userData);

                if (user == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                var userDTO = _mapper.Map<UserReadDTO>(user);

                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public class GoogleTokenResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("id_token")]
            public string IdToken { get; set; }
        }

        public class GoogleUserInfo
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("authMethod", user.AuthMethod),
                new Claim("authMethodId", user.AuthMethodId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static UserCreateDTO GetUserFromToken(string token)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();

                var jsonToken = jwtHandler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken == null)
                {
                    return null;
                }

                var authMethodId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "authMethodId")?.Value;
                var authMethod = jsonToken?.Claims.FirstOrDefault(c => c.Type == "authMethod")?.Value;
                var name = jsonToken?.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                var email = jsonToken?.Claims.FirstOrDefault(c => c.Type == "email")?.Value;



                if (string.IsNullOrEmpty(authMethodId))
                {
                    return null;
                }

                return new UserCreateDTO
                {
                    AuthMethodId = authMethodId,
                    AuthMethod = authMethod,
                    Name = name,
                    Email = email
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Github Auth
        [HttpGet("auth/github")]
        public IActionResult RedirectToGitHub()
        {
            var clientId = _configuration["GitHub:ClientId"];
            var redirectUri = Uri.EscapeDataString(_configuration["GitHub:RedirectUri"]);
            var state = Guid.NewGuid().ToString();

            var url = $"https://github.com/login/oauth/authorize?client_id={clientId}&redirect_uri={redirectUri}&scope=user:email&state={state}";

            Response.Cookies.Append("OAuthState", state);

            return Redirect(url);
        }

        [HttpGet("auth/github-callback")]
        public async Task<IActionResult> GitHubCallback([FromQuery] string code, [FromQuery] string state)
        {
            var savedState = Request.Cookies["OAuthState"];
            if (state != savedState)
            {
                return BadRequest("Estado inválido.");
            }

            var tokenUrl = "https://github.com/login/oauth/access_token";
            var clientId = _configuration["GitHub:ClientId"];
            var clientSecret = _configuration["GitHub:ClientSecret"];
            var redirectUri = _configuration["GitHub:RedirectUri"];

            var tokenRequest = new Dictionary<string, string>
        {
            { "code", code },
            { "client_id", clientId },
            { "client_secret", clientSecret },
            { "redirect_uri", redirectUri }
        };

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var tokenResponse = await httpClient.PostAsync(tokenUrl, new FormUrlEncodedContent(tokenRequest));

            if (!tokenResponse.IsSuccessStatusCode)
            {
                var errorContent = await tokenResponse.Content.ReadAsStringAsync();
                return BadRequest($"Error al obtener el token: {errorContent}");
            }

            var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
            var tokenResult = JsonConvert.DeserializeObject<GitHubTokenResponse>(tokenContent);

            // Obtener la información del usuario
            var userInfoUrl = "https://api.github.com/user";
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResult.AccessToken);
            httpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("App", "1.0"));

            var userInfoResponse = await httpClient.GetAsync(userInfoUrl);
            if (!userInfoResponse.IsSuccessStatusCode)
            {
                var errorContent = await userInfoResponse.Content.ReadAsStringAsync();
                return BadRequest($"Error al obtener la información del usuario: {errorContent}");
            }

            var userInfoContent = await userInfoResponse.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<GitHubUserInfo>(userInfoContent);
            var emails = await GetGithubUserEmailsAsync(tokenResult.AccessToken);
            var primaryEmail = emails?.FirstOrDefault(e => e.Primary && e.Verified)?.Email;


            // Crear o recuperar usuario
            var userData = new UserCreateDTO
            {
                Email = primaryEmail,
                Name = userInfo.Name ?? userInfo.Login,
                AuthMethod = "github",
                AuthMethodId = userInfo.Id.ToString()
            };

            var user = _mapper.Map<User>(userData);
            var userCreated = await _userService.GetOrCreateUserAsync(user);

            // Generar token JWT
            var token = GenerateJwtToken(userCreated);

            return Redirect($"https://localhost:4200/auth/callback?token={token}");
        }

        public class GitHubTokenResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }
        }

        private static async Task<List<GithubEmail>> GetGithubUserEmailsAsync(string accessToken)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Add("User-Agent", "Squash");
            var response = await client.GetAsync("https://api.github.com/user/emails");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GithubEmail>>(content);
        }
        public class GitHubUserInfo
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("login")]
            public string Login { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
        public class GithubEmail
        {
            public string Email { get; set; }
            public bool Primary { get; set; }
            public bool Verified { get; set; }
        }
    }
}
