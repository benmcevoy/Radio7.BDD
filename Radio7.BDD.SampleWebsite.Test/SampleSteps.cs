using System;
using TechTalk.SpecFlow;

namespace Radio7.BDD.SampleWebsite.Test
{
    [Binding]
    public class SampleSteps 
    {
        private readonly SamplePage _samplePage;

        public SampleSteps(SamplePage samplePage)
        {
            _samplePage = samplePage;
            _samplePage.NavigateTo();
        }

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            throw new NotImplementedException();
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            throw new NotImplementedException();
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            throw new NotImplementedException();
        }
    }
}
