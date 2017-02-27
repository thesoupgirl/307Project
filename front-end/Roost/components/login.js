import Nav from './nav.js';
import React, { Component } from 'react';
import GoogleSignIn from 'react-native-google-sign-in'
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
  TouchableOpacity
} from 'react-native';

/*  
    LOGIN: SOPHIA
    Build a log in screen, import any extra components you my need from native base.
    Check out the native base documentation as well for guidance.
    */
const { width, height } = Dimensions.get("window");

const background = require("./img/purple.jpg");
const mark = require("./img/roost2.png");
const lockIcon = require("./img/lock.png");
const personIcon = require("./img/person.png");

export default class Login extends Component {

  constructor(props) {
    super(props)
      this.state = {
      }

  }

  render() {
    return (
        <View style={styles.container}>
        <TouchableHighlight onPress={async () => {
          await GoogleSignIn.configure({
            clientID: '705785466919-1sa8lnd53dbass778r5oa3hgn75ocnai.apps.googleusercontent.com',
            scopes: ['openid', 'email', 'profile'],
            shouldFetchBasicProfile: true,
          });

          const user = await GoogleSignIn.signInPromise();
          setTimeout(() => {
            alert(JSON.stringify(user, null, '  '));
          }, 1500);
        }}>
    <Text style={styles.instructions}>
      Google Sign-In
      </Text>
      </TouchableHighlight>
      </View>
      );
  }

  /*
     render() {
     return (
     <View style={styles.container}>
     <H1> Log In</H1>
     </View>
     );

     }
     }

/*
const styles = StyleSheet.create({
container: {
flex: 1,
backgroundColor: '#7B68EE'
}
});
*/
/*
   render() {
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
   placeholder="Username" 
   placeholderTextColor="#FFF"
   style={styles.input} 
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
   />
   </View>
   <TouchableOpacity activeOpacity={.5}>
   <View>
   <Text style={styles.forgotPasswordText}>Forgot Password?</Text>
   </View>
   </TouchableOpacity>
   <TouchableOpacity activeOpacity={.5}>
   <View style={styles.button}>
   <Text style={styles.buttonText}>Sign In</Text>
   </View>
   </TouchableOpacity>
   </View>
   <View style={styles.container}>
   <View style={styles.signupWrap}>
   <Text style={styles.accountText}>Do not have an account?</Text>
   <TouchableOpacity activeOpacity={.5}>
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
   }

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

*/
