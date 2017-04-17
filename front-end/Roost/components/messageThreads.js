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
  TouchableHighlight
} from 'react-native';
import Chat from './Chat.js'
import path from '../properties.js'

/*  
    
*/

var threads = [{title: 'baseball'},
            {title: 'soccer'},
            {title: 'tennis'},
            {title: 'hockey'},
            {title: 'breakfast'},
            {title: 'Hiking'},
            {title: 'CS307'}]


export default class MessageThreads extends Component {
  constructor(hideNav, showNav, user) {
        super()
        this.state = {
            group: '',
            threads: true,
            id: 0,
            chatId: 0,
            data: [],
            filteredData: []
        }
       this.renderData = this.renderData.bind(this)
       this.threadsHandler = this.threadsHandler.bind(this)
       this.data = this.data.bind(this)
  }

      data (d) {
        this.setState({data: d.data})
        var filtered = this.state.data.filter((item) => item.status === 'open'); 
        this.setState({filteredData: filtered})
    }


      componentWillMount() {
      //set activities array
        var dist = this.props.user.dist
        var id = this.props.user.username
        let ws = `${path}/api/activities/${id}/getactivities`
        let xhr = new XMLHttpRequest();
        xhr.open('GET', ws);
        xhr.onload = () => {
        if (xhr.status===200) {
            console.warn(xhr.responseText)
            var json = JSON.parse(xhr.responseText);
            //onsole.warn(json.data[1].name)
            this.data(json)
            //console.warn('successful getting activites')
        } else {
            console.warn('error getting activites')
        }
        }; xhr.send()
        //console.log(search)
    }

    threadsHandler () {
      this.setState({threads: true})
    }
    renderData () {
      if (this.state.threads && this.state.data !== []) {
        return (
          <Container>
        <Header>
            <Left>
            </Left>
            <Body>
                <Title>My Groups</Title>
            </Body>
            <Right/>
        </Header>
        <Content>
          <List dataArray={this.state.filteredData} renderRow={(data) =>
                <TouchableHighlight onPress={ () => console.warn('press') }>
                  <ListItem thumbnail>
                        <Left>
                            <Thumbnail square size={40} source={require('./img/water.png')} />
                        </Left>
                        
                        <Body>
                            <Text>{data.name}</Text>
                            <Text note></Text>
                        </Body>
                        
                        <Right>
                            <Button transparent onPress={() => {this.props.hideNav(), this.setState({threads: false}),
                              this.setState({id: data.ActivityId}), this.setState({chatID: data.chatId}), this.setState({group: data.name})}}>
                                <Text>Chat</Text>
                            </Button>
                        </Right>
                      </ListItem>
                    </TouchableHighlight>
              } />
        </Content>
      </Container>
        )
      }
      else {
        return (
          <Chat threadsHandler={this.threadsHandler}
                groupID={this.state.id}
                chatID={this.state.chatID}
                chatName = {this.state.group}
                hideNav={this.props.hideNav}
                showNav={this.props.showNav}
                userID={this.props.user.username}/>
        )
      }
    }

  render() {
    return (
      <Container>
        {this.renderData()}
      </Container>
    );
  }
}


