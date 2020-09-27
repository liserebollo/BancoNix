import angular from 'angular';
import template from './home.html';
import customTable from '../../components/custom-table/custom-table.component';

export default angular
  .module('home.view', [customTable])
  .component('home', {
    template,
  })
  .name;
