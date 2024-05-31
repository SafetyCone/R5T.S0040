using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;


namespace R5T.S0040.Library
{
    [DraftFunctionalityMarker]
    public interface IOperations : IDraftFunctionalityMarker
    {
        public void Test()
        {
        }

        public string GetRepositoriesDirectoryPath()
        {
            var output = @"C:\Code\DEV\Git\GitHub\SafetyCone";
            return output;
        }

        public string[] GetRepositoriesDirectoryPaths()
        {
            var output = new[]
            {
                @"C:\Code\DEV\Git\GitHub\SafetyCone",
                @"C:\Code\DEV\Git\GitHub\davidcoats",
            };

            return output;
        }

        public string[] GetAllRepositoryDirectoryPaths(string repositoriesDirectoryPath)
        {
            var output = Instances.FileSystemOperator.EnumerateAllChildDirectoryPaths(
                repositoriesDirectoryPath)
                .ToArray();

            return output;
        }

        public string[] GetAllSolutionDirectoryPaths(IEnumerable<string> repositoryDirectoryPaths)
        {
            var output = repositoryDirectoryPaths
                .Select(xRepositoryDirectoryPath => Instances.SolutionPathsOperator.GetSourceSolutionDirectoryPath(
                    xRepositoryDirectoryPath))
                .ToArray();

            return output;
        }

        public string[] GetAllProjectDirectoryPaths(IEnumerable<string> solutionDirectoryPaths)
        {
            var output = solutionDirectoryPaths
                .SelectMany(xSolutionDirectoryPath =>
                {
                    var directoryPathExists = Instances.FileSystemOperator.Exists_Directory(xSolutionDirectoryPath);

                    var output = directoryPathExists
                        ? Instances.FileSystemOperator.EnumerateAllChildDirectoryPaths(
                            xSolutionDirectoryPath)
                        : Enumerable.Empty<string>()
                        ;

                    return output;
                })
                .ToArray();

            return output;
        }

        public string[] GetAllProjectFilePaths(IEnumerable<string> projectDirectoryPaths)
        {
            var output = projectDirectoryPaths
                .SelectMany(xProjectDirectoryPath => this.GetProjectFilesInDirectory(
                    xProjectDirectoryPath))
                .ToArray();

            return output;
        }

        public string[] GetProjectFilesInDirectory(string directoryPath)
        {
            var projectFilePaths = Instances.FileSystemOperator.FindChildFilesInDirectoryByFileExtension(
               directoryPath,
               Instances.FileExtensions.CSharp_ProjectFile);

            return projectFilePaths;
        }

        public string[] GetAllProjectFilePaths()
        {
            var output = this.GetAllProjectFilePaths(
                this.GetRepositoriesDirectoryPath());

            return output;
        }

        public string[] GetAllProjectFilePaths(string repositoriesDirectoryPath)
        {
            var repositoryDirectoryPaths = this.GetAllRepositoryDirectoryPaths(repositoriesDirectoryPath);
            var solutionDirectoryPaths = this.GetAllSolutionDirectoryPaths(repositoryDirectoryPaths);
            var projectDirectoryPaths = this.GetAllProjectDirectoryPaths(solutionDirectoryPaths);

            var projectFilePaths = this.GetAllProjectFilePaths(projectDirectoryPaths);
            return projectFilePaths;
        }
    }
}
