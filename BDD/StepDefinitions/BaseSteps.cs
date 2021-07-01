using System;
using BoDi;
using OpenQA.Selenium;

namespace JupiterToys.BDD.StepDefinitions {
    public abstract class BaseSteps {
        protected readonly IWebDriver driver;
        protected IObjectContainer objectContainer;

        protected BaseSteps(IObjectContainer objectContainer) {
            this.driver = objectContainer.Resolve<IWebDriver>();
            this.objectContainer = objectContainer;
        }
    }
}


