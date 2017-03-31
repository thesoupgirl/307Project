// __tests__/Intro-test.js
import 'react-native';
import React from 'react';
import Nav from '../components/nav.js';

// Note: test renderer must be required after react-native.
import renderer from 'react-test-renderer';

test('renders Nav correctly', () => {
  const tree = renderer.create(
    <Nav />
  ).toJSON();
  expect(tree).toMatchSnapshot();
});
