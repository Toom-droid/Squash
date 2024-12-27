import 'package:flutter/material.dart';
import 'package:squash_app/resources/theme/theme.dart';

class LogoutCard extends StatelessWidget {
  final Widget? child;
  const LogoutCard({super.key, this.child});

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.symmetric(horizontal: 20),
      height: 250,
      width: double.infinity,
      decoration: BoxDecoration(
        color: AppTheme.darkBlueColor,
        borderRadius: BorderRadius.circular(10),
      ),
      child: child,
    );
  }
}
