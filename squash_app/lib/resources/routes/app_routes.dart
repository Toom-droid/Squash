import 'package:flutter/material.dart';
import 'package:squash_app/view/view.dart';

class AppRoutes {
  static const String home = 'home';
  static const String login = 'login';
  static const String logout = 'logout';

  static Map<String, WidgetBuilder> getRoutes() {
    return {
      home: (context) => const HomePage(),
      login: (context) => const LoginPage(),
      logout: (context) => const LogoutPage(),
    };
  }
}
