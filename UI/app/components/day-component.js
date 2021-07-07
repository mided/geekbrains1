import Component from '@ember/component';
import { inject as service } from '@ember/service';
import Ember from 'ember';

export default Component.extend({
  logicService: service('basic-logic-service'),
  date: new Date(),
  deeds: [],
  weekData: [],

  async didReceiveAttrs() {
    this._super(...arguments);
    let logicService = this.logicService;
    Ember.set(this, 'deeds', logicService.getDeedsForDate(this.date));
  },

  actions: {
    addDeed: function () {
      let logicService = this.logicService;
      logicService.mainComponent.subwindowShow({ date: this.date });
    },
  },
});
