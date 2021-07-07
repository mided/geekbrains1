import Component from '@ember/component';
import Ember from 'ember';
import { inject as service } from '@ember/service';

export default Component.extend({
  logicService: service('basic-logic-service'),
  weekData: null,
  showSubwindow: false,

  actions: {
    close: function () {
      this.logicService.mainComponent.subwindowHide();
    },
  },
});
