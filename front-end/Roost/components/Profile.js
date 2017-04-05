import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, Item,
Picker, Input, Label, Form, CheckBox, ListItem} from 'native-base';
var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
  Navigator,
  Image,
  TextInput,
  Switch,
  Alert,
  ScrollView
} from 'react-native';
import Login from './login.js'
import Launch from './launch.js'
var md5 = require('md5');
import TextField from 'react-native-md-textinput';
import path from '../properties.js'



/*  
    
*/


export default class Profile extends Component {
  constructor({hideNav, user}) {

        super()
        this.state = {
            id: '',
            username:'',
            password:'',
            dist: '5',
            push: false,
            logout: false,
            updateSetttings: false
        }
       //this.logoff = this.logoff.bind(this);
       this.disp = this.disp.bind(this);
       this.info = this.info.bind(this);
       this.userUpdate = this.userUpdate.bind(this)
  }
  userUpdate () {
      console.warn(this.state.username)
      console.warn(this.state.password)
      var username = this.state.username
      var password = md5(this.state.password)
      var notifications;
      if (this.state.push)
        notifications = 1;
      else notifications = 0;
      var distance = this.state.dist
      let ws = `${path}/api/users/update/${this.state.id}`
      let xhr = new XMLHttpRequest();
      xhr.open('POST', ws, true);
      xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
      xhr.onload = () => {
      if (xhr.status===200) {
          //this.props.handler(this.state, true)
          this.setState({logout: true},
          this.props.hideNav())
          Alert.alert(
                'Account updated! Please log back in!',      
        )
      } else {
        Alert.alert(
                'Updating information failed',      
        )
      }
    }; xhr.send(`username=${username}&password=${password}&notifications=${notifications}&distance${distance}`)
    }

  componentWillMount() {
    //CALL TO GET USER INFORMATION
    this.setState({id: this.props.user.username})
    this.setState({push: !this.state.push})
    this.setState({username: this.props.user.username, 
                   password: this.props.user.password,
                   push: this.props.user.push})
  }
  componentDidMount() {

  }
  info () {
    if (this.state.updateSetttings)
      return (
        <Content>
      <ScrollView>
        <TextField label={'password'} highlightColor={'#00BCD4'}
                   onChangeText={(text) => {
                   this.state.password = text;}}
                   value={this.state.password} />
      </ScrollView>
      <Text>push notifications</Text>
      <Switch
          onValueChange={(value) => this.setState({push: value})}
          value={this.state.push} />
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
                        <Item label="50 miles" value="50" />
                   </Picker>
               
                 <Button light block style={styles.center} onPress={() => this.setState({updateSetttings: !this.state.updateSetttings},
             this.userUpdate())}>
                   <Text>Confirm Changes</Text></Button>
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
        <Content style={{padding: 20}}>
         <Button primary block style={styles.center} onPress={() => this.setState({updateSetttings: !this.state.updateSetttings})
         }><Text>Update Profile</Text></Button>
            {this.info()}
        <Text></Text>
          <Button danger block style={styles.center} onPress={() => this.setState({logout: true},
             this.props.hideNav())}><Text>Logout</Text></Button>
      </Content>
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