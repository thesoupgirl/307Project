import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, List, ListItem} from 'native-base';
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
import {GiftedChat} from 'react-native-gifted-chat'
import path from '../properties.js'

/*  
    
*/

var threads = [{title: 'baseball', description: 'come play!'},
            {category: 'Sports', title: 'soccer', description: 'come play!'},
            {category: 'Sports', title: 'tennis', description: 'come play!'},
            {category: 'Sports', title: 'hockey', description: 'come play!'},
            {category: 'Eat', title: 'breakfast', description: 'come get breakfast!'},
            {category: 'Adventures', title: 'Hiking', description: 'come Hiking!'},
            {category: 'Study Groups', title: 'CS307', description: 'Looking for a group!'}]


export default class Chat extends Component {
  constructor(threadsHandler, id, hideNav, showNav, userID) {
        super()
        this.state = {
            page: 'chat',
            messages: [
       
            ],
            updated: false
        }
     this.onSend = this.onSend.bind(this)
     this.renderPage = this.renderPage.bind(this) 
     this.leave = this.leave.bind(this)
     this.close = this.close.bind(this)
     this.open = this.open.bind(this)
     this.delete = this.delete.bind(this)
    
    }
    close () {
        var id = this.props.userID
        //console.warn(id)
        let ws = `${path}/api/activities/${id}/close`
        let xhr = new XMLHttpRequest();
        xhr.open('POST', ws);
        xhr.onload = () => {
        if (xhr.status===200) {
            console.warn('activity closed')
            
        } else {
                Alert.alert(
                'Error closing activity. You may not have this privilege',      
        )

        }
        }; xhr.send()
        this.renderBody
    }
    open() {
        var id = this.props.userID
        //console.warn(id)
        let ws = `${path}/api/activities/${id}/open`
        let xhr = new XMLHttpRequest();
        xhr.open('POST', ws);
        xhr.onload = () => {
        if (xhr.status===200) {
            console.warn('activity opened')
            
        } else {
                Alert.alert(
                'Error re-opening activity. You may not have this privilege',      
        )

        }
        }; xhr.send()
        this.renderBody
    }
    delete () {
        var id = this.props.userID
        //console.warn(id)
        let ws = `${path}/api/activities/${id}/deleteactivity`
        let xhr = new XMLHttpRequest();
        xhr.open('POST', ws);
        xhr.onload = () => {
        if (xhr.status===200) {
            console.warn('activity deleted')
            
        } else {
                Alert.alert(
                'Error deleting activity. You may not have this privilege',      
        )

        }
        }; xhr.send()
        this.renderBody
    }
    leave () {
      //call
        //console.warn(md5(this.state.password));
        var id = this.props.userID
        //console.warn(id)
        let ws = `${path}/api/chat/${id}/leave`
        let xhr = new XMLHttpRequest();
        xhr.open('POST', ws);
        xhr.onload = () => {
        if (xhr.status===200) {
            console.warn('activity left')
            
        } else {
                Alert.alert(
                'Error leaving activity',      
        )

        }
        }; xhr.send()
        this.renderBody
    }
    onSend(messages = []) {
        this.setState((previousState) => {
        return {
            messages: GiftedChat.append(previousState.messages, messages),
            
        };
    });        
    }
    renderPage() {
        if (this.state.page === 'chat') {
            return (
                
                <Container>
        <Header>
            <Left>
                <Button transparent onPress={() => {this.props.showNav(), this.props.threadsHandler()}}>
                    <Icon name='arrow-back' />
                </Button>
            </Left>
            <Body>
                <Title>{this.props.chatName}</Title>
            </Body>
            <Right>
                <Button transparent onPress={() => this.setState({page: 'menu'})}>
                    <Icon name='menu' />
                </Button>
            </Right>
        </Header>
       
          <GiftedChat
           
            messages={this.state.messages}
            onSend={this.onSend}
            user={{
            _id: 1,
            }}
            />
         
            </Container>

      )
        }
        
        else if (this.state.page === 'menu') {
            return (
                <Container>
        <Header>
            <Left>
                <Button transparent onPress={() => {this.setState({page: 'chat'})}}>
                    <Icon name='arrow-back' />
                </Button>
            </Left>
            <Body>
                <Title>Chat Menu</Title>
            </Body>
            <Right>
            </Right>
        </Header>
        <Content>
        <Text/>
        <View style={{padding: 20}}>
          <Button onPress={() => this.leave()} block>
            <Text>Leave Activity</Text>
          </Button >
          <Text/>
          <Button block>
            <Text>Close Activity</Text>
          </Button>
          <Text/>
          <Button block>
            <Text>Re-open Activity</Text>
          </Button>
          <Text/>
          <Button danger block>
            <Text>Delete Activity</Text>
          </Button>
          </View>
        </Content>
      </Container>
            )
        }
    }

    componentWillMount() {
    this.setState({
      messages: [
          {
          _id: 2,
          text: 'Please work',
          createdAt: new Date(Date.UTC(2017, 7, 30, 17, 20, 0)),
          user: {
            _id: 3,
            name: 'React Native',
            avatar: 'https://facebook.github.io/react/img/logo_og.png',
          },
        },
        {
          _id: 3,
          text: 'Hello developer',
          createdAt: new Date(Date.UTC(2016, 7, 30, 17, 20, 0)),
          user: {
            _id: 2,
            name: 'React Native',
            avatar: 'https://facebook.github.io/react/img/logo_og.png',
          },
        },
      ],
    });
  }

    
  render() {
    return (
      <View style={{flex: 1}}>
        {this.renderPage()}
      </View>
    );
  }
}


