import Service from '@ember/service';
import Ember from 'ember';
import AppSettings from '../settings/appsettings';

export default Service.extend({
  init() {
    this._super(...arguments);
  },

  async get(route, params) {
	let queryString = params == null ? '' : '?' + Object.keys(params).map(key => key + '=' + params[key]).join('&');
	
	let response = await fetch(encodeURI(AppSettings.api + '/' + route + queryString));
	
	let data = await response.json();
	if (AppSettings.consoleLog) { console.log(data); }
	return data;
  },

  async post(route, request) {
	let response = await fetch(encodeURI(AppSettings.api + '/' + route), {
		headers: { "Content-Type": "application/json; charset=utf-8" },
		method: 'POST',
		body: JSON.stringify(request)
	});
	
	let data = await response.json();
	if (AppSettings.consoleLog) { console.log(data); }
	return data;
  },
});
