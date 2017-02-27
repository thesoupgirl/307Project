import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1} from 'native-base';
var styles = require('./styles');
import {
  AppRegistry,
  StyleSheet,
  Navigator,
  Image,
  Alert,
  TouchableHighlight
} from 'react-native';

/*  
    
*/


export default class Profile extends Component {
  constructor(props) {
        super(props)
        this.state = {
            
        }
       
    }

  render() {
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
          <Button style={styles.wrapper}
          onPress={() => Alert.alert(
            'Bad credentials',
          )}/>
      </Container>
    );
  }
}


