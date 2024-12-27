import 'package:flutter/material.dart';
import 'package:squash_app/resources/theme/app_theme.dart';

class AppBarSquash extends StatelessWidget implements PreferredSizeWidget {
  const AppBarSquash({super.key});

  @override
  Widget build(BuildContext context) {
    return AppBar(
      backgroundColor: AppTheme.blackColor,
      centerTitle: true,
      title: const AppbarTitle(),
      leading: null,
      automaticallyImplyLeading: false,
    );
  }

  @override
  Size get preferredSize => const Size.fromHeight(kToolbarHeight);
}

class AppbarTitle extends StatelessWidget {
  const AppbarTitle({super.key});

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        Image.asset(
          'assets/images/logo.png',
          scale: 12,
        ),
        const Text(
          'Squash',
          style: TextStyle(
              color: AppTheme.blueColor,
              fontWeight: FontWeight.bold,
              fontFamily: 'MonaSans',
              fontSize: 30),
        )
      ],
    );
  }
}
