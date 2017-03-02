import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1} from 'native-base';
var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
  Navigator,
  Image
} from 'react-native';
import Login from './login.js'
import Launch from './launch.js'


/*  
    
*/


export default class Profile extends Component {
  constructor({handler}) {
        super()
        this.state = {
            logout: false
        }
       //this.logoff = this.logoff.bind(this);
       this.disp = this.disp.bind(this);
  }
    disp() {
      if (this.state.logout) {
        return (<Launch/>)
      }
      else {
        return (
        <Container>
        <Header>
            <Left>
            </Left>
            <Body>
                <Title>Roost</Title>
            </Body>
            <Right/>
        </Header>
          <Text>Profile</Text>
          <Button block style={styles.center} onPress={() => this.setState({logout: true}, this.props.hideNav())}><Text>Logout</Text></Button>
      </Container>
      )}
    }


  render() {
    return (
      <View style={styles.container}>
        {this.disp()}
      </View>
    );
  }
}


const styles = {
    title: {
      color: 'red',
    },
    center: {
      justifyContent: 'center',
      alignItems: 'center',
    }
};