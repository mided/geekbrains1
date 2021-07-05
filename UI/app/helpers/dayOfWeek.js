import { helper } from '@ember/component/helper';
import { dayOfWeek as dayOfWeekFunc } from '../helpers/dateTimeFunctions';

function dayOfWeek(date) {
	return dayOfWeekFunc(date[0]);
}

export default helper(dayOfWeek);