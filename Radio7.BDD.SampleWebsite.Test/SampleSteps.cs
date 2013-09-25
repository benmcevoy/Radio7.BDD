using System;
using TechTalk.SpecFlow;

namespace Radio7.BDD.SampleWebsite.Test
{
    [Binding]
    public class SampleSteps 
    {
        private readonly SamplePage _samplePage;
        private readonly CommonSteps _commonSteps;

        public SampleSteps(SamplePage samplePage, CommonSteps commonSteps)
        {
            _samplePage = samplePage;
            _commonSteps = commonSteps;
            _samplePage.NavigateTo();
        }

        [Given(@"I wait for the value ""(.*)"" to be present in element with id ""(.*)"" to timeout")]
        public void GivenIWaitForTheValueToBePresentInElementWithIdToTimeout(string text, string id)
        {
            try
            {
                _commonSteps.GivenIWaitForValueToBePresentInElementWithId(text, id);
            }
            catch (Exception ex)
            {
                // TODO: promote to infrastructure
                ScenarioContext.Current["LastExceptionTypeName"] = ex.GetType().Name;
            }
        }
    }
}
