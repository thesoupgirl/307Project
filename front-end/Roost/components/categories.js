import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, ListItem, connectStyle} from 'native-base';
var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
  Navigator,
  Image
} from 'react-native';


/*  
    
*/


export default class Categories extends Component {
  constructor(props) {
        super(props)
        this.state = {
          page: 'explore',
          active: true
        }
       this.click = this.click.bind(this)
    }

    click () {
      if (this.state.page === 'explore')
            return <SwipeActivities/>
    }
    

  render() {
    return (
      
      <Container>  
        <View>
        <View style={styles.center}>
          <H1 style={styles.title}>Categories</H1>
        </View>
          <ListItem onClick={this.click()}>
                <Text>Sports</Text>
            </ListItem>
            <ListItem>
                <Text>Eat</Text>
            </ListItem>
            <ListItem >
                <Text>Adventures</Text>
            </ListItem>
            <ListItem>
                <Text>Study Groups</Text>
            </ListItem>
            </View>
      </Container>
    );
  }
}

const styles = {
    title: {
      color: 'red',
    },
    center: {
      justifyContent: 'center',
      alignItems: 'center',
    }
};


