import Service from '@ember/service';
import Endpoints from '../models/endpoints';
import UserModel from '../models/userModel';
import { inject as service } from '@ember/service';

export default Service.extend({
  communicationService: service('communication-service'),

  init() {
    this._super(...arguments);
  },

  async login(username) {
	  var usermodel = new UserModel();
	  usermodel.name = username;

	  let communicationService = this.get('communicationService');

	  await communicationService.get(Endpoints.getUser, usermodel);
  },
});
