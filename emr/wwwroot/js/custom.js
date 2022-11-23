function getCurrentDate() {
	var now = new Date();
	return formatDate(now);
}
function getDateTime(isSecond) {
	var now = new Date();
	return formatDateTime(now, isSecond);
}
function formatDate(date) {
	var year = date.getFullYear();
	var month = date.getMonth() + 1;
	var day = date.getDate();
	if (month.toString().length == 1) {
		var month = '0' + month;
	}
	if (day.toString().length == 1) {
		var day = '0' + day;
	}

	return month + '/' + day + '/' + year;
}

function formatDateTime(date, isSecond) {
	var hour = date.getHours();
	var minute = date.getMinutes();
	var second = date.getSeconds();

	if (hour.toString().length == 1) {
		var hour = '0' + hour;
	}
	if (minute.toString().length == 1) {
		var minute = '0' + minute;
	}
	if (second.toString().length == 1) {
		var second = '0' + second;
	}
	var dateTime = formatDate(date) + ' ' + hour + ':' + minute;
	if (typeof isSecond !== 'undefined' && isSecond) {
		dateTime += ":" + second;
	}
	return dateTime;
}
function parseDateTime(date) {
	var time = "00:00:00";
	if (date.indexOf(' ') !== -1) {
		var t1 = date.split(" ");
		date = t1[0];
		time = t1[1];
	}
	var parts = date.split("/");
	var month = parts[0];
	var day = parts[1];
	var year = parts[2];
	
	parts = time.split (":");
	
	var hour = parseInt(parts[0]);
	var min  = parseInt(parts[1]);
	var sec  = 0;
	if (parts.length == 3){
		sec = parseInt(parts[2]);
	}

	var now = new Date(year, month - 1, day, hour, min, sec);
	return now;
}
function isIframe() {
	try {
		return window.self !== window.top;
	} catch (e) {
		return false;
	}
}
