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

  constructor({handler, user}) {
        super()
        this.state = {
          page: 'explore',
          active: true,
          show: true

            
        }

        this.selectedPage = this.selectedPage.bind(this)
        this.renderNav = this.renderNav.bind(this)
        this.hideNav = this.hideNav.bind(this)
       
    }

    componentDidMount() {
       //console.warn(this.props.user)
    }

    hideNav () {
        this.setState({show: false})
    }

    renderNav() {
        if (this.state.show) {
            return (
               
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
            
            )
        }
    }

    selectedPage() {
        if (this.state.page === 'explore')
            return <SwipeActivities handler = {this.props.handler}/>
        else if (this.state.page === 'categories')
            return <Categories handler = {this.props.handler}/>
        else if (this.state.page === 'add')
            return <AddActivity handler = {this.props.handler}/>
        else if (this.state.page === 'messages')
            return <MessageThreads handler = {this.props.handler}/>
        else if (this.state.page === 'profile')
            return <Profile handler = {this.props.handler}
                            hideNav = {this.hideNav}
                            user = {this.props.user}/>
    }

       render() {
        return (
            <Container>
                <Content>
                    {this.selectedPage()}
                </Content>
                {this.renderNav()}
            </Container>
        );
    }
}