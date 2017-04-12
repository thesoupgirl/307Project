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
            members: 3,
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
        var gid = this.props.groupID
        console.warn(gid)
        let ws = `${path}/api/activities/${gid}/close`
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
        }; xhr.send(`userId=${id}`)
        this.renderBody
    }
    open() {
        var id = this.props.userID
        var gid = this.props.groupID
        console.warn(gid)
        let ws = `${path}/api/activities/${gid}/open`
        let xhr = new XMLHttpRequest();
        xhr.open('POST', ws);
        xhr.onload = () => {
        if (xhr.status===200) {
            console.warn('activity opened')
            
        } else {
                Alert.alert(
                'Error opening activity. You may not have this privilege',      
        )

        }
        }; xhr.send(`userId=${id}`)
        this.renderBody
    }
    delete () {
        var id = this.props.userID
        var gid = this.props.groupID
        console.warn(gid)
        let ws = `${path}/api/activities/${gid}/deleteactivity`
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
        }; xhr.send(`userId=${id}`)
        this.renderBody
    }
    leave () {
        var id = this.props.userID
        var gid = this.props.groupID
        console.warn(gid)
        let ws = `${path}/api/chat/${gid}/leave`
        let xhr = new XMLHttpRequest();
        xhr.open('POST', ws);
        xhr.onload = () => {
        if (xhr.status===200) {
            console.warn('activity left')
            this.setState({page: 'chat'})
            
        } else {
                Alert.alert(
                'Error leaving activity.',      
        )

        }
        }; xhr.send(`userId=${id}`)
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
         <Button onPress={() => {this.setState({page: 'group'})}} block>
            <Text>Activity Members</Text>
          </Button >
          <Text/>
          <Button onPress={() => this.leave()} block>
            <Text>Leave Activity</Text>
          </Button >
          <Text/>
          <Button onPress={() => this.close()} block>
            <Text>Close Activity</Text>
          </Button>
          <Text/>
          <Button onPress={() => this.open()} block>
            <Text>Re-open Activity</Text>
          </Button>
          <Text/>
          <Button danger onPress={() => this.delete()} block>
            <Text>Delete Activity</Text>
          </Button>
          </View>
        </Content>
      </Container>
            )
        }
        else if (this.state.page === 'group') {
            return (
                <Container>
                    <Header>
                        <Left>
                            <Button transparent onPress={() => {this.setState({page: 'menu'})}}>
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
                    <Text># of Members {this.state.members}</Text>
                        <List dataArray={threads} renderRow={(data) =>
                            <ListItem thumbnail>

                      <Left>
                          <Thumbnail square size={40} source={require('./img/water.png')} />
                      </Left>
                      <Body>
                          <Text>{data.name}</Text>
                          <Text note>{data.description}</Text>
                      </Body>
                      <Right>
                          <Button transparent onPress={() => this.join(data.ActivityId)}>
                              <Text>Join</Text>
                          </Button>
                      </Right>
                    </ListItem>
            } />
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


