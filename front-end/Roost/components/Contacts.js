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
  constructor(threadsHandler, id, hideNav, showNav, user) {
        super()
            this.state = {
                favorites: []
            }  
            this.update = this.update.bind(this)
        }

        update () {
            this.componentWillMount()
        }



        componentWillMount() {
            var userID = this.props.user.username
            var ws = `${path}/api/users/${userID}/favorites`
            var xhr = new XMLHttpRequest();
            xhr.open('GET', ws);
            xhr.onload = () => {
            if (xhr.status===200) {
                var json = JSON.parse(xhr.responseText);
                this.setState({favorites: json})
                
            } else {
                console.warn('failed to get a users favorites')

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
                                user={this.props.user}
                                update={this.update}/>
                        </Tab>
            </Tabs>
            </Content>
            
            
            </Container>
            );
        }
        }