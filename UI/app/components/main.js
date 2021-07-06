import Component from '@ember/component';
import Ember from 'ember';
import { inject as service } from '@ember/service';

export default Component.extend({
  logicService: service('basic-logic-service'),
  weekData: null,
  showSubwindow: false,
  subwindowData: null,
  currentUser: null,

  async didInsertElement() {
    this._super(...arguments);
    let logicService = this.logicService;

    let user = await logicService.login('Карлсон');
	Ember.set(this, 'currentUser', user.name);

	await this.logicService.getUsers();

	await this.reload();

	logicService.mainComponent = this;
  },

  async reload() {
    let weekDataResponse = await this.logicService.getWeekData();
    Ember.set(this, 'weekData', weekDataResponse);
  },

  subwindowShow(data) {
	  Ember.set(this, 'showSubwindow', true);
	  Ember.set(this, 'subwindowData', data);
  },
  
  subwindowHide() {
	Ember.set(this, 'showSubwindow', false);
	Ember.set(this, 'subwindowData', null);
  },

});
