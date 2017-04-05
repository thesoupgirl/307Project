import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, H3} from 'native-base';
var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
  Navigator,
  Image,
  Alert
} from 'react-native';
import path from '../properties.js'


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
  constructor(user) {
        super()
        this.state = {
            
        }
       this.right = this.right.bind(this)
       this.left = this.left.bind(this)
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

    right () {
        console.warn('right')
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
                            {
                            //<Left>
                            //<Thumbnail source={item.image} />
                            //</Left>
                            }
                            
                            <Body>
                                <Text>{item.name}</Text>
                                <Text note>{item.description}</Text>
                            </Body>
                        </CardItem>
                        <CardItem cardBody>
                            <Image style={{ resizeMode: 'cover', width: null, flex: 1, height: 300 }} source={item.image} />
                        </CardItem>
                        <CardItem>
                            {//
                            //<Left>
                            //<Button danger onPress={() => this.left()}><Text> Skip </Text></Button>
                            //</Left>
                            }
                           
                            <H3>{item.name}</H3>
                            {
                            //<Right>
                            //<Button success onPress={() => this.right()}><Text> Join </Text></Button>
                            //</Right>
                            }
                        </CardItem>
                    </Card>
                }
            />            
        </View>
      </Container>
    );
  }
}




