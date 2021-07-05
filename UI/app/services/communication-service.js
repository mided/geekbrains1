import Service from '@ember/service';
import AppSettings from '../settings/appsettings';

export default Service.extend({
  init() {
    this._super(...arguments);
  },

  fromatDate(date) {
	let ye = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(date);
	let mo = new Intl.DateTimeFormat('en', { month: 'short' }).format(date);
	let da = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(date);
	return `${da}-${mo}-${ye}`
  },


  async get(route, params) {
    let queryString =
      params == null
        ? ''
        : '?' +
          Object.keys(params)
            .map((key) => key + '=' + (typeof params[key].getMonth === 'function' ? this.fromatDate(params[key]) : params[key]))
            .join('&');

    let response = await fetch(
      encodeURI(AppSettings.api + '/' + route + queryString)
    );

    let data = await response.json();
    if (AppSettings.consoleLog) {
      console.log(data, 'response ' + route);
    }
    return data;
  },

  async post(route, request) {
	
	if (AppSettings.consoleLog) {
		console.log(request, 'request ' + route);
	}

    let response = await fetch(encodeURI(AppSettings.api + '/' + route), {
      headers: { 'Content-Type': 'application/json; charset=utf-8' },
      method: 'POST',
      body: JSON.stringify(request),
    });

    let data = await response.json();

    if (AppSettings.consoleLog) {
      console.log(data, 'response ' + route);
    }
    return data;
  },
});
