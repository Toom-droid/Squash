import 'package:flutter/material.dart';
import 'package:squash_app/resources/theme/app_theme.dart';
import 'package:squash_app/view/home/widgets/widgets.dart';

class HomeBody extends StatelessWidget {
  const HomeBody({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // appBar: AppBar(
      //   backgroundColor: AppTheme.blackColor,
      //   centerTitle: true,
      //   title: const AppbarTitle(),
      // ),
      backgroundColor: AppTheme.darkGreyColor,
      body: Padding(
        padding:
            const EdgeInsets.only(top: 70, bottom: 20, left: 20, right: 20),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          mainAxisAlignment: MainAxisAlignment.start,
          children: [
            const Spacer(),
            const SquashTitle(),
            const Description(),
            const Spacer(),
            const Dashboard(),
            const Spacer(),
            GetStarted(
              onTap: () {
                Navigator.pushNamed(context, 'login');
              },
            ),
            const Spacer(),
          ],
        ),
      ),
    );
  }
}

class GetStarted extends StatelessWidget {
  final Function()? onTap;
  const GetStarted({super.key, this.onTap});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(
        horizontal: 70,
      ),
      child: GestureDetector(
        onTap: onTap,
        child: Container(
          height: 40,
          decoration: BoxDecoration(
            borderRadius: BorderRadius.circular(5),
            border: Border.all(color: AppTheme.blueColor),
            // color: AppTheme.blueColor,
          ),
          child: const Row(
            crossAxisAlignment: CrossAxisAlignment.center,
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Spacer(),
              Text(
                'Get Started',
                style: TextStyle(
                    fontFamily: 'MonaSans',
                    fontSize: 20,
                    color: AppTheme.veryLightGreyColor),
              ),
              Spacer(),
              Icon(
                Icons.arrow_forward_ios,
                color: AppTheme.veryLightGreyColor,
                size: 23,
              ),
              Spacer(),
            ],
          ),
        ),
      ),
    );
  }
}
