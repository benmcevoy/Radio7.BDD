Feature: WaitForExpectedConditions
	In order to test the ExcepectedConditionsExtensions
	Complete a few scenarios that exercise them

Scenario: Wait for TextToBePresentInElement
	Given I have navigated to "/SamplePage.html"
	And I click the element with id "insertTextInElementAfterDelayButton"
	Then I wait for the value "test value" to be present in element with id "textElement"
	And the field with id "textElement" has value "test value"

Scenario: Timeout waiting for TextToBePresentInElement
	Given I have navigated to "/SamplePage.html"
	And I click the element with label "insert text in element after long delay"
	And I wait for the value "test value" to be present in element with id "textElement" to timeout
	Then the expected exception is of type "WebDriverTimeoutException"
