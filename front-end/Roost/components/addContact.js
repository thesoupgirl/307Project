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
  Alert,
  TextInput
} from 'react-native';
import path from '../properties.js'

/*  
    DAVID: Using dummy data while creating. Declare variables above class
*/

export default class AddContact extends Component {
  constructor(user) {
        super()
        this.state = {
          search: 'hi'
        }
        this.addFriend = this.addFriend.bind(this)
    
    }
    addFriend () {
        var user = this.props.user.username
        favorite = this.state.search
        ws = `${path}/api/users/${user}/${favorite}` //fix route
        xhr = new XMLHttpRequest();
        xhr.open('POST', ws);
        xhr.onload = () => {
        if (xhr.status===200) {
            console.warn('succesfully added favorite')
            this.props.update()
            
        } else {
            console.warn('failed to add favorite')

        }
        }; xhr.send()
    }
 
  render() {
    return (
    <Container>
        <Content>
        <View style={{padding: 15}}>
            <TextInput
            style={{height: 40}}
            placeholder="Search for user"
            onChangeText={(search) => this.setState({search})}
            /> 
            <Button transparent onPress={() => this.addFriend()}><Text>Save user</Text></Button>
        </View>
        </Content>
    </Container>
    );
  }
}

