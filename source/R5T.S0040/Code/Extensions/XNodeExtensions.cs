using System;
using System.Xml.Linq;

using R5T.Magyar;

using Instances = R5T.S0040.Instances;


namespace System.Xml.XPath
{
    public static class XNodeExtensions
    {
        public static XElement XPathGetElement(this XNode xNode,
            string expression)
        {
            var output = Instances.XPathOperations.XPathGetElement(
                xNode,
                expression);

            return output;
        }

        public static WasFound<XElement> XPathHasElement(this XNode xNode,
            string expression)
        {
            var output = Instances.XPathOperations.XPathHasElement(
                xNode,
                expression);

            return output;
        }
    }
}
