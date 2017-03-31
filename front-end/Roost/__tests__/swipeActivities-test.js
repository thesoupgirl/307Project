// __tests__/Intro-test.js
import 'react-native';
import React from 'react';
import SwipeActivities from '../components/swipeActivities.js';

// Note: test renderer must be required after react-native.
import renderer from 'react-test-renderer';

test('renders SwipeActivities correctly', () => {
  const tree = renderer.create(
    <SwipeActivities />
  ).toJSON();
  expect(tree).toMatchSnapshot();
});
