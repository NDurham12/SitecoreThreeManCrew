using System;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SitecoreThreeManCrew.Web.Tests
{
    [TestClass]
    public class UnitTestCultureInfo
    {
        [TestMethod]
        public void SerializeCultureInfoTestMethod()
        {
            var cultureInfo = new CultureInfo("en-US");
            var output = SerializeLogParameterLess(cultureInfo);

            System.Diagnostics.Trace.WriteLine(output);

            Assert.IsFalse(string.IsNullOrEmpty(output), "String should have Serialize");
            
        }

        public static string SerializeLogParameterLess(object objectToSave)
        {
            var objType = objectToSave.GetType();
            var xDoc = new XElement(objType.Name);
            try
            {
                var props = objType.GetProperties();

                foreach (var propertyInfo in props)
                {
                    try
                    {
                        xDoc.Add(new XElement(propertyInfo.Name, propertyInfo.GetValue(objectToSave, null).ToString()));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {

            }
            return xDoc.ToString();
        }
    }
}
