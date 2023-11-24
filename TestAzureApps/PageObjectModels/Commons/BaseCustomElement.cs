using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace DemoSeleniumAutomationTesting.PageObjectModels.Commons
{
    public abstract class BaseCustomElement : BaseObjectModel, IWrapsElement
    {
        #region Attributes
        protected IWebElement _wrappedElement;
        #endregion

        #region Properties
        public IWebElement WrappedElement => _wrappedElement;
        #endregion

        #region Constructors
        public BaseCustomElement(IWebElement wrappedElement, IWebDriver webDriver) : base(webDriver)
        {
            this._wrappedElement = wrappedElement;
        }
        #endregion

        #region Public Methods
        public override IWebElement FindElement(By by)
        {
           return _wrappedElement.FindElement(by: by);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _wrappedElement.FindElements(by);
        }
        #endregion
    }
}
