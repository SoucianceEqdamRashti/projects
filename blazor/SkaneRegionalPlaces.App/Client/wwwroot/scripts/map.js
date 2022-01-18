function showMap(latitude, longitude, name, location, address) {
   
    var map = new Microsoft.Maps.Map('#myMap', {
        credentials: "OtbwXYnYV7XKPaBebt4g~idtB0DhV7bS6HTA2kPeC5w~AouqeqjbqwsR1iskcwZwJMH9yC1TCHWNx3-C81txZepK2WF9A917jiIa8Kypctnz",
        center: new Microsoft.Maps.Location(latitude, longitude),
        zoom: 9
        
    });
    infobox = new Microsoft.Maps.Infobox(map.getCenter(), {
        visible: false
    });
    var center = map.getCenter();
    //Assign the infobox to a map instance.
    infobox.setMap(map);

    // Create custom Pushpin
    var pin = new Microsoft.Maps.Pushpin(center, {
        title: address,
        subTitle: location,
        text: name,
        
    });
    //Add a click event handler to the pushpin.
    Microsoft.Maps.Events.addHandler(pin, 'click', pushpinClicked);

    // Add the pushpin to the map
    map.entities.push(pin);
    
            
};
function pushpinClicked(e) {
    //Make sure the infobox has metadata to display.
    if (e.target.metadata) {
        //Set the infobox options with the metadata of the pushpin.
        infobox.setOptions({
            location: e.target.getLocation(),
            title: e.target.metadata.title,
            description: e.target.metadata.description,
            visible: true
        });
    }
}