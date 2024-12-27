import 'package:flutter/material.dart';
import 'package:squash_app/resources/theme/app_theme.dart';

class LogoutCardTitle extends StatelessWidget {
  const LogoutCardTitle({super.key});

  @override
  Widget build(BuildContext context) {
    return const Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      mainAxisAlignment: MainAxisAlignment.center,
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
          'You can login again whenever you want.',
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
