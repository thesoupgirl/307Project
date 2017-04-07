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
            password: '',
            push: false,
            dist: 5
        }
        this.display = this.display.bind(this);
        this.handler = this.handler.bind(this);
        //this.callbackParent = this.callbackParent.bind(this);      
    }

    handler(user, status) {
      this.setState({username: user.username});
      this.setState({password: user.password});
      this.setState({dist: user.dist});
      this.setState({push: user.push});
      this.setState({isLoggedIn: status});
      this.display()
      //console.warn(this.state.username)

    }


  display () {
    if (this.state.isLoggedIn) {
      return (<Nav handler = {this.handler}
                  user = {this.state}/>)

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
