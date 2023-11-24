using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSeleniumAutomationTesting.PageObjectModels.Commons
{
    /// <summary>
    ///     Interface supports for creating a new <see cref="Actions"/> instance.
    /// </summary>
    public interface IActionsCreator
    {
        /// <summary>
        ///     Create new <see cref="Actions"/> to interact.
        /// </summary>
        /// <returns>
        ///     The <see cref="Actions"/> instance that used for interaction.
        /// </returns>
        Actions CreateActions();
    }
}
