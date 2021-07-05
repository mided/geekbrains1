export function getWeekStart() {
	let curr = new Date;
	let first = curr.getDate() - curr.getDay() + 1;
	return new Date(curr.setDate(first));
}

export function getWeekEnd() {
	let curr = new Date;
	let first = curr.getDate() - curr.getDay();	
	let last = first + 6;
	return new Date(curr.setDate(last));
}

export function dayOfWeek(date) {
	var days = ['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'];
	return days[date.getDay() ];
}