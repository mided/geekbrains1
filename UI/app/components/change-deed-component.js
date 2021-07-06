import Component from '@ember/component';
import { inject as service } from '@ember/service';
import Ember from 'ember';
import { guidFor } from '@ember/object/internals';

export default Component.extend({
  logicService: service('basic-logic-service'),
  deed: null,
  users: [],
  guid: null,
  selectedUser: null,
  validationErrors: true,
  description: null,
  date: null,

  didInsertElement() {
	this._super(...arguments);
	this.guid = guidFor(this);

	Ember.set(this, 'users', this.logicService.users);

	let component = this;
	this.logicService.subscribeOnUsers(this.guid, () => {
		Ember.set(component, 'users', component.logicService.users);
	});
	
	Ember.set(this, 'deed', this.logicService.mainComponent.subwindowData?.deed);
	Ember.set(this, 'date', this.logicService.mainComponent.subwindowData?.date);

	Ember.set(this, 'description', this.deed?.description);
	
	this.validate();
  },

  didDestroyElement() {
	this.logicService.unsubscribeFromUsers(this.guid);
  },

  validate() {
	Ember.set(this, 'validationErrors', this.description == null || this.description == '' || this.selectedUser == null || this.date == null);
  },

  dateObserver: Ember.observer('date', function () {this.validate()}),
  descriptionObserver: Ember.observer('description', function () {this.validate()}),

  addDeed() {
	let deed = 
	{
		description: this.description,
		executions: [{executionerId: this.selectedUser, plannedDate: this.date}]
	}

	this.logicService.addDeed(deed);
  },

  addExecutioner() {
	let execution = 
	{
		deedId: this.deed.id,
		execution: {executionerId: this.selectedUser, plannedDate: this.date}
	}

	this.logicService.addExecutioner(execution);
  },

  actions: {
	selectUser: function(userId) {
		this.selectedUser = userId;
		this.validate();
	},

	save: function() {
		console.log(this.deed, 'this.deed');
		if (this.deed == null)
		{
			this.addDeed();
		} else
		{
			this.addExecutioner();
		}
	},
  },
});
