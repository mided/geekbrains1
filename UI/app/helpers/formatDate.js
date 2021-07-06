import { helper } from '@ember/component/helper';

function fromatDate(date) {
  if (typeof date[0] == 'string') {
    return date[0].split('T')[0];
  }

  let ye = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(date[0]);
  let mo = new Intl.DateTimeFormat('en', { month: 'short' }).format(date[0]);
  let da = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(date[0]);
  return `${da}-${mo}-${ye}`;
}

export default helper(fromatDate);
