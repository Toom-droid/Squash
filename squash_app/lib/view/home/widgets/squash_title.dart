import 'package:flutter/material.dart';

class SquashTitle extends StatelessWidget {
  const SquashTitle({super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 30, top: 20),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Image.asset(
            'assets/images/Squash.png',
            scale: 1,
          ),
          const Text(
            ' your Links',
            style: TextStyle(
                color: Colors.white,
                fontSize: 40,
                fontWeight: FontWeight.bold,
                fontFamily: 'MonaSans',
                wordSpacing: 10),
          ),
        ],
      ),
    );
  }
}
