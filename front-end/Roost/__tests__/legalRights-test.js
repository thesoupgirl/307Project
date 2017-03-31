// __tests__/Intro-test.js
import 'react-native';
import React from 'react';
import Profile from '../components/legalRights.js';

// Note: test renderer must be required after react-native.
import renderer from 'react-test-renderer';

test('renders LegalRights correctly', () => {
  const tree = renderer.create(
    <Profile />
  ).toJSON();
  expect(tree).toMatchSnapshot();
});
