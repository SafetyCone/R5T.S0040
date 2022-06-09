using System;

using R5T.T0131;


namespace R5T.S0040
{
    [DraftValuesMarker]
    public class XPaths : IDraftValuesMarker
    {
        #region Infrastructure

        public static XPaths Instance { get; } = new();

        private XPaths()
        {
        }

        #endregion


#pragma warning disable CA1822 // Mark members as static


        public string HasTargetFrameworkPropertyGroupXDocumentRelativeXPath => "//Project/PropertyGroup[TargetFramework]";
    }
}
