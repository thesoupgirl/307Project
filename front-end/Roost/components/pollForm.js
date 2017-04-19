import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, List, ListItem, Segment, Tab, Tabs} from 'native-base';
var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
  Image,
  TouchableHighlight,
  Dimensions,
  Alert,
  TextInput
} from 'react-native';
import path from '../properties.js'


export default class PollForm extends Component {
  constructor() {
        super()
        this.state = {
          question: '',
          response1: '',
          response2: '',
          response3: '',
          response4: '',
          response5: '',
        }
    }
 
  render() {
    return (
    <Container>
      <Header>
            <Text> PLACEHOLDER </Text>
      </Header>
      <Content>
        <View style={{padding: 20}}>
         <TextInput
            style={{height: 60}}
            placeholder = 'Enter Poll Question Here'
            onChangeText={(question) => this.setState({question})}
         />

        <Text style={{fontSize: 12}}> (Leave excess response options blank to ignore them)</Text>
        <Text/>

         <TextInput
            style={{height: 60}}

            placeholder = 'Response 1'
            onChangeText={(response1) => this.setState({response1})}
         />
         <TextInput
                     style={{height: 60}}

            placeholder = 'Response 2'
            onChangeText={(response2) => this.setState({response2})}
         />
         <TextInput
                     style={{height: 60}}

            placeholder = 'Response 3'
            onChangeText={(response3) => this.setState({response3})}
         />
         <TextInput
            placeholder = 'Response 4'
                        style={{height: 60}}

            onChangeText={(response4) => this.setState({response4})}
         />
         <TextInput
            placeholder = 'Response 5'
                        style={{height: 60}}

            onChangeText={(response5) => this.setState({response5})}
         />
         
      </View>
      </Content>
       
       
    </Container>
    );
  }
}