Feature: GetLocation

Scenario: Check the current url is as expected
	Given I have navigated to "/newpage.html"
	Then the current url is "/newpage.html"

Scenario: Check the current url is as expected after navigating a couple of pages
	Given I have navigated to "/newpage.html"
	Then the current url is "/newpage.html"
	When I have navigated to "/samplepage.html"
	And I have navigated to "/samplepage.html"
	Then the current url is "/samplepage.html"


Scenario: Check the current url is as expected after navigating 
	Given I have navigated to "/newpage.html"
	When I have navigated to "/samplepage.html"
	And I have navigated to "/newpage.html"
	Then the current url is "/newpage.html"
