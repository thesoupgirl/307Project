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
  constructor(favorites) {
        super()
        this.state = {
          favorites: ['Danny','Anoop69']
        }
    }

    componentWillMount () {
      //this.setState({favorites: this.props.favorites})
    }
 
  render() {
    return (
    <Container>
      <Content>
       	<List dataArray={this.state.favorites} renderRow={(data) =>
                            <ListItem thumbnail>
                      <Body>
                          <Text>{data}</Text>
                          <Text note></Text>
                      </Body>
                    </ListItem>
            } />
       </Content>
       
    </Container>
    );
  }
}