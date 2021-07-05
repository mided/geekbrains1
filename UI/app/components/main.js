import Component from '@ember/component';
import Ember from 'ember';
import { inject as service } from '@ember/service';

export default Component.extend({
	logicService: service('basic-logic-service'),

    async didInsertElement() {        
		let communicationService = this.get('logicService');

		await communicationService.login('Denis');

		//await communicationService.get('user', {name: 'Denis'});

		//await communicationService.post('user/register', {name: 'Мэрилин Мэнсон'});
    },
});
