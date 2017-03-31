// __tests__/Intro-test.js
import 'react-native';
import React from 'react';
import Categories from '../components/categories.js';

// Note: test renderer must be required after react-native.
import renderer from 'react-test-renderer';

test('renders Categories correctly', () => {
  const tree = renderer.create(
    <Categories />
  ).toJSON();
  expect(tree).toMatchSnapshot();
});
