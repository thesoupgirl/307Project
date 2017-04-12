import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, List, ListItem, Segment, Tabs, InputGroup, Input} from 'native-base';
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

export default class AddContact extends Component {
  constructor(threadsHandler, id, hideNav, showNav, userID) {
        super()
        this.state = {
          
        }
    }
 
  render() {
    return (
    <Container>
    <Header><Text style={{fontSize: 18, fontWeight: 'bold', alignSelf: 'center'}}>Input User ID and press enter.</Text></Header>
                <Content>
                    <InputGroup borderType='rounded'>
                        <Icon name='md-person' style={{color:'#384850'}}/>
                        <Input placeholder='<Search User ID Here>'/>
                    </InputGroup>
                </Content>
            </Container>
    );
  }
}