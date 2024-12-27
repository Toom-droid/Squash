import 'package:flutter/material.dart';
import 'package:squash_app/resources/routes/app_routes.dart';

void main() => runApp(const MyApp());

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Squash App',
      initialRoute: AppRoutes.logout,
      routes: AppRoutes.getRoutes(),
    );
  }
}
