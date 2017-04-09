Feature: IncludeExcludeTags
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@includeTag
Scenario: This test should run
	Given I navigate to "/newpage.html"
	Then the current url is "/newpage.html"

@excludeTag
Scenario: This test should be ignored
	Given I navigate to "/newpage.html"
	Then the current url is "/newpage.html"

@includeTag @excludeTag
Scenario: This test should be maybe run?
	#A: it got excluded
	Given I navigate to "/newpage.html"
	Then the current url is "/newpage.html"
