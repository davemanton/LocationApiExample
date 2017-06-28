if (navigator.geolocation) {
	navigator.geolocation.getCurrentPosition(updateMap);
} else {
	updateMap();
}

function updateMap(position) {

	var latlon = position != null ? position.coords.latitude + "," + position.coords.longitude : "53.480506,-2.23687";

	var imgUrl = "https://maps.googleapis.com/maps/api/staticmap?center=" + latlon + "&zoom=14&size=1920x1080&sensor=false&key=AIzaSyD2lln26f5wLuV-SEGaIG6G2kRykEQ5gEU";

	$(document).ready(function () {
		$('#background').css('background-image', "url('" + imgUrl + "')");
		$('#background').css('background-size', "cover");
	});
}