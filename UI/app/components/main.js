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
    logicService.mainComponent = this;
  },

  async initAfterLogin() {
	let component = this.logicService.mainComponent;
	await this.logicService.getUsers();
	Ember.set(component, 'currentUser', this.logicService.currentUser.name);	
	await component.reload();
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
