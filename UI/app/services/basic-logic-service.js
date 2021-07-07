import Service from '@ember/service';
import Endpoints from '../models/endpoints';
import GetUserRequest from '../requests/getUserRequest';
import GetDeedsRequest from '../requests/getDeedsRequest';
import CompleteDeedRequest from '../requests/completeDeedRequest';
import UncompleteDeedRequest from '../requests/uncompleteDeedRequest';
import AddDeedRequest from '../requests/addDeedRequest';
import AddExecutionerRequest from '../requests/addExecutionerRequest';
import DeleteDeedRequest from '../requests/deleteDeedRequest';
import RegisterUserRequest from '../requests/registerUserRequest';
import { inject as service } from '@ember/service';
import { getWeekStart, getWeekEnd } from '../helpers/dateTimeFunctions';
import Ember from 'ember';
import DeleteExecutionerRequest from '../requests/deleteExecutionerRequest';

export default Service.extend({
  communicationService: service('communication-service'),
  currentUser: null,
  weekData: [],
  users: [],
  userSubsctiptions: [],
  mainComponent: null,

  init() {
    this._super(...arguments);
  },

  usersChanged: Ember.observer('users', function () {
    this.userSubsctiptions.forEach((s) => s.callback());
  }),

  subscribeOnUsers(id, callback) {
    let subscription = { id, callback };
    subscription.id = id;
    subscription.callback = callback;
    this.userSubsctiptions.push(subscription);
  },

  unsubscribeFromUsers(id) {
    let subscription = this.userSubsctiptions.find((s) => s.id == id);
    if (subscription != null) {
      this.userSubsctiptions.remove(subscription);
    }
  },

  async login(username) {
    let request = new GetUserRequest();
    request.name = username;

    let communicationService = this.communicationService;

    let response = await communicationService.get(Endpoints.getUser, request);

    this.currentUser = response[0];

    return this.currentUser;
  },

  async register(username) {
    let request = new RegisterUserRequest();
    request.name = username;

    let communicationService = this.communicationService;

    let response = await communicationService.post(Endpoints.registerUser, null, request);

    this.currentUser = response[0];

    return this.currentUser;
  },

  async getUsers() {
    let communicationService = this.communicationService;
    let request = new GetUserRequest();

    let response = await communicationService.get(Endpoints.getUser, request);

    Ember.set(this, 'users', response);
    return this.users;
  },

  async getWeekData() {
    let communicationService = this.communicationService;

    let request = new GetDeedsRequest();
    request.userId = this.currentUser.id;
    request.from = getWeekStart();
    request.to = getWeekEnd();

    let response = await communicationService.get(Endpoints.getDeeds, request);

    Ember.set(this, 'weekData', response);
    return this.weekData;
  },

  getDeedsForDate(date) {
    let formattedDate = date.toISOString().split('T')[0];
    let res = this.weekData.filter((deed) =>
      deed.executions.some(
        (e) =>
          e.plannedDate.split('T')[0] == formattedDate &&
          this.currentUser.id == e.user.id
      )
    );
    return res;
  },

  async changeDeedExecution(deedId, userId, executed) {
    if (executed) {
      let request = new CompleteDeedRequest();
      request.userId = userId;
      request.deedId = deedId;

      await this.communicationService.post(Endpoints.completeDeed, request);
    } else {
      let request = new UncompleteDeedRequest();
      request.userId = userId;
      request.deedId = deedId;

      await this.communicationService.post(Endpoints.uncompleteDeed, request);
    }
  },

  async addDeed(deed) {
    let request = new AddDeedRequest();
    request.description = deed.description;
    request.executions = deed.executions;

    await this.communicationService.post(Endpoints.createDeed, null, request);

    this.mainComponent.reload();
    this.mainComponent.subwindowHide();
  },

  async addExecutioner(execution) {
    let request = new AddExecutionerRequest();
    request.deedId = execution.deedId;
    request.execution = execution.execution;
    await this.communicationService.post(
      Endpoints.addexecutioner,
      null,
      request
    );

    this.mainComponent.reload();
    this.mainComponent.subwindowHide();
  },

  async deleteDeed(deedId) {
    let request = new DeleteDeedRequest();
    request.deedId = deedId;

    await this.communicationService.post(Endpoints.deleteDeed, request, null);

    this.mainComponent.reload();
    this.mainComponent.subwindowHide();
  },

  async deleteExecutioner(deedId, userId) {
    let request = new DeleteExecutionerRequest();
    request.deedId = deedId;
    request.userId = userId;

    await this.communicationService.post(
      Endpoints.deleteExecutioner,
      request,
      null
    );

    this.mainComponent.reload();
    this.mainComponent.subwindowHide();
  },
});
