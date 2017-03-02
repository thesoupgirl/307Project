import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, Item,
Picker, Input, Label, Form} from 'native-base';
var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
  Navigator,
  Image
} from 'react-native';
import Login from './login.js'
import Launch from './launch.js'


/*  
    
*/


export default class Profile extends Component {
  constructor({handler}) {
        super()
        this.state = {
            username:'',
            password:'',
            dist: '5',
            logout: false,
            updateSetttings: false
        }
       //this.logoff = this.logoff.bind(this);
       this.disp = this.disp.bind(this);
       this.info = this.info.bind(this);
  }

  componentWillMount() {
    //CALL TO GET USER INFORMATION
  }
  info () {
    if (this.state.updateSetttings)
      return (
        <Content>
          <Form>
              <Item stackedLabel>
                  <Label>Username</Label>
                  <Input />
              </Item>
              <Item stackedLabel last>
                  <Label>Password</Label>
                  <Input />
              </Item>
          </Form>
          <Content>
            <Text>choose distance:</Text>
                    <Picker
                        iosHeader="Select one"
                        mode="dropdown"
                        selectedValue={this.state.dist}
                        onValueChange={(distance) => {this.setState({dist: distance})}}>
                        <Item label="5 miles" value="5" />
                        <Item label="10 miles" value="10" />
                        <Item label="20 miles" value="20" />
                        <Item label="30 miles" value="30" />
                        <Item label="40 miles" value="50" />
                   </Picker>
                </Content>
          <Button light block style={styles.center} onPress={() => this.setState({logout: true},
             this.props.hideNav())}><Text>Logout</Text></Button>
        </Content>
      )
    
  }
    disp() {
      if (this.state.logout) {
        return (<Launch/>)
      }
      else {
        return (
        <Container>
        <Header>
            <Left>
            </Left>
            <Body>
                <Title>Profile</Title>
            </Body>
            <Right/>
        </Header>
        <Text></Text>
         <Button light block style={styles.center} onPress={() => this.setState({updateSetttings: !this.state.updateSetttings})
         }><Text>Update Profile</Text></Button>
            {this.info()}
        <Text></Text>
          <Button light block style={styles.center} onPress={() => this.setState({logout: true},
             this.props.hideNav())}><Text>Logout</Text></Button>
      </Container>
      )}
    }


  render() {
    return (
      <View style={styles.container}>
        {this.disp()}
      </View>
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