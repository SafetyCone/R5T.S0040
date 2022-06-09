using System;

using R5T.T0062;
using R5T.T0070;


namespace R5T.S0040
{
    public static class Instances
    {
        public static T0032.IFileExtension FileExtension { get; } = T0032.FileExtension.Instance;
        public static T0044.IFileSystemOperator FileSystemOperator { get; } = T0044.FileSystemOperator.Instance;
        public static IHost Host { get; } = T0070.Host.Instance;
        public static Functionalities.IOperations Operations { get; } = Functionalities.Operations.Instance;
        public static IServiceAction ServiceAction { get; } = T0062.ServiceAction.Instance;
        public static T0040.ISolutionPathsOperator SolutionPathsOperator { get; } = T0040.SolutionPathsOperator.Instance;
        public static IXPathOperations XPathOperations { get; } = S0040.XPathOperations.Instance;
        public static XPaths XPaths { get; } = XPaths.Instance;
    }
}