import Component from '@ember/component';
import Ember from 'ember';
import { inject as service } from '@ember/service';
import { getWeekStart } from '../helpers/dateTimeFunctions';
import DayEntity from '../entities/dayEntity';

export default Component.extend({
  logicService: service('basic-logic-service'),
  weekDays: [],
  weekData: [],

  async didInsertElement() {
    this._super(...arguments);

    this.weekDays.slice(0, this.weekDays.length);

    let monday = getWeekStart();
    for (let i = 0; i < 7; i++) {
      let day = new DayEntity();
      day.day = new Date();
      day.day.setDate(monday.getDate() + i);
      day.day.setUTCHours(0, 0, 0, 0);
      this.weekDays.push(day);
      Ember.set(this, 'weekDays', this.weekDays.slice(0));
    }
  },
});
