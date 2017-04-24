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


export default class Polls extends Component {
  constructor(menu) {
        super()
        this.state = {
          question: '',
          response1: '',
          response2: '',
          response3: '',
          response4: '',
          response5: '',
          link: ''
        }
        this.sendForm = this.sendForm.bind(this)
    }

    sendForm () {
      if (this.state.response1 === '') {
          Alert.alert(
            'You need at least one response!',      
          )
        return
      }
      var question = this.state.question
      var response1 = this.state.response1
      var response2 = this.state.response2
      var response3 = this.state.response3
      var response4 = this.state.response4
      var response5 = this.state.response5
      let ws = `${path}/api/chat/createpoll`
      let xhr = new XMLHttpRequest();
      xhr.open('POST', ws);
      xhr.onload = () => {
      if (xhr.status===200) {
          console.warn('created poll')
          var json = JSON.parse(xhr.responseText);
          for (var i = 0; i < json.headers.length; i++) {
           // console.warn(json.headers[i].value)
            if (json.headers[i].key === "Location")
              this.setState({link: json.headers[i].value})  
          }
          var groupID = this.props.gid
          var chatID = this.props.cid
          var message = 'hi'
          var user = this.props.username
          ws = `${path}/api/chat/${groupID}/${chatID}/addinvite`
          xhr = new XMLHttpRequest();
          xhr.open('POST', ws);
          xhr.onload = () => {
          if (xhr.status===200) {
              console.warn('Created Message')

        
          } else {
              console.warn('Failed to create message')

          }
          }; xhr.send(`message=${message}&user=${user}`)
          this.renderBody
          
      } else {
              Alert.alert(
              'Error creating poll',      
      )
      }
      }; xhr.send(`question=${question}&response1=${response1}&response2=${response2}&response3=${response3}&response4${response4}&response5=${response5}`)
      this.renderBody

      this.props.update()

    }
 
  render() {
    return (
    <Container>
      <Header>
        <Left>
            <Button transparent onPress={() => this.props.menu()}>
                <Icon name='arrow-back' />
            </Button>
        </Left>
        <Body>
            <Title>Poll</Title>
        </Body>
        <Right>
        </Right>
        </Header>
      <Content>
        <View style={{padding: 20}}>
         <TextInput
            style={{height: 60, fontSize: 25}}
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

         <Button onPress={() => this.sendForm()} block><Text>Add Poll</Text></Button>
         
      </View>
      </Content>
       
       
    </Container>
    );
  }
}