import 'package:flutter/material.dart';
import 'package:squash_app/resources/theme/theme.dart';

class Description extends StatelessWidget {
  const Description({super.key});

  @override
  Widget build(BuildContext context) {
    return const Padding(
      padding: EdgeInsets.only(
        top: 8,
        left: 8,
        right: 8,
      ),
      child: Text(
        'Squash is a free, open-source platform designed for effortless creation, management, and sharing of short links. Its intuitive, secure, and built for speed.',
        style: TextStyle(
          color: AppTheme.lightGreyColor,
          fontFamily: 'MonaSans',
          height: 1.5,
        ),
        textAlign: TextAlign.center,
      ),
    );
  }
}
