import 'package:flutter/material.dart';
import 'package:squash_app/resources/theme/theme.dart';

class LogoutButton extends StatelessWidget {
  const LogoutButton({super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(top: 8),
      child: Container(
        margin: const EdgeInsets.symmetric(horizontal: 10),
        height: 45,
        width: double.infinity,
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(5),
          color: AppTheme.blueColor,
        ),
        child: const Row(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(
              Icons.logout,
              color: AppTheme.veryLightGreyColor,
            ),
            Padding(
              padding: EdgeInsets.only(left: 8),
              child: Text(
                'Log Out',
                style: TextStyle(
                    fontSize: 14,
                    color: AppTheme.veryLightGreyColor,
                    fontFamily: 'MonaSans'),
              ),
            )
          ],
        ),
      ),
    );
  }
}
