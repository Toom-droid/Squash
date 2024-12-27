import 'package:flutter/material.dart';
import 'package:squash_app/resources/theme/app_theme.dart';

class LoginCardTitle extends StatelessWidget {
  const LoginCardTitle({super.key});

  @override
  Widget build(BuildContext context) {
    return const Column(
      mainAxisAlignment: MainAxisAlignment.center,
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        Padding(
          padding: EdgeInsets.only(bottom: 10),
          child: Text(
            'Squash',
            style: TextStyle(
              fontSize: 30,
              color: AppTheme.veryLightGreyColor,
              fontWeight: FontWeight.bold,
              fontFamily: 'MonaSans',
            ),
          ),
        ),
        Text(
          'Chose your favourite provider to get started:',
          style: TextStyle(
            fontSize: 14,
            color: AppTheme.lightGreyColor,
            fontFamily: 'MonaSans',
          ),
          textAlign: TextAlign.center,
        ),
      ],
    );
  }
}
