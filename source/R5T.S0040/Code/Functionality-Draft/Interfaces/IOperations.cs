using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

using R5T.Magyar.Xml;

using R5T.T0132;


namespace R5T.S0040
{
    /// <summary>
    /// General operations.
    /// </summary>
    [DraftFunctionalityMarker]
    public interface IOperations : IDraftFunctionalityMarker
    {
        /// <summary>
        /// Test for ambiguous method resolution.
        /// </summary>
        public void Test()
        {
        }

        /// <summary>
        /// Loads an XML document.
        /// </summary>
        public async Task<XDocument> LoadXDocument(string filePath)
        {
            using var fileStream = FileStreamHelper.NewRead(filePath);

            var xDocument = await XDocument.LoadAsync(
                fileStream,
                LoadOptions.None,
                CancellationToken.None);

            return xDocument;
        }

        public async Task SaveXDocument(
            string filePath,
            XDocument xDocument)
        {
            using var outputFileStream = FileStreamHelper.NewWrite(filePath);

            using var xmlWriter = XmlWriterHelper.New(outputFileStream);

            await xDocument.SaveAsync(
                xmlWriter,
                CancellationToken.None);
        }

        public async Task AddGenerateDocumentationFileElementToProject(string projectFilePath)
        {
            var projectXDocument = await this.LoadXDocument(projectFilePath);

            var propertyGroup = projectXDocument.XPathGetElement(
                Instances.XPaths.HasTargetFrameworkPropertyGroupXDocumentRelativeXPath);

            // Safety check to avoid adding twice.
            if(propertyGroup.HasChild("GenerateDocumentationFile"))
            {
                throw new Exception("Project file already had GenerateDocumentationFile node.");
            }

            var generateDocumentationFileXElement = new XElement("GenerateDocumentationFile")
            {
                Value = "true"
            };

            propertyGroup.Add(generateDocumentationFileXElement);

            await this.SaveXDocument(
                projectFilePath,
                projectXDocument);
        }
    }
}
