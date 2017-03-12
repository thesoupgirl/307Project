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
import Login from './login'


/*  
    
*/

var data = [{category: 'sports', title: 'baseball', description: 'come play!'},
            {category: 'sports', title: 'soccer', description: 'come play!'},
            {category: 'sports', title: 'tennis', description: 'come play!'},
            {category: 'sports', title: 'hockey', description: 'come play!'}]


export default class Categories extends Component {
  constructor(props) {
        super(props)
        this.state = {
          page: 'search',
          active: true
        }
       this.click = this.click.bind(this)
       this.renderCategory = this.renderCategory.bind(this)
       this.renderData = this.renderData.bind(this)
    }

    click (page) {
      this.state.page = page
    }

    renderData () {
      data.map(function(item){
        return <Text>{item.title}</Text>;
      })
    }

    renderCategory() {
      if (this.state.page === 'search') {
        return (  
        <View>
        <Header>
            <Left>
            </Left>
            <Body>
                <Title>Categories</Title>
            </Body>
            <Right/>
        </Header>
        <View style={styles.center}>
          <Text style={styles.title}>Categories</Text>
        </View>
          <ListItem button onPress={() => {this.setState({ page: 'sports' })}}>
                <Text>Sports</Text>
            </ListItem>
            <ListItem button onPress={() => {this.setState({ page: 'Eat' })}}>
                <Text>Eat</Text>
            </ListItem>
            <ListItem button onPress={() => {this.setState({ page: 'Adventures' })}}>
                <Text>Adventures</Text>
            </ListItem>
            <ListItem button onPress={() => {this.setState({ page: 'Study Groups' })}}>
                <Text>Study Groups</Text>
            </ListItem>
            </View>
        )
      }
      else {
        return (
        <View>
          <Header>
            <Left>
                <Button transparent onPress={() => {this.setState({ page: 'search' })}}>
                    <Icon name='arrow-back' />
                </Button>
            </Left>
            <Body>
                <Title>Roost</Title>
            </Body>
            <Right>
            </Right>
        </Header>
        
        <Text>{this.state.page}</Text>
        {this.renderData()}
        </View>
        )
      }
      
    }
    

  render() {
    return (
      <Container>
        {this.renderCategory()}
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


