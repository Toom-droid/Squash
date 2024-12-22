using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Squash.WebAPI.Interfaces.Services;
using Squash.WebAPI.Models.DTOs.Url;
using Squash.WebAPI.Models;
using Microsoft.SqlServer.Server;
using Squash.WebAPI.Services;
using Google.Apis.Auth;

namespace Squash.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController(IUrlService UrlService, IMapper mapper) : ControllerBase
    {
        private readonly IUrlService _urlService = UrlService;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UrlReadDTO>>> GetAllAsync()
        {
            try
            {
                var urls = await _urlService.GetAllAsync();
                var urlsDTO = _mapper.Map<IEnumerable<UrlReadDTO>>(urls);
                return Ok(urlsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UrlReadDTO>> GetByIdAsync(int id)
        {
            try
            {
                var url = await _urlService.GetByIdAsync(id);
                var urlDTO = _mapper.Map<UrlReadDTO>(url);
                return Ok(urlDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("userId/{userId}")]
        public async Task<ActionResult<IEnumerable<UrlReadDTO>>> GetByUserIdAsync(int userId)
        {
            try
            {
                var urls = await _urlService.GetUrlsByUserIdAsync(userId);
                var urlsDTO = _mapper.Map<IEnumerable<UrlReadDTO>>(urls);
                return Ok(urlsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UrlCreateDTO urlDto)
        {
            var url = _mapper.Map<Url>(urlDto);
            if (!await _urlService.CreateAsync(url)) return NotFound();
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UrlUpdateDTO urlDto)
        {
            try
            {
                var url = _mapper.Map<Url>(urlDto);
                if (!await _urlService.UpdateAsync(url)) return NotFound();
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
                if (!await _urlService.DeleteAsync(id)) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
