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


export default class SwipeActivities extends Component {
  constructor(props) {
        super(props)
        this.state = {
            
        }
       
    }


  render() {
    return (
      <Container>
        <H1>Swipe Activites</H1>
      </Container>
    );
  }
}




