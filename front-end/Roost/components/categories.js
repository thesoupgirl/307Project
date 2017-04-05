import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, ListItem, connectStyle, List} from 'native-base';
var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
  Navigator,
  Image,
  Alert
} from 'react-native';
import Login from './login'
import path from '../properties.js'



/*  
    
*/

var a = [{category: 'Sports', title: 'baseball', description: 'come play!'},
            {category: 'Sports', title: 'soccer', description: 'come play!'},
            {category: 'Sports', title: 'tennis', description: 'come play!'},
            {category: 'Sports', title: 'hockey', description: 'come play!'},
            {category: 'Eat', title: 'breakfast', description: 'come get breakfast!'},
            {category: 'Adventures', title: 'Hiking', description: 'come Hiking!'},
            {category: 'Study Groups', title: 'CS307', description: 'Looking for a group!'}]


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
       this.join = this.join.bind(this)
    }

    componentWillMount() {
      //set activities array
        let ws = `ROUTE`
        let xhr = new XMLHttpRequest();
        xhr.open('GET', ws);
        xhr.onload = () => {
        if (xhr.status===200) {
            var json = JSON.parse(xhr.responseText);
            json = json.data
            this.searchResults(json)
            console.warn('successful')
        } else {
            console.warn('error getting activites')
        }
        }; xhr.send()
        //console.log(search)
    }
    
    click (page) {
      this.state.page = page
    }

    join () {
        //add user to group with ajax call
        //call to authenticate
    
        //console.warn(md5(this.state.password));
        var id = this.props.user.username
        //console.warn(id)
        let ws = `${path}/api/activities/join/${id}`
        let xhr = new XMLHttpRequest();
        xhr.open('POST', ws);
        xhr.onload = () => {
        if (xhr.status===200) {
            console.warn('activity joined')
            
        } else {
                Alert.alert(
                'Error joining activity',      
        )

        }
        }; xhr.send()
        this.renderBody
    }

    renderData () {
      var filteredData = a.filter((item) => item.category === this.state.page);  
      return (
            <List dataArray={filteredData} renderRow={(data) =>
                <ListItem thumbnail>

                      <Left>
                          <Thumbnail square size={40} source={require('./img/water.png')} />
                      </Left>
                      <Body>
                          <Text>{data.title}</Text>
                          <Text note>{data.description}</Text>
                      </Body>
                      <Right>
                          <Button transparent onPress={() => this.join()}>
                              <Text>Join</Text>
                          </Button>
                      </Right>
                    </ListItem>
            } />
       
      
      )
      /*var d = data.map(function(item){
        return (<Text>{item.title}</Text>)
      })
      return  <Text>{d}</Text>*/
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
          <ListItem button onPress={() => {this.setState({ page: 'Sports' })}}>
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
                <Title>{this.state.page}</Title>
            </Body>
            <Right>
            </Right>
        </Header>
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


