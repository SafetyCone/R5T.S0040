using System;

using R5T.F0000;


namespace R5T.S0040.Library
{
    public static class Instances
    {
        public static Z0072.Z002.IFileExtensions FileExtensions => Z0072.Z002.FileExtensions.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = F0000.FileSystemOperator.Instance;
        public static T0040.ISolutionPathsOperator SolutionPathsOperator { get; } = T0040.SolutionPathsOperator.Instance;
    }
}