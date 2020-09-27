import angular from 'angular';
import template from './navigation.html';
import './navigation.scss';

export default angular
  .module('navigation.component', [])
  .component('navigation', {
    template,
  })
  .name;
