import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1} from 'native-base';
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
        name: 'Baseball',
        description: 'Come join us for a game of baseball',
        image: require('./img/water.png')
    },
    {
        name: 'Soccer',
        description: 'Come join us for a game of soccer',
        image: require('./img/water.png')
    },
    {
        name: 'Hockey',
        description: 'Come join us for a game of hockey',
        image: require('./img/water.png')
    },
    {
        name: 'Study',
        description: 'Come join us to study for CS307',
        image: require('./img/water.png')
    }
];


export default class SwipeActivities extends Component {
  constructor(props) {
        super(props)
        this.state = {
            
        }
       this.right = this.right.bind(this)
       this.left = this.left.bind(this)
    }

    right () {
        console.warn('right')
        //add user to group with ajax call
    }
    left () {
        console.warn('left')
        //get next card
    }
  render() {
    return (
      <Container>
          <Header>
            <Left>
            </Left>
            <Body>
                <Title>Explore</Title>
            </Body>
            <Right/>
        </Header>
        <View padder >
             <DeckSwiper
                onSwipeRight={() => this.right()}
                onSwipeLeft={() => this.left()}
                dataSource={cards}
                renderItem={item =>
                    <Card style={{ elevation: 3 }}>
                        <CardItem>
                            <Left>
                                <Thumbnail source={item.image} />
                            </Left>
                            <Body>
                                <Text>{item.name}</Text>
                                <Text note>{item.description}</Text>
                            </Body>
                        </CardItem>
                        <CardItem cardBody>
                            <Image style={{ resizeMode: 'cover', width: null, flex: 1, height: 300 }} source={item.image} />
                        </CardItem>
                        <CardItem>
                            <Left>
                            <Button danger onPress={() => this.left()}><Text> Skip </Text></Button>
                            </Left>
                            
                            <Text>{item.name}</Text>
                            
                            <Right>
                            <Button success onPress={() => this.right()}><Text> Join </Text></Button>
                            </Right>
                        </CardItem>
                    </Card>
                }
            />
                                
        </View>
      </Container>
    );
  }
}




