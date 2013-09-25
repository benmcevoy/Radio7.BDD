Feature: WaitForExpectedConditions
	In order to test the ExcepectedConditionsExtensions
	Complete a few scenarios that exercise them

Scenario: TextToBePresentInElement
	Given I have navigated to "/SamplePage.html"
	And I click the element with id "insertTextInElementAfterDelayButton"
	When I wait for the value "test value" to be present in element with id "textElement"
	Then field with id "textElement" has value "test value"
