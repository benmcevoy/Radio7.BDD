Feature: GetLocation

Scenario: Check the current url is as expected
	Given I navigate to "/newpage.html"
	Then the current url is "/newpage.html"

Scenario: Check the current url is as expected after navigating a couple of pages
	Given I navigate to "/newpage.html"
	Then the current url is "/newpage.html"
	When I navigate to "/samplepage.html"
	And I navigate to "/samplepage.html"
	Then the current url is "/samplepage.html"

Scenario: Check the current url is as expected after navigating 
	Given I navigate to "/newpage.html"
	When I navigate to "/samplepage.html"
	And I navigate to "/newpage.html"
	Then the current url is "/newpage.html"
	
Scenario: Check the absolute url is as expected after navigating 
	Given I navigate to "/newpage.html"
	Then the current url is "http://localhost:50523/newpage.html"
