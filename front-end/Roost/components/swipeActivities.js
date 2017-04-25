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


var c
var cards = [
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
        "num": 20,
        "name": 'Study',
        "description": 'Come join us to study for CS307',
        "image": require('./img/water.png')
    }
].sort(function(obj1, obj2) {
	// Ascending: first age less than the previous
	return obj2.num - obj1.num;

});



export default class SwipeActivities extends Component {
  constructor(user) {
        super()
        this.state = {
            index: 0,
            mounted: false,
            activityID: '',
            data: [{
             } ]
        }
       this.right = this.right.bind(this)
       this.left = this.left.bind(this)
       this.setI = this.setI.bind(this)
       this.data = this.data.bind(this)
       this.mountedData = this.mountedData.bind(this)
    }
    mountedData() {
        if (this.state.mounted && this.state.data != [])
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
        <View padder>
             <DeckSwiper
                //onSwipeRight={() => this.right()}
                //onSwipeLeft={() => this.left()}
                dataSource={this.state.data}
                renderItem={item =>  
                <Card style={{ elevation: 2 }}>
                    <CardItem>
                        {
                        //<Left>
                        //<Thumbnail source={item.image} />
                        //</Left>
                        }
                        {
                            //(item) => this.setState({activityID: item.num})
                        }
                        <Body>
                            <Text>{item.name}</Text>
                            <Text note>{item.description}</Text>
                        </Body>
                    </CardItem>
                    <CardItem cardBody>
                        <Image style={{ resizeMode: 'cover', width: null, flex: 1, height: 300 }} source={cards[0].image} />
                    </CardItem>
                    <CardItem>
                        {//
                        <Left>
                        <Text>{item.name}</Text>
                        </Left>
                        }
                        
                        {
                        <Right>
                        <Button success onPress={() => {this.right(item.ActivityId)}}><Text> Join </Text></Button>
                        </Right>
                        }
                    </CardItem>
                </Card>
            }
            />            
        </View>
      </Container>
        )
    }

    data (d) {
        
        var sorted = d.data.sort(function(obj1, obj2) {
	    // Ascending: first age less than the previous
	    return obj2.numMembers - obj1.numMembers;
        //this.setState({data: sorted})

});
    this.setState({data: sorted})
    this.setState({mounted: true})
    }

    setI (item) {
        //i = item.data
        //console.warn(item)

    }

    componentWillMount() {
      //set activities array
        var dist = this.props.user.dist
        var id = this.props.user.username
        let ws = `${path}/api/activities/${id}/${dist}/search`
        let xhr = new XMLHttpRequest();
        xhr.open('GET', ws);
        xhr.onload = () => {
        if (xhr.status===200) {
            //console.warn(xhr.responseText)
            var json = JSON.parse(xhr.responseText);
            //onsole.warn(json.data[1].name)
            this.data(json)
            //console.warn('successful getting activites')
        } else {
            //console.warn('error getting activites')
        }
        }; xhr.send()
        //console.log(search)
    }

    componentDidMount() {
        //console.warn(routeD)
        


    }

    right (id) {


        this.setState({index: this.state.index+1})
        //console.warn(id)
        //add user to group with ajax call
         //call to authenticate
    
        //console.warn(md5(this.state.password));
        var username = this.props.user.username
        var password = this.props.user.password
        //console.warn(id)
        let ws = `${path}/api/activities/join/${id}`
        let xhr = new XMLHttpRequest();
        xhr.open('POST', ws);
        xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        xhr.onload = () => {
        if (xhr.status===200) {
            //console.warn('activity joined')
            
        } else {
                Alert.alert(
                'Error joining activity. You may already be joined! (or kicked)',      
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
          {this.mountedData()}
      </Container>
    );
  }
}




