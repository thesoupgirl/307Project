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
            page: 'add',
            category: 'None',
            activity: '',
            description: '',
            num: 0,
            selected: []
        }
       this.submit = this.submit.bind(this)
       this.getSelectedImages = this.getSelectedImages.bind(this)
       this.renderPage = this.renderPage.bind(this)
    }

    getSelectedImages(images, current) {
      var num = images.length;

      this.setState({
        num: num,
        selected: images,
      });
    }

    submit () {
      
    }

    renderPage () {
      if (this.state.page === 'add') {
        return (
      <Content>
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
       
        <Text/>
        <Button block  success onPress={this.submit()}><Text>Submit Activity</Text></Button>
      </Content>
      </Content>
    );
      }
      else if (this.state.page === 'pic') {
        return (
          <Content>

          </Content>
        )
      }
    }

  render() {
    return (
    <Container>{this.renderPage()}</Container>
    )
  }
}

const styles = {
    title: {

    },
    center: {
      justifyContent: 'center',
      alignItems: 'center',
    },
    container: {
    flex: 1,
    backgroundColor: '#F6AE2D',
  },
  content: {
    marginTop: 15,
    height: 50,
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    flexWrap: 'wrap',
  },
  text: {
    fontSize: 16,
    alignItems: 'center',
    color: '#fff',
  },
  bold: {
    fontWeight: 'bold',
  },
  info: {
    fontSize: 12,
  }
};