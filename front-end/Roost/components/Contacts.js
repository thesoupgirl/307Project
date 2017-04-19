import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,

Right, DeckSwiper, Card, CardItem, Thumbnail, H1, List, ListItem, Segment, Tab, Tabs} from 'native-base';

var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
  contacts,
  Image,
  TouchableHighlight,
  Dimensions,
  Alert
} from 'react-native';
import path from '../properties.js'

/*  
    DAVID
*/

import AddContact from './addContact'
import ContactsList from './contactsList'


export default class Contacts extends Component {
  constructor(threadsHandler, id, hideNav, showNav, userID) {
        super()
            this.state = {
                favorites: []
            }  
        }

        componentWillMount() {
            var ws = `${path}`//fix route
            var xhr = new XMLHttpRequest();
            xhr.open('GET', ws);
            xhr.onload = () => {
            if (xhr.status===200) {
                // console.warn('succesfully grabbed the number of users in chat')
                var json = JSON.parse(xhr.responseText);
                this.setState({favorites: json})
                //console.warn(+json)
                
            } else {
                console.warn('failed to get the number of users in a chat')

            }
                    }; xhr.send()
        }
 
        render() {
            return (
            <Container>
            <Header>
                    <Body>
                        <Title>Contacts</Title>
                    </Body>
            </Header>
            <Content>
            <Tabs>
                        <Tab heading="Favorites">
                            <ContactsList 
                                favorites={this.state.favorites}
                                user={this.props.user} />
                        </Tab>
                        <Tab heading="Add Contact">
                            <AddContact 
                                user={this.props.user}/>
                        </Tab>
            </Tabs>
            </Content>
            
            
            </Container>
            );
        }
        }