using System;
using System.Diagnostics;
using System.Linq;
using Radio7.BDD.Config;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.UnitTestProvider;

namespace Radio7.BDD.Filters
{
    [Binding]
    public class IncludeScenarioTags
    {
        private readonly ISeleniumConfig _config;

        public IncludeScenarioTags(ISeleniumConfig config)
        {
            _config = config;
        }

        [BeforeScenario]
        public void Filter()
        {
            var tags = _config.IncludeScenarioTags;

            if (string.IsNullOrWhiteSpace(tags)) return;

            var tagsToInclude = tags.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var tag in tagsToInclude)
            {
                if (!ScenarioContext.Current.ScenarioInfo.Tags.Contains(tag)) continue;

                Trace.Write($"Scenario '{ScenarioContext.Current.ScenarioInfo.Title}' tagged with '{tag}' is being included.");
                return;
            }

            IgnoreTest();
        }

        private static void IgnoreTest()
        {
            var unitTestRuntimeProvider =
                (IUnitTestRuntimeProvider) ScenarioContext.Current.GetBindingInstance(typeof(IUnitTestRuntimeProvider));

            unitTestRuntimeProvider.TestIgnore(
                $"Scenario '{ScenarioContext.Current.ScenarioInfo.Title}' was not tagged with an include tag and is being skipped.");
        }
    }
}
