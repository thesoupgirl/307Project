// __tests__/Intro-test.js
import 'react-native';
import React from 'react';
import AddActivity from '../components/addActivity.js';

// Note: test renderer must be required after react-native.
import renderer from 'react-test-renderer';

test('renders AddActivities correctly', () => {
  const tree = renderer.create(
    <AddActivity />
  ).toJSON();
  expect(tree).toMatchSnapshot();
});
