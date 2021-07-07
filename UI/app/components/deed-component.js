import Component from '@ember/component';
import { inject as service } from '@ember/service';
import Ember from 'ember';

export default Component.extend({
  logicService: service('basic-logic-service'),
  date: new Date(),
  deed: null,

  async didReceiveAttrs() {
    this._super(...arguments);
    let logicService = this.logicService;
  },

  actions: {
    addExecution: function () {
      this.logicService.mainComponent.subwindowShow({
        deed: this.deed,
        date: this.date,
      });
    },

    deleteDeed: function () {
      this.logicService.deleteDeed(this.deed.id);
    },
  },
});
