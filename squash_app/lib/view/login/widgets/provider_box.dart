import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:squash_app/resources/theme/app_theme.dart';

class ProviderBox extends StatelessWidget {
  final String title;
  final String icon;
  const ProviderBox({
    super.key,
    required this.title,
    required this.icon,
  });

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
        child: Row(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            SvgPicture.asset(
              icon,
              height: 24,
              width: 24,
            ),
            Padding(
              padding: const EdgeInsets.only(left: 4),
              child: Text(
                'Continue with $title',
                style: const TextStyle(
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
