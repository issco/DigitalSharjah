using MRHE.Helpers;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace MRHE.Hooks
{
    [Binding]
    public class EnvironmentHooks
    {
        public static string env = "web";

        [BeforeFeature(Order = -int.MaxValue)]

  
        public static void SetFeatureEnvironment(FeatureContext featureContext)
        {
            var stage = Stage.Unknown;

        var tags = featureContext.FeatureInfo.Tags;
          
            if (tags.Contains("Test")) stage = Stage.Test;
            else if (tags.Contains("Staging")) stage = Stage.Staging;
            else if (tags.Contains("Production")) stage = Stage.Production;
        
            EnvironmentHelper.SetStage(stage);

        }

        [BeforeScenario(Order = -int.MaxValue)]
        public static void SetScenarioEnvironment(ScenarioContext scenarioContext)
        {
            var stage = Stage.Unknown;
            var tags = scenarioContext.ScenarioInfo.Tags;
            if (tags.Contains("Test")) stage = Stage.Test;
            else if (tags.Contains("Staging")) stage = Stage.Staging;
            else if (tags.Contains("Production")) stage = Stage.Production;

            if (EnvironmentHelper.CurrentStage == Stage.Unknown)
            {
                  EnvironmentHelper.SetStage(stage);
            }
            }

        [AfterScenario(Order = -1)]
        public static void ScreenshotError(ScenarioContext scenarioContext)
        {
 
                ScreenshotManager.TakeScreenshot(DriverManager.WebDriver, ScreenshotType.Error);
            
        }

        [AfterScenario(Order = 1000)]
        public static void CleanDriver(ScenarioContext scenarioContext)
        {
            DriverManager.Cleanup();
        }

    }
}
