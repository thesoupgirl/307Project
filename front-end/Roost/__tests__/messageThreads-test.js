// __tests__/Intro-test.js
import 'react-native';
import React from 'react';
import MessageThreads from '../components/messageThreads.js';

// Note: test renderer must be required after react-native.
import renderer from 'react-test-renderer';

test('renders messageThreads correctly', () => {
  const tree = renderer.create(
    <MessageThreads />
  ).toJSON();
  expect(tree).toMatchSnapshot();
});
