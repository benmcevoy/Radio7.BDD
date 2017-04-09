using System;
using System.Linq;
using Radio7.BDD.Config;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.UnitTestProvider;

namespace Radio7.BDD.Filters
{
    [Binding]
    public class ExcludeScenarioTags
    {
        private readonly ISeleniumConfig _config;

        public ExcludeScenarioTags(ISeleniumConfig config)
        {
            _config = config;
        }

        [BeforeScenario]
        public void Filter()
        {
            var tags = _config.ExcludeScenarioTags;

            if (string.IsNullOrWhiteSpace(tags)) return;

            var tagsToExclude = tags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var tag in tagsToExclude)
            {
                if (!ScenarioContext.Current.ScenarioInfo.Tags.Contains(tag)) continue;

                IgnoreTest();
                return;
            }
        }

        private static void IgnoreTest()
        {
            var unitTestRuntimeProvider =
                (IUnitTestRuntimeProvider)ScenarioContext.Current.GetBindingInstance(typeof(IUnitTestRuntimeProvider));

            unitTestRuntimeProvider.TestIgnore(
                $"Scenario '{ScenarioContext.Current.ScenarioInfo.Title}' was tagged with an exclude tag and is being skipped.");
        }
    }
}
