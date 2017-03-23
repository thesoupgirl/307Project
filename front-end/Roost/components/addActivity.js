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
  ScrollView,
  PixelRatio
} from 'react-native';
import TextField from 'react-native-md-textinput';
var ImagePicker = require('react-native-image-picker');

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
            groupSize: '0',
            num: 0,
            latitude: null,
            longitude: null,
            error: null,
            selected: [],
            avatarSource: null
        }
       this.submit = this.submit.bind(this)
       this.renderPage = this.renderPage.bind(this)
       this.selectPhotoTapped = this.selectPhotoTapped.bind(this)
      
    }

    selectPhotoTapped() {
      const options = {
        quality: 1.0,
        maxWidth: 500,
        maxHeight: 500,
        storageOptions: {
          skipBackup: true
        }
      };

      ImagePicker.showImagePicker(options, (response) => {
        console.log('Response = ', response);

        if (response.didCancel) {
          console.log('User cancelled photo picker');
        }
        else if (response.error) {
          console.log('ImagePicker Error: ', response.error);
        }
        else if (response.customButton) {
          console.log('User tapped custom button: ', response.customButton);
        }
        else {
          let source = { uri: response.uri };

          // You can also display the image using data:
          // let source = { uri: 'data:image/jpeg;base64,' + response.data };

          this.setState({
            avatarSource: source
          });
        }
      });
    }

    componentDidMount() {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          this.setState({
            latitude: position.coords.latitude,
            longitude: position.coords.longitude,
            error: null,
          });
        },
        (error) => this.setState({ error: error.message }),
        { enableHighAccuracy: true, timeout: 20000, maximumAge: 1000 },
      );
  }

  componentWillUnmount() {
    navigator.geolocation.clearWatch(this.watchId);
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
      <ScrollView>
        <TextField label={'Group Size'} highlightColor={'#00BCD4'}
                   keyboardType={'numeric'}
                   onChangeText={(text) => {
                   this.state.groupSize = text}} />
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
        <Button onPress={this.selectPhotoTapped.bind(this)}><Text>Choose Image</Text></Button>
        <Text/>
        <View style={[styles.avatar, styles.avatarContainer, {marginBottom: 20}]}>
          { this.state.avatarSource === null ? null :
            <Image style={styles.avatar} source={this.state.avatarSource} />
          }
          </View>
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
  },
  avatarContainer: {
    borderColor: '#9B9B9B',
    borderWidth: 1 / PixelRatio.get(),
    justifyContent: 'center',
    alignItems: 'center'
  },
  avatar: {
    //borderRadius: 75,
    width: 90,
    height: 90
  }
};