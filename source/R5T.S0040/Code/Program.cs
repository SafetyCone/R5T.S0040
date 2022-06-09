using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using R5T.Magyar;
using R5T.Magyar.Extensions;
using R5T.Magyar.Xml;

using R5T.D0088;
using R5T.D0090;
using R5T.D0105;


// To see C# language and compiler version in Visual Studio error output.
//#error version

namespace R5T.S0040
{
    class Program : ProgramAsAServiceBase
    {
        #region Static
        
        static async Task Main()
        {
            //OverridableProcessStartTimeProvider.Override("20211214 - 163052");
            //OverridableProcessStartTimeProvider.DoNotOverride();
        
            await Instances.Host.NewBuilder()
                .UseProgramAsAService<Program, T0075.IHostBuilder>()
                .UseHostStartup<HostStartup, T0075.IHostBuilder>(Instances.ServiceAction.AddHostStartupAction())
                .Build()
                .SerializeConfigurationAudit()
                .SerializeServiceCollectionAudit()
                .RunAsync();
        }

        #endregion


        private ILogger Logger { get; }

        
        public Program(IServiceProvider serviceProvider,
            ILogger<Program> logger)
            : base(serviceProvider)
        {
            this.Logger = logger;
        }

        protected override Task ServiceMain(CancellationToken stoppingToken)
        {
            return this.RunMethod();
            //return this.RunOperation();
        }

#pragma warning disable CA1822 // Mark members as static
#pragma warning disable IDE0051 // Remove unused private members

        private Task RunOperation()
        {
            return Task.CompletedTask;
        }

        private async Task RunMethod()
        {
            //await this.ListProjects();
            //await this.ListProjectsWithoutGenerateDocumentationFileElement();
            //await this.AddGenerateDocumentationFileElementToProjects();
            await this.AddNoWarnAttribute();
        }

#pragma warning disable CS1587 // XML comment is not placed on a valid language element

        private async Task AddNoWarnAttribute()
        {
            //Instances.Operations.As<Library.IOperations, IOperations>().Test();
            Instances.Operations.Test();

            var repositoriesDirectoryPath = Instances.Operations.GetRepositoriesDirectoryPath();

            var projectFilePaths = Instances.Operations.GetAllProjectFilePaths(repositoriesDirectoryPath);

            var projectFilePathsCount = projectFilePaths.Length;

            var problemProjects = new List<string>();

            var counter = 0;
            foreach (var projectFilePath in projectFilePaths)
            {
                try
                {
                    this.Logger.LogInformation($"Modifying {counter} of {projectFilePathsCount}: {projectFilePath}");

                    var projectXDocument = await Instances.Operations.LoadXDocument(projectFilePath);

                    var propertyGroup = projectXDocument.XPathGetElement(
                        Instances.XPaths.HasTargetFrameworkPropertyGroupXDocumentRelativeXPath);

                    // Safety check to avoid adding twice.
                    if (propertyGroup.HasChild("NoWarn"))
                    {
                        continue;
                    }

                    var generateDocumentationFileXElement = new XElement("NoWarn")
                    {
                        Value = "1591;1573"
                    };

                    propertyGroup.Add(generateDocumentationFileXElement);

                    await Instances.Operations.SaveXDocument(
                        projectFilePath,
                        projectXDocument);
                }
                catch
                {
                    problemProjects.Add(projectFilePath);
                }
            }
        }

        private async Task BuildAllProjectsToGenerateDocumentationFiles()
        {
            /// Inputs.
            var projectsModifiedListFilePath = @"C:\Temp\Projects List-Modified for GenerateDocumentationFile element.txt";

            var outputFilePath = @"C:\Temp\Projects List-Rebuilt.txt";
            var problemProjectsFilePath = @"C:\Temp\Projects List-Problems with Build.txt";

            /// Run.
            var projectFilePaths = FileHelper.ActuallyReadAllLines(
                projectsModifiedListFilePath)
                // Skip the first two lines.
                .Skip(2)
                .ToArray();

            var projectFilePathsCount = projectFilePaths.Length;

            var problemProjects = new List<string>();

            var counter = 1;
            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogInformation($"Building {counter} of {projectFilePathsCount}: {projectFilePath}");

                try
                {

                }
                catch
                {
                    problemProjects.Add(projectFilePath);
                }

                counter++;
            }

            var outputLines = EnumerableHelper.From($"Project file paths rebuilt (Count: {projectFilePathsCount})\n")
                .Append(projectFilePaths
                    .Except(problemProjects)
                    .OrderAlphabetically());

            await FileHelper.WriteAllLines(
                outputFilePath,
                outputLines);

            await FileHelper.WriteAllLines(
                problemProjectsFilePath,
                problemProjects);

            var notepadPlusPlusOperator = this.ServiceProvider.GetRequiredService<INotepadPlusPlusOperator>();

            await notepadPlusPlusOperator.OpenFilePath(outputFilePath);
            await notepadPlusPlusOperator.OpenFilePath(problemProjectsFilePath);
        }

        private async Task AddGenerateDocumentationFileElementToProjects()
        {
            /// Inputs.
            var projectsWithoutElementListFilePath = @"C:\Temp\Projects List-Without GenerateDocumentationFile element.txt";

            var outputFilePath = @"C:\Temp\Projects List-Modified for GenerateDocumentationFile element.txt";
            var problemProjectsFilePath = @"C:\Temp\Projects List-Problems with add GenerateDocumentationFile element.txt";

            /// Run.
            var projectFilePaths = FileHelper.ActuallyReadAllLines(
                projectsWithoutElementListFilePath)
                // Skip the first two lines.
                .Skip(2)
                .ToArray();

            var projectFilePathsCount = projectFilePaths.Length;

            var problemProjects = new List<string>();

            var counter = 1;
            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogInformation($"Processing {counter} of {projectFilePathsCount}: {projectFilePath}");

                try
                {
                    await Instances.Operations.AddGenerateDocumentationFileElementToProject(
                        projectFilePath);
                }
                catch
                {
                    problemProjects.Add(projectFilePath);
                }

                counter++;
            }

            var outputLines = EnumerableHelper.From($"Project file paths now with <GenerateDocumentationFile> (Count: {projectFilePathsCount})\n")
                .Append(projectFilePaths
                    .Except(problemProjects)
                    .OrderAlphabetically());

            await FileHelper.WriteAllLines(
                outputFilePath,
                outputLines);

            await FileHelper.WriteAllLines(
                problemProjectsFilePath,
                problemProjects);

            var notepadPlusPlusOperator = this.ServiceProvider.GetRequiredService<INotepadPlusPlusOperator>();

            await notepadPlusPlusOperator.OpenFilePath(outputFilePath);
            await notepadPlusPlusOperator.OpenFilePath(problemProjectsFilePath);
        }

        private async Task ListProjectsWithoutGenerateDocumentationFileElement()
        {


            /// Inputs.
            var projectsListFilePath = @"C:\Temp\Projects List.txt";

            var outputFilePath = @"C:\Temp\Projects List-Without GenerateDocumentationFile element.txt";

            /// Run.
            var projectFilePaths = FileHelper.ActuallyReadAllLines(
                projectsListFilePath)
                // Skip the first two lines.
                .Skip(2)
                .ToArray();

            var projectReferenceXDocumentRelativeXPath = "//Project/PropertyGroup/GenerateDocumentationFile";

            var projectFilePathsOfInterest = new List<string>();

            var projectFilePathsCount = projectFilePaths.Length;

            var counter = 1;
            foreach (var projectFilePath in projectFilePaths)
            {
                this.Logger.LogInformation($"Processing {counter} of {projectFilePathsCount}: {projectFilePath}");

                using var fileStream = FileStreamHelper.NewRead(projectFilePath);

                var projectXDocument = await XDocument.LoadAsync(
                    fileStream,
                    LoadOptions.None,
                    CancellationToken.None);

                var generateDocumentationFileXElementOrDefault = projectXDocument.XPathSelectElement(projectReferenceXDocumentRelativeXPath);

                var generateDocumentationFileXElementWasFound = WasFound.From(generateDocumentationFileXElementOrDefault);

                if(!generateDocumentationFileXElementWasFound)
                {
                    projectFilePathsOfInterest.Add(projectFilePath);
                }

                counter++;
            }

            
            var projectFilePathsOfInterestCount = projectFilePathsOfInterest.Count;

            var outputLines = EnumerableHelper.From($"Project file paths without <GenerateDocumentationFile> (Count: {projectFilePathsOfInterestCount} of {projectFilePathsCount})\n")
                .Append(projectFilePathsOfInterest
                    .OrderAlphabetically());

            await FileHelper.WriteAllLines(
                outputFilePath,
                outputLines);

            var notepadPlusPlusOperator = this.ServiceProvider.GetRequiredService<INotepadPlusPlusOperator>();

            await notepadPlusPlusOperator.OpenFilePath(outputFilePath);
        }

        private async Task ListProjects()
        {
            /// Inputs.
            var outputProjectsListFilePath = @"C:\Temp\Projects List.txt";

            /// Run.
            var repositoriesDirectoryPath = Instances.Operations.GetRepositoriesDirectoryPath();

            var projectFilePaths = Instances.Operations.GetAllProjectFilePaths(repositoriesDirectoryPath);

            var projectFilePathsCount = projectFilePaths.Length;

            var outputLines = EnumerableHelper.From($"Project file paths (Count: {projectFilePathsCount})\n")
                .Append(projectFilePaths
                    .OrderAlphabetically());

            await FileHelper.WriteAllLines(
                outputProjectsListFilePath,
                outputLines);

            var notepadPlusPlusOperator = this.ServiceProvider.GetRequiredService<INotepadPlusPlusOperator>();

            await notepadPlusPlusOperator.OpenFilePath(outputProjectsListFilePath);
        }
    }
}