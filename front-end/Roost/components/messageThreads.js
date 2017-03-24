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
  constructor(hideNav, showNav) {
        super()
        this.state = {
            threads: true,
            id: 0
        }
       this.renderData = this.renderData.bind(this)
       this.threadsHandler = this.threadsHandler.bind(this)
  }

    threadsHandler () {
      this.setState({threads: true})
    }
    renderData () {
      if (this.state.threads) {
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
          <List dataArray={threads} renderRow={(data) =>
                <TouchableHighlight onPress={ () => console.warn('press') }>
                  <ListItem thumbnail>
                        <Left>
                            <Thumbnail square size={40} source={require('./img/water.png')} />
                        </Left>
                        
                        <Body>
                            <Text>{data.title}</Text>
                            <Text note></Text>
                        </Body>
                        
                        <Right>
                            <Button transparent onPress={() => {this.props.hideNav(), this.setState({threads: false})}}>
                                <Text>chat</Text>
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
                chatID={this.state.id}
                hideNav={this.props.hideNav}
                showNav={this.props.showNav}/>
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


