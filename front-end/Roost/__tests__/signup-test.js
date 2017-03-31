// __tests__/Intro-test.js
import 'react-native';
import React from 'react';
import Signup from '../components/signup.js';

// Note: test renderer must be required after react-native.
import renderer from 'react-test-renderer';

test('renders SignUp correctly', () => {
  const tree = renderer.create(
    <Signup />
  ).toJSON();
  expect(tree).toMatchSnapshot();
});
