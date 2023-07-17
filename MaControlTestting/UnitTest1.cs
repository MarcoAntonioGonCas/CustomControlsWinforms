using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using MaControllers.Helpers;

namespace MaControlTestting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            int numeros = ColorUtils.HtmlToArgb("#FFFFFF");
            string valorNuevo = ColorUtils.ArgbToHtml(numeros);



            Assert.AreEqual("#FFFFFF", valorNuevo);
        }
    }
}
