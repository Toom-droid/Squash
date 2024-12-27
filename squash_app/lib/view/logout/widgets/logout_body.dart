import 'package:flutter/material.dart';
import 'package:squash_app/resources/theme/theme.dart';
import 'package:squash_app/view/logout/widgets/widgets.dart';
import 'package:squash_app/widgets/appbar_squash.dart';

class LogoutBody extends StatelessWidget {
  const LogoutBody({super.key});

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      appBar: AppBarSquash(),
      backgroundColor: AppTheme.darkGreyColor,
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.center,
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          LogoutCard(
            child: Padding(
              padding: EdgeInsets.symmetric(horizontal: 20),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.center,
                mainAxisAlignment: MainAxisAlignment.start,
                children: [
                  Spacer(),
                  LogoutCardTitle(),
                  Spacer(),
                  LogoutButton(),
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
