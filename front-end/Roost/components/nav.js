import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title, Right,
DeckSwiper, Card, CardItem, Thumbnail} from 'native-base';
import SwipeActivities from './swipeActivities.js';
import Categories from './categories.js';
import AddActivity from './addActivity.js';
import MessageThreads from './messageThreads.js';
import Profile from './Profile.js';


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
          page: 'explore',
          active: true
            
        }

        this.selectedPage = this.selectedPage.bind(this)
       
    }

    selectedPage() {
        if (this.state.page === 'explore')
            return <SwipeActivities/>
        else if (this.state.page === 'categories')
            return <Categories/>
        else if (this.state.page === 'add')
            return <AddActivity/>
        else if (this.state.page === 'messages')
            return <MessageThreads/>
        else if (this.state.page === 'profile')
            return <Profile/>
    }

       render() {
        return (
            <Container>
                <Content>
                    {this.selectedPage()}
                </Content>
                <Footer>
                      <FooterTab>
                          <Button active={this.state.page === 'explore'}
                                  onPress={() => {this.setState({ page: 'explore' })
                                  this.selectedPage()}}>
                              <Icon name="globe" />
                          </Button>
                          <Button active={this.state.page === 'categories'}
                                  onPress={() => {this.setState({ page: 'categories' })
                                  this.selectedPage()}}>
                              <Icon name="folder" />
                          </Button>
                          <Button active={this.state.page === 'add'}
                                  onPress={() => {this.setState({ page: 'add' })
                                  this.selectedPage()}}>
                              <Icon name="add-circle" />
                          </Button>
                          <Button active={this.state.page === 'messages'}
                                  onPress={() => {this.setState({ page: 'messages' })
                                  this.selectedPage()}}>
                              <Icon name="chatboxes" />
                          </Button>
                          <Button active={this.state.page === 'profile'}
                                  onPress={() => {this.setState({ page: 'profile' })
                                  this.selectedPage()}}>
                              <Icon name="person" />
                          </Button>
                      </FooterTab>
                  </Footer>
            </Container>
        );
    }
}