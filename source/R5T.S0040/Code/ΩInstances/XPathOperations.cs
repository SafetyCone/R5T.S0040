using System;


namespace R5T.S0040
{
    public sealed class XPathOperations : IXPathOperations
    {
        #region Infrastructure

        public static XPathOperations Instance { get; } = new();

        private XPathOperations()
        {
        }

        #endregion
    }
}
