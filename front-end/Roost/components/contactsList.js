import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, List, ListItem, Segment, Tabs} from 'native-base';
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
    DAVID: Using dummy data while creating. Declare variables above class
*/

export default class ContactsList extends Component {
  constructor(threadsHandler, id, hideNav, showNav, userID) {
        super()
        this.state = {
          
        }
    }
 
  render() {
    return (
    <Container>
      <Header>
            <Text style={{fontSize: 18, fontWeight: 'bold', alignSelf: 'center'}}>Contacts List</Text>
      </Header>

      <Content>
       	<List>
            <ListItem>
                <Thumbnail source={require('./img/person.png')} />
                <Text>Anoop</Text>
            </ListItem>
            <ListItem>
                <Thumbnail source={require('./img/person.png')} />
                <Text>Lord and Savior Gustavo</Text>
            </ListItem>
        </List>
       </Content>
       
    </Container>
    );
  }
}