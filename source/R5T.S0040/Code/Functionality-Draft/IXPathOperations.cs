using System;
using System.Xml.Linq;
using System.Xml.XPath;

using R5T.Magyar;

using R5T.T0132;


namespace R5T.S0040
{
    /// <summary>
    /// Basic XML XPath operations.
    /// </summary>
    [DraftFunctionalityMarker]
    public interface IXPathOperations : IDraftFunctionalityMarker
    {
        public XElement XPathGetElement(
            XNode xNode,
            string expression)
        {
            var hasElement = this.XPathHasElement(
                xNode,
                expression);

            if(!hasElement)
            {
                throw new Exception("XElement not found.");
            }

            return hasElement.Result;
        }

        public WasFound<XElement> XPathHasElement(
            XNode xNode,
            string expression)
        {
            var elementOrDefault = xNode.XPathSelectElement(expression);

            var output = WasFound.From(elementOrDefault);
            return output;
        }
    }
}
