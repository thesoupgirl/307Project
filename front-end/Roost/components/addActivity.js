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

/*  
    
*/


export default class AddActivity extends Component {
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
                <Title>Add Activity</Title>
            </Body>
            <Right/>
        </Header>
          <Text>Add</Text>
      </Container>
    );
  }
}


