import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, List, ListItem, Segment} from 'native-base';
var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
  Navigator,
  Image,
  TouchableHighlight,
  Dimensions,
  Alert
} from 'react-native';
import path from '../properties.js'

/*  
    DAVID
*/

export default class Contacts extends Component {
  constructor(threadsHandler, id, hideNav, showNav, userID) {
        super()
        this.state = {
        
        }
    
    }
 
  render() {
    return (
      <Container>
        <Header>
            {
                //header shit
            }
        </Header>
        <Content>
            {
                //content shit
            }
        </Content>
       
    </Container>
    );
  }
}


