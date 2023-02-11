using System;

using R5T.T0131;


namespace R5T.S0040
{
    [DraftValuesMarker]
    public partial interface IXPaths : IDraftValuesMarker
    {
        public string HasTargetFrameworkPropertyGroupXDocumentRelativeXPath => "//Project/PropertyGroup[TargetFramework]";
    }
}
