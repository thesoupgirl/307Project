import Nav from './nav.js';
import React, { Component } from 'react';
import Login from './login.js'
var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
  Text,
  View
} from 'react-native';
//import Login from './login.js';

/*  
  The launch compoent determines if the user is logged in.
  If they are logged in it will direct them to the default page,
  if not, they will be brought to the log in screen.
*/

export default class Launch extends Component {

  constructor(props) {
        super(props)
        this.state = {
            isLoggedIn: false,
            username: '',
            password: ''
        }
        this.display = this.display.bind(this);
        this.handler = this.handler.bind(this);
        //this.callbackParent = this.callbackParent.bind(this);      
    }

    handler(user, pass, status) {
      this.setState({isLoggedIn: status});
      this.setState({username: user});
      this.setState({password: pass});
      consile.warn(user)
      this.display()

    }

    handler(status) {
      this.setState({isLoggedIn: status});
      //call to add user
    
    }

  display () {
    if (this.state.isLoggedIn) {
      return (<Nav handler = {this.handler}
                username ={this.state.username}
                password = {this.state.password}/>)
    }
    return (<Login handler = {this.handler}/>)
  }
  
  render() {
    return (
      <View style={styles.container}>
        {this.display()}
      </View>
    );
  }
}