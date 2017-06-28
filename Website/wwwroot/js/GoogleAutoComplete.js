var options = {
	componentRestrictions: {
		country: 'gb'
	}
}

var originInput = document.getElementById("origin-input");
var autocomplete = new google.maps.places.Autocomplete(originInput, options);

var destinationInput = document.getElementById("destination-input");
var autocomplete = new google.maps.places.Autocomplete(destinationInput, options);
