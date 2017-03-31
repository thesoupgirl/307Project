// __tests__/Intro-test.js
import 'react-native';
import React from 'react';
import Launch from '../components/launch.js';

// Note: test renderer must be required after react-native.
import renderer from 'react-test-renderer';

test('renders Launch correctly', () => {
  const tree = renderer.create(
    <Launch />
  ).toJSON();
  expect(tree).toMatchSnapshot();
});
