# README #

To get this project up and running, you just need to clone the repo and run it in local debug mode.  I haven't set up IIS for convenience of getting it going.


### What to expect ###

I've created a simple journey info search.  

On the first page, it's a simple search form for a journey.  In the background you will notice a map, this is coded to lookup your location and display a static image from google maps.  It also includes autocomplete functions for the origin and destination points which have been restricted to the UK.  The reason for this restriction is the distance and travel time api tends to fail if the origin and destination points are in two different countries.

On the second page, you should see two sections.  The first is the request for travel distance and time information.  The second is a search for nearby attractions that should appear as a carousel (although I have just borrowed this from the standard template).  This second section may not appear if you go over the query limit for the API - it will just be a blank space.  In the background you will also notice that the map corresponds to the searched for destination.  In the event of failure to retrieve time and distance info you should get a search box again to try again with.

I have done some basic CSS work using bootstrap, so it should adjust to work with different screen sizes.  However I haven't put a huge amount of time into it as it's basically a proof of concept.


### Code ###

In the back end, I have chosen to use some of my own conventions.  I appreciate this may not be the same standards as your business but I would of course adhere to your standards and conventions.

This is a standard .Net Core Web Application setup with the standard template.  I have used the out of the box dependency injection.


### Interfaces and Classes ###

I have chosen to name interfaces, not by copying the class name with an I in front, but with an IDoThis naming convention.  The reason for this is I believe it makes the SOLID Interface Segregation Principle easier to follow and it mentally decouples the interface contract from the implementation.

Using the google API is a great example of why I do this.  For example if I want to get some travel stats the interface is named ISearchForTravelStats, as that will be the function and contract.  However, the implementation is specifically using Google and so is named GoogleTravelStatsSearcher.  This means there is space for a Bing version or Apple Maps version to use the same interface.  Therefore, when implementing circumstances such as factory based design patterns, the interface can clearly define the contract of any object returned from that factory rather than being implementation specific.


### Unit Testing ###

This project uses test wrappers and test classes with Microsoft Testing Framework.  Although it is possible to do setup of tests within the test class itself, I often find for practicality a test wrapper can be very useful as I can have the setup code in one window and the tests next to it in another window.

In the wrapper, I setup data for the test in the constructor, mocks in the GetClassUnderTest method and then if these properties are made available to the test I can adjust them as required.  The aim of the test wrapper is to create the ideal 'happy path' conditions for the implementation class, individual tests can adjust these to test specific circumstances.

In tests, by changing properties after calling the wrapper constructor, I can adjust what the mocks will respond with.  Although reference types don't require this, it is much simpler if your mock returns a primitive type.  Mock can also be adjusted by changing the setup after the GetClassUnderTest method is called.  This way of setting up tests has allowed me a great deal of flexibility in setting up different circumstances in a simple framework.


### ApiClient ###

The ApiClient has been written to make interfacing with the Google API simpler.  In other projects, I would probably have broken this out and turned it into a Nuget package.  I have created Data Transfer Objects to attempt to match the JSON structure that Google sends back results in, I appreciate I may not have covered all bases here, but I was only using a fraction of the data.

I have created a generic base for calling the API.  This is a class I use in other projects, which means the inherited API calling implementation can be made quite simple and simply pass an URL and the parameters to the base class to make the call.  It can be extended to include other types of call.  

The interface can then be made available to the application and implemented with dependency injection, allowing a simple client to be created for accessing the API.

Mapping from DTOs to the Model has been achieved with AutoMapper.  I tend to tend individual mappings in Application tests, although this can be a belt and braces approach, as I have also included the general AutoMapper config verification test.