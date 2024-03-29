using System;

using R5T.F0000;


namespace R5T.S0040.Library
{
    public static class Instances
    {
        public static T0032.IFileExtension FileExtension { get; } = T0032.FileExtension.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = F0000.FileSystemOperator.Instance;
        public static T0040.ISolutionPathsOperator SolutionPathsOperator { get; } = T0040.SolutionPathsOperator.Instance;
    }
}