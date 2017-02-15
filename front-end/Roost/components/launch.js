import Nav from './nav.js';
import React, { Component } from 'react';
var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
  Text,
  View
} from 'react-native';

/*  
  The launch compoent determines if the user is logged in.
  If they are logged in it will direct them to the default page,
  if not, they will be brought to the log in screen.
*/

export default class Launch extends Component {

  constructor(props) {
        super(props)
        this.state = {
            isLoggedIn: true
        }
        this.display = this.display.bind(this);        
    }

  display () {
    if (this.state.isLoggedIn) {
      return (<Nav/>)
    }
    return (<Text style={styles.welcome}>Not Logged in</Text>)
  }
  
  render() {
    return (
      <View style={styles.container}>
        {this.display()}
      </View>
    );
  }
}