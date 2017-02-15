import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail} from 'native-base';
var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
  Navigator,
  Image
} from 'react-native';

/*  
    
*/

const cards = [
    {
        name: 'One',
        image: 'http://cdn.wonderfulengineering.com/wp-content/uploads/2016/02/red-wallpaper-3D.jpg'
    },
    {
        name: 'Two',
        image: 'http://g01.a.alicdn.com/kf/HTB1U84HKpXXXXbOXpXXq6xXFXXXn/Washable-wrinkle-resistant-font-b-photo-b-font-font-b-backdrops-b-font-150cm-x-200.jpg'
    },
    {
        name: 'Three',
        image: 'http://a.rgbimg.com/cache1nGAOw/users/b/br/branox/300/mi2ZMGO.jpg'
    },
    {
        name: 'Four',
        image: 'http://files.gretastyleinitaly.webnode.it/200002182-308f5318c9/friends-forever-red-pattern.jpg'
    }
];


export default class SwipeActivities extends Component {
  constructor(props) {
        super(props)
        this.state = {
            
        }
       
    }


  render() {
    return (
      <Container>
      <View padder >
                    <DeckSwiper dataSource={cards}
                        renderItem={(item)=>
                            <Card style={{elevation: 3}}>
                                <CardItem>
                                    <Thumbnail source={{uri : item.image}} />
                                    <Text>Instrumental Songs</Text>
                                    <Text note>Guitar</Text>
                                </CardItem>
                                <CardItem>
                                    <Image style={{ resizeMode: 'cover' }} source={{uri : item.image}} />
                                </CardItem>
                                <CardItem>
                                    <Icon name={'ios-heart'} style={{color : '#ED4A6A'}} />
                                    <Text>{item.name}</Text>
                                </CardItem>
                            </Card>
                        }>
                    </DeckSwiper>
                </View>
      </Container>
    );
  }
}


