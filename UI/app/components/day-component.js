import Component from '@ember/component';
import { inject as service } from '@ember/service';
import Ember from 'ember';

export default Component.extend({
  logicService: service('basic-logic-service'),
  date: new Date(),
  deeds: [],
  weekData: [],

  async didUpdateAttrs() {
    this._super(...arguments);
    let logicService = this.logicService;
	Ember.set(this, 'deeds', logicService.getDeedsForDate(this.date));	
  },
});
