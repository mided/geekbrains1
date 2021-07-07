import Component from '@ember/component';
import { inject as service } from '@ember/service';
import Ember from 'ember';

export default Component.extend({
  logicService: service('basic-logic-service'),
  register: false,
  loginName: '',
  
  async didInsertElement() {
    this._super(...arguments);
	Ember.set(this, 'register', false);
  },

  async login() {
	let user = await this.logicService.login(this.loginName);
	if (user === undefined)
	{
		alert('User not found');
	} else
	{
		this.initAfterLogin();
	}
  },

  async registerUser() {
	await this.logicService.register(this.loginName);
	await this.login();
  },

  actions: {
    toRegister: function () {
      Ember.set(this, 'register', true);
    },
	toLogin: function () {
		Ember.set(this, 'register', false);
	},
	register: function () {
		this.registerUser();
	},
	login: function () {
		this.login();
	},
  },
});
