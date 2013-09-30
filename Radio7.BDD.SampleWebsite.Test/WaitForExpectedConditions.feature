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
	And I click the element labelled "insert text in element after long delay"
	And I wait for the value "test value" to be present in element with id "textElement" to timeout
	Then the expected exception is of type "WebDriverTimeoutException"

Scenario: Wait for AlertIsPresent
	Given I have navigated to "/SamplePage.html"
	And I click the element labelled "raise alert after delay"
	When I wait for an alert to be displayed
	Then an alert is displayed

Scenario: Wait for ElementContainsTextIsInvisible by setting visibilty hidden
	Given I have navigated to "/SamplePage.html"
	And I click the element with id "setTextAndVisibilityHiddenButton"
	And I wait for the value "disappearing soon" to be present in element with id "textElement"
	When I wait for element with id "textElement" and value "disappearing soon" to be invisible
	Then element with id "textElement" is invisible

Scenario: Wait for ElementContainsTextIsInvisible by setting display none
	Given I have navigated to "/SamplePage.html"
	And I click the element with id "setTextAndDisplayNoneButton"
	And I wait for the value "disappearing soon" to be present in element with id "textElement"
	When I wait for element with id "textElement" and value "disappearing soon" to be invisible
	Then element with id "textElement" is invisible

Scenario: Wait for ElementContainsTextIsInvisible by removing from dom
	Given I have navigated to "/SamplePage.html"
	And I click the element with id "setTextAndRemoveButton"
	And I wait for the value "disappearing soon" to be present in element with id "textElement"
	When I wait for element with id "textElement" and value "disappearing soon" to be invisible
	Then element with id "textElement" is invisible

Scenario: Wait for ElementIsInvisible by setting visibilty hidden
	Given I have navigated to "/SamplePage.html"
	And I click the element with id "setTextAndVisibilityHiddenButton"
	When I wait for element with id "textElement" to be invisible
	Then element with id "textElement" is invisible

Scenario: Timeout for ElementIsInvisible by setting visibilty hidden
	Given I have navigated to "/SamplePage.html"
	And I click the element with id "setVisibilityHiddenAfterLongDelayButton"
	When I wait for element with id "textElement" to be invisible to timeout
	Then the expected exception is of type "WebDriverTimeoutException"
