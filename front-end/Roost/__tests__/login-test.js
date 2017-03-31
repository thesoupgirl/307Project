import 'react-native'
import React from 'react';
import {shallow} from 'enzyme';
import Login from '../components/login.js';
import renderer from 'react-test-renderer'

// Note: test renderer must be required after react-native.
// import renderer from 'react-test-renderer';

// //tests to see if navigation renders correctly
test('Renders Login correctly', () => {
  const tree = renderer.create(
      <Login/>
      ).toJSON();
  expect(tree).toMatchSnapshot();
});
