import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
<<<<<<< HEAD
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, List, ListItem, Segment} from 'native-base';
=======
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, List, ListItem, Segment, Tab, Tabs} from 'native-base';
>>>>>>> contacts
var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
<<<<<<< HEAD
  Navigator,
=======
>>>>>>> contacts
  Image,
  TouchableHighlight,
  Dimensions,
  Alert
} from 'react-native';
import path from '../properties.js'

/*  
    DAVID
*/

import AddContact from './addContact'
import ContactsList from './contactsList'


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
            <Body>
                <Title>Contacts</Title>
            </Body>
      </Header>
      <Content>
      <Tabs>
                <Tab heading="My Contacts">
                    <ContactsList />
                </Tab>
                <Tab heading="Add Contact">
                    <AddContact />
                </Tab>
      </Tabs>
      </Content>
    
       
    </Container>
    );
  }
}