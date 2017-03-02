import Nav from './nav.js';
import React, { Component } from 'react';
//var styles = require('./styles.js');
/*
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1} from 'native-base'; 
*/
import {
  AppRegistry,
  StyleSheet,
  Text,
  View,
  Image,
  Dimensions,
  TextInput,
  Button,
  TouchableOpacity,
  Alert,
  
} from 'react-native';
import { Container} from 'native-base';

/*  
  LOGIN: SOPHIA
  Build a log in screen, import any extra components you my need from native base.
  Check out the native base documentation as well for guidance.
*/
const { width, height } = Dimensions.get("window");

const background = require("./img/header.jpg");
const mark = require("./img/roost2.png");
const lockIcon = require("./img/lock.png");
const personIcon = require("./img/person.png");

export default class Login extends Component {

  constructor({handler}) {
        super()
        this.state = {
          users : [
            {username:'Dpiotti',pass:'123'},
            {username:'roost',pass:'123'}
        ],
        login: false,
        username: '',
        password: '',
        isLoggedIn: false,
        page: 'login'
        }
     this.loginPressed = this.loginPressed.bind(this)
     this.createAccountPressed = this.createAccountPressed.bind(this)
    this.renderPage = this.renderPage.bind(this)
    }

    loginPressed() {
        var i;
        //call to authenticate
        for (i = 0; i < this.state.users.length; i++) {
          if (this.state.users[i].username === this.state.username &&
              this.state.users[i].pass === this.state.password) {
                    this.props.handler(this.state.username, this.state.password, true)
                    return;
              }
        }
              Alert.alert(
            'Login Failed',
          )
      
            
        }

    createAccountPressed() {
      var user = {username: this.state.username , pass: this.state.password}
      this.setState({ 
        users: [this.state.users.concat(user)]
        
      })
      if (this.state.username == '' ||
          this.state.password == '') {
              Alert.alert('Your username or password is empty')
            return;    
          }
      else if (this.state.password.length < 6) {
        Alert.alert('password must be greater than 6 characters')
        return;
      }
      this.props.handler(true)
      //call to add user
    }


  renderPage () {
    if (this.state.page ==='login') {
      return (
       <View style={styles.container}>
        <Image source={background} style={styles.background} resizeMode="cover">
          <View style={styles.markWrap}>
            <Image source={mark} style={styles.mark} resizeMode="contain" />
          </View>
          <View style={styles.wrapper}>
            <View style={styles.inputWrap}>
              <View style={styles.iconWrap}>
                <Image source={personIcon} style={styles.icon} resizeMode="contain" />
              </View>
              <TextInput
                placeholderTextColor="#FFF"
                placeholder="Username" 
                style={styles.input}
                value={this.state.username}
                onChangeText={username => this.setState({username})}
          
              />
            </View>
            <View style={styles.inputWrap}>
              <View style={styles.iconWrap}>
                <Image source={lockIcon} style={styles.icon} resizeMode="contain" />
              </View>
              <TextInput 
                placeholderTextColor="#FFF"
                placeholder="Password" 
                style={styles.input} 
                secureTextEntry 
                value={this.state.password}
                onChangeText={password => this.setState({password})}
              />
            </View>
            <TouchableOpacity onPress={this.loginPressed} activeOpacity={.5}>
              <View style={styles.button}>
                <Text style={styles.buttonText}>Sign In</Text>
              </View>
            </TouchableOpacity>
          </View>
          <View style={styles.container}>
            <View style={styles.signupWrap}>
              <Text style={styles.accountText}>Do not have an account?</Text>
              <TouchableOpacity activeOpacity={.5} onPress={() => {this.setState({page: 'create'});
                this.setState({username: '', password:''})}}>
                <View>
                  <Text style={styles.signupLinkText}>Sign Up</Text>
                </View>
              </TouchableOpacity>
            </View>
          </View>
        </Image>
      </View>
    ); 
  }
  else if (this.state.page === 'create') {
    return (<View style={styles.container}>
        <Image source={background} style={styles.background} resizeMode="cover">
          <View style={styles.markWrap}>
            <Image source={mark} style={styles.mark} resizeMode="contain" />
          </View>
          <View style={styles.wrapper}>
            <View style={styles.inputWrap}>
              <View style={styles.iconWrap}>
                <Image source={personIcon} style={styles.icon} resizeMode="contain" />
              </View>
              <TextInput
                placeholderTextColor="#FFF"
                placeholder="Username" 
                style={styles.input}
                value={this.state.username}
                onChangeText={username => this.setState({username})}
          
              />
            </View>
            <View style={styles.inputWrap}>
              <View style={styles.iconWrap}>
                <Image source={lockIcon} style={styles.icon} resizeMode="contain" />
              </View>
              <TextInput 
                placeholderTextColor="#FFF"
                placeholder="Password" 
                style={styles.input} 
                secureTextEntry 
                value={this.state.password}
                onChangeText={password => this.setState({password})}
              />
            </View>
            <TouchableOpacity onPress={this.createAccountPressed} activeOpacity={.5}>
              <View style={styles.button}>
                <Text style={styles.buttonText}>Sign Up!</Text>
              </View>
            </TouchableOpacity>
          </View>
          <View style={styles.container}>
           <View style={styles.signupWrap}>
              <Text style={styles.accountText}></Text>
              <TouchableOpacity activeOpacity={.5} onPress={() => {this.setState({page: 'login'});
                this.setState({username: '', password:''})}}>
                <View>
                  <Text style={styles.signupLinkText}>Login with account</Text>
                </View>
              </TouchableOpacity>
            </View>
          </View>
        </Image>
      </View>)
  }

  }

  render() {
    return (
      <Container>
        {this.renderPage()}
      </Container>
    )
  }}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  markWrap: {
    flex: 1,
    paddingVertical: 30,
  },
  mark: {
    width: null,
    height: null,
    flex: 1,
  },
  background: {
    width,
    height,
  },
  wrapper: {
    paddingVertical: 30,
  },
  inputWrap: {
    flexDirection: "row",
    marginVertical: 10,
    height: 40,
    borderBottomWidth: 1,
    borderBottomColor: "#CCC"
  },
  iconWrap: {
    paddingHorizontal: 7,
    alignItems: "center",
    justifyContent: "center",
  },
  icon: {
    height: 20,
    width: 20,
  },
  input: {
    flex: 1,
    paddingHorizontal: 10,
  },
  button: {
    backgroundColor: "#FF3366",
    paddingVertical: 20,
    alignItems: "center",
    justifyContent: "center",
    marginTop: 30,
  },
  buttonText: {
    color: "#FFF",
    fontSize: 18,
  },
  forgotPasswordText: {
    color: "#D8D8D8",
    backgroundColor: "transparent",
    textAlign: "right",
    paddingRight: 15,
  },
  signupWrap: {
    backgroundColor: "transparent",
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
  },
  accountText: {
    color: "#D8D8D8"
  },
  signupLinkText: {
    color: "#FFF",
    marginLeft: 5,
  }
});
