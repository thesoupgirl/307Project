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
        text: 'Card One',
        name: 'One',
        image: require('./img/water.png')
    },
    {
        text: 'Card One',
        name: 'Two',
        image: require('./img/water.png')
    },
    {
        text: 'Card One',
        name: 'Three',
        image: require('./img/water.png')
    },
    {
        text: 'Card One',
        name: 'Four',
        image: require('./img/water.png')
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
             <DeckSwiper
                        dataSource={cards}
                        renderItem={item =>
                            <Card style={{ elevation: 3 }}>
                                <CardItem>
                                    <Left>
                                        <Thumbnail source={item.image} />
                                    </Left>
                                    <Body>
                                        <Text>{item.text}</Text>
                                        <Text note>NativeBase</Text>
                                    </Body>
                                </CardItem>
                                <CardItem cardBody>
                                    <Image style={{ resizeMode: 'cover', width: null, flex: 1, height: 300 }} source={item.image} />
                                </CardItem>
                                <CardItem>
                                    <Left>
                                    <Button danger><Text> Skip </Text></Button>
                                    </Left>
                                    <H1>
                                    <Text>{item.name}</Text>
                                    </H1>
                                    <Right>
                                    <Button success><Text> Join </Text></Button>
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




