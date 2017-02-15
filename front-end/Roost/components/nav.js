import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title, Right,
DeckSwiper, Card, CardItem, Thumbnail} from 'native-base';
import SwipeActivities from './swipeActivities.js';
//import getTheme from './native-base-theme';
import {
  AppRegistry,
  StyleSheet,
  Navigator
} from 'react-native';
var styles = require('./styles'); 

  
export default class Nav extends Component {

  constructor(props) {
        super(props)
        this.state = {
          page: 'explore'
            
        }

        this.selectedPage = this.selectedPage.bind(this)
        this.handleExplore = this.handleExplore.bind(this)
       
    }

    handleExplore() {
      //this.setState({page: 'explore'})
    }

    selectedPage() {
      if (this.state.page === 'hi')
        return (<Text>hi</Text>)
      else if (this.state.page === 'explore')
        return <SwipeActivities/>
      return <Text>nothing</Text>
    }

       render() {
        return (
            <Container>
                <Header>
                    <Left>
                        <Button transparent>
                            <Icon name='menu' />
                        </Button>
                    </Left>
                    <Body>
                       <Text>Roost</Text>
                    </Body>
                    <Right />
                </Header>

                <Content>
                    {this.selectedPage()}
                </Content>
                <Footer>
                      <FooterTab>
                          <Button active={this.state.page === 'explore'} onPress={this.handleExplore()}>
                              <Icon name="globe" />
                          </Button>
                          <Button>
                              <Icon name="folder" />
                          </Button>
                          <Button>
                              <Icon name="add-circle" />
                          </Button>
                          <Button >
                              <Icon name="chatboxes" />
                          </Button>
                          <Button>
                              <Icon name="person" />
                          </Button>
                      </FooterTab>
                  </Footer>
            </Container>
        );
    }
}