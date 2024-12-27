import 'package:flutter/material.dart';
import 'package:squash_app/resources/theme/theme.dart';
import 'package:squash_app/view/login/widgets/widgets.dart';
import 'package:squash_app/widgets/widgets.dart';

class LoginBody extends StatelessWidget {
  const LoginBody({super.key});

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      appBar: AppBarSquash(),
      backgroundColor: AppTheme.darkGreyColor,
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.center,
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          LoginCard(
            child: Padding(
              padding: EdgeInsets.symmetric(horizontal: 20),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.center,
                mainAxisAlignment: MainAxisAlignment.start,
                children: [
                  Spacer(),
                  LoginCardTitle(),
                  Spacer(),
                  ProviderBox(
                    title: 'Google',
                    icon: 'assets/images/google_icon.svg',
                  ),
                  ProviderBox(
                    icon: 'assets/images/github_icon.svg',
                    title: 'Github',
                  ),
                  Spacer(),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
