/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 * @flow
 */
import Launch from './components/launch.js'
import Login from './components/login.js'
import Signup from './components/signup.js'
import Contacts from './components/Contacts.js'
import React, { Component } from 'react';
import {
  AppRegistry,
  StyleSheet,
  Text,
  View
} from 'react-native';


AppRegistry.registerComponent('App', () => Launch);
