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
        num: 2,
        name: 'Baseball',
        description: 'Come join us for a game of baseball',
        image: require('./img/water.png')
    },
    {
        num: 8,
        name: 'Soccer',
        description: 'Come join us for a game of soccer',
        image: require('./img/water.png')
    },
    {
        num: 17,
        name: 'Hockey',
        description: 'Come join us for a game of hockey',
        image: require('./img/water.png')
    },
    {
        num: 20,
        name: 'Study',
        description: 'Come join us to study for CS307',
        image: require('./img/water.png')
    }
].sort(function(obj1, obj2) {
	// Ascending: first age less than the previous
	return obj2.num - obj1.num;

});

export default class SwipeActivities extends Component {
  constructor(user) {
        super()
        this.state = {
            activityID: ''
        }
       this.right = this.right.bind(this)
       this.left = this.left.bind(this)
       this.setI = this.setI.bind(this)
    }

    setI (item) {
        //i = item.data
        console.warn(item)

    }

    componentWillMount() {
      //set activities array
      /*
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
        */
    }

    right (id) {
        console.warn(this.state.activityID)
        //add user to group with ajax call
         //call to authenticate
    
        //console.warn(md5(this.state.password));
        var username = this.props.user.username
        var password = this.props.user.password
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
        }; xhr.send(`username=${username}&password=${password}`)
        this.renderBody       
    }
    left () {
        //console.warn('left')
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
                <Card style={{ elevation: 1 }}>
                    <CardItem>
                        {
                        //<Left>
                        //<Thumbnail source={item.image} />
                        //</Left>
                        }
                        {(item) => this.setState({activityID: item.num}) }
                    
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
                        <Left>
                        <Button danger onPress={() => this.left()}><Text> Skip </Text></Button>
                        </Left>
                        }
                        <Text>{item.name}</Text>
                        {
                        <Right>
                        <Button success onPress={() => this.right(item.name)}><Text> Join </Text></Button>
                        </Right>
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




