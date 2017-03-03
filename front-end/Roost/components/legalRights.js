import React, { Component } from 'react';
import { Container, Content, Button, Text, Footer, 
Icon, FooterTab, Header, View, Left, Body, Title,
Right, DeckSwiper, Card, CardItem, Thumbnail, H1, Item,
Picker, Input, Label, Form, H2, H3} from 'native-base';
var styles = require('./styles'); 
import {
  AppRegistry,
  StyleSheet,
  Navigator,
  Image
} from 'react-native';



/*  
    
*/


export default class Profile extends Component {
  constructor() {
        super()
        this.state = {
           
        }
  }
  render () {
           return (
               <Content style={{padding: 20}}>
                    <H2>ROOST TERMS AND CONDITIONS</H2>


<H3>I)	Legal Rights Disclaimer</H3>

<Text>By registering in our application and using Roost's services, you hereby relinquish any rights to hold our team responsible for anything that may occur relating to the use of the application. Please be cautious and safe when you use the application and interact with other people.</Text>


<H3>II)	Acceptable Use of Roost</H3>

<Text>You must not use this application in any way that causes, or may cause, damage to the application or impairment of the availability or accessibility of or in any way which is unlawful, illegal, fraudulent or harmful, or in connection with any unlawful, illegal, fraudulent or harmful purpose or activity.

You must not use our application to copy, store, host, transmit, send, use, publish or distribute any material which consists of (or is linked to) any malicious computer software or illegal activity.

You must not conduct any systematic or automated data collection activities on or in relation to this application without Roost's express written consent.</Text>
<Text>This includes:</Text>
	<Text>a)	scraping</Text>
	<Text>b)	data mining</Text>
	<Text>c)	data extraction</Text>
	<Text>d)	data harvesting</Text>
	<Text>e)	'framing' (iframes)</Text>
	<Text>f)	Article 'Spinning'</Text>

<Text>You must not use this application or any part of it to transmit or send unsolicited commercial communications.

You must not use this application for any purposes related to marketing without the express written consent of our company.</Text>


<H3>III)	User Content</H3>

<Text>(In these terms and conditions, “your user content” is defined as material (including without limitation text, images, polls, and external links) that you submit to this application, for whatever purpose.)

You grant to a worldwide, irrevocable, non-exclusive, royalty-free license to use, reproduce, adapt, publish, translate and distribute your user content in any existing or future media. You also grant to the right to sub-license these rights, and the right to bring an action for infringement of these rights.

Your user content must not be illegal or unlawful, must not infringe any third party's legal rights, and must not be capable of giving rise to legal action whether against you or or a third party (in each case under any applicable law).

You must not submit any user content to the application that is or has ever been the subject of any threatened or actual legal proceedings or other similar complaint.

Rooost reserves the right to edit or remove any material submitted to this application, or stored on the servers of Roost, or hosted or published upon the application.

Roost's rights under these terms and conditions in relation to user content, does not undertake to monitor the submission of such content to, or the publication of such content on, this application.</Text>


<H3>IV)	Exceptions</H3>

<Text>Nothing in this application disclaimer will exclude or limit any warranty implied by law that it would be unlawful to exclude or limit; and nothing in this application disclaimer will exclude or limit the liability of Roost in respect of any:</Text>
	<Text>a)	death or personal injury caused by the negligence of or its agents, employees or shareholders/owners;</Text>
	<Text>b)	fraud or fraudulent misrepresentation on the part of ; or</Text>
	<Text>c)	matter which it would be illegal or unlawful for to exclude or limit, or to attempt or purport to exclude or limit, its liability.</Text>


<H3>V)	Breaches of these terms and conditions</H3>

<Text>Without prejudice to Roost's other rights under these terms and conditions, if you breach these terms and conditions in any way, may take such action as deems appropriate to deal with the breach, including suspending your access to the application, prohibiting you from accessing the application, blocking devices using your IP/MAC address from accessing the application, contacting your internet service provider to request that they block your access to the application and/or bringing court proceedings against you.</Text>


<H3>VI)	Variation</H3>

<Text>Roost may revise these terms and conditions from time-to-time. Revised terms and conditions will apply to the use of this application from the date of the publication of the revised terms and conditions on this application. Please check this page regularly to ensure you are familiar with the current version.</Text>


               </Content>
           )
       }
}
  