import 'package:flutter/material.dart';
import 'package:squash_app/resources/theme/theme.dart';

class Dashboard extends StatelessWidget {
  const Dashboard({super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 70),
      child: Container(
        height: 40,
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(5),
          color: AppTheme.blueColor,
        ),
        child: const Row(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Spacer(),
            Icon(
              Icons.attachment,
              color: AppTheme.veryLightGreyColor,
              size: 32,
            ),
            Spacer(),
            Text(
              'DashBoard',
              style: TextStyle(
                  fontFamily: 'MonaSans',
                  fontSize: 20,
                  color: AppTheme.veryLightGreyColor),
            ),
            Spacer(),
          ],
        ),
      ),
    );
  }
}
