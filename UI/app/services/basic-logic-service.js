import Service from '@ember/service';
import Endpoints from '../models/endpoints';
import GetUserRequest from '../requests/getUserRequest';
import GetDeedsRequest from '../requests/getDeedsRequest';
import { inject as service } from '@ember/service';
import { getWeekStart, getWeekEnd } from '../helpers/dateTimeFunctions';
import Ember from 'ember';

export default Service.extend({
  communicationService: service('communication-service'),
  currentUser: null,
  weekData: [],

  init() {
    this._super(...arguments);
  },

  async login(username) {
    let request = new GetUserRequest();
    request.name = username;

    let communicationService = this.communicationService;

    let response = await communicationService.get(
      Endpoints.getUser,
      request
    );

	this.currentUser = response[0];	
  },

  async getWeekData() {
	let communicationService = this.communicationService;

	let request = new GetDeedsRequest();
	request.userId = this.currentUser.id;
	request.from = getWeekStart();
	request.to = getWeekEnd();

	let response = await communicationService.get(
		Endpoints.getDeeds,
		request
	);

	Ember.set(this, 'weekData', response);	
	return this.weekData;
  },

  getDeedsForDate(date) {
	let formattedDate = date.toISOString().split('T')[0];	
	return this.weekData.filter(deed => deed.executions.some(e => e.plannedDate.split('T')[0] == formattedDate));
  }
});
