import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, Picker, Item} from 'native-base';
var styles = require('./styles'); 
import {
  CameraRoll,
  Switch,
  AppRegistry,
  StyleSheet,
  Navigator,
  Image,
  TextInput,
  ScrollView
} from 'react-native';
import TextField from 'react-native-md-textinput';
import CameraRollPicker from 'react-native-camera-roll-picker';

var pic = require('./img/water.png')


/*  
    
*/


export default class AddActivity extends Component {
  constructor(props) {
        super(props)
        this.state = {
            category: 'none',
            activity: '',
            description: ''
        }
       this.submit = this.submit.bind(this)
    }

    submit () {
      
    }

  render() {
    return (
      <Container>
        <Header>
            <Left>
            </Left>
            <Body>
                <Title>Add Activity</Title>
            </Body>
            <Right/>
        </Header>
           <Content>
             <Text/>
             <View style={styles.center}>
             <H1 style={styles.title}>Add Activity</H1>
             </View>
          <ScrollView>
        <TextField label={'Activity'} highlightColor={'#00BCD4'} 
                    onChangeText={(text) => {
                    this.state.activity = text}} />
      </ScrollView>
      <ScrollView>
        <TextField label={'Description'} highlightColor={'#00BCD4'}
                   onChangeText={(text) => {
                   this.state.description = text}} />
      </ScrollView>
      <Text>choose category:</Text>
        <Picker
            iosHeader="Select one"
            mode="dropdown"
            selectedValue={this.state.category}
            onValueChange={(category) => {this.setState({category: category})}}>
            <Item label="N/A" value="None" />
            <Item label="Sports" value="Sports" />
            <Item label="Eat" value="Eat" />
            <Item label="Adventures" value="Adventures" />
            <Item label="Study Groups" value="Study Groups" />
        </Picker>
        <Text>choose picture:</Text>
        <Button block  success onPress={this.submit()}><Text>Update Profile</Text></Button>
      </Content>
      </Container>
    );
  }
}

const styles = {
    title: {

    },
    center: {
      justifyContent: 'center',
      alignItems: 'center',
    }
};
