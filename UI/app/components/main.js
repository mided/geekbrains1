import Component from '@ember/component';
import Ember from 'ember';
import { inject as service } from '@ember/service';

export default Component.extend({
  logicService: service('basic-logic-service'),
  weekData: null,

  async didInsertElement() {
    this._super(...arguments);
    let communicationService = this.logicService;

    await communicationService.login('Denis');

	let weekDataResponse = await this.logicService.getWeekData();
	Ember.set(this, 'weekData', weekDataResponse);	
  },
});
