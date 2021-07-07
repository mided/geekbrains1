import Component from '@ember/component';
import { inject as service } from '@ember/service';
import Ember from 'ember';

export default Component.extend({
  logicService: service('basic-logic-service'),
  execution: null,
  executed: false,
  deedId: 0,

  async didReceiveAttrs() {
    this._super(...arguments);
    Ember.set(this, 'executed', this.execution.executionDate != null);
  },

  executionChanged: Ember.observer('executed', function () {
    this.logicService.changeDeedExecution(
      this.deedId,
      this.execution.user.id,
      this.executed
    );
  }),
  actions: {
    deleteExecution: function () {
      this.logicService.deleteExecutioner(this.deedId, this.execution.user.id);
    },
  },
});
