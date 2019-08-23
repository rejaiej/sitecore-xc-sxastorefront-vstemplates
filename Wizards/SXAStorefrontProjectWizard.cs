namespace Customer.Commerce.XA.Storefront.Extension.Wizards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Linq;

    using Microsoft.VisualStudio.ComponentModelHost;
    using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.TemplateWizard;

    using EnvDTE;
    using EnvDTE80;

    using NuGet.VisualStudio;

    using Configurations;
    using Extensions;
   

    public class SXAStorefrontProjectWizard : IWizard
    {
        private string _desiredNamespace;
        private string _newDestinationDirectory;
        private string _originalDestinationDirectory;
        private string _originalSolutionDirectory;
        private string _vsTemplatePath;
        private EnvDTE._DTE _currentDTE;
        private SitecoreConfiguration _sitecoreConfiguration;
        private Project _currentProject;

        // This method is called before opening any item that
        // has the OpenInEditor attribute.
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
            // grab reference of project since we need it
            this._currentProject = project;

            // if this isn't a stub, go and grab the NuGet packages
            if (!this._sitecoreConfiguration.IsStub)
            {
                using (var serviceProvider = new ServiceProvider((IServiceProvider)project.DTE))
                {
                    var componentModel = (IComponentModel)serviceProvider.GetService(typeof(SComponentModel));
                    IVsPackageInstaller2 installer =
                        componentModel.GetService<IVsPackageInstaller2>();
                   
                    foreach (var package in this._sitecoreConfiguration.NuGetConfiguration.Packages)
                    {
                        installer.InstallPackage(null, project, package.Name, package.Version, false);
                    }
                }
            }
        }

        // This method is only called for item templates,
        // not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem
            projectItem)
        { 
        }

        // This method is called after the project is created.
        public void RunFinished()
        {
            if(this._sitecoreConfiguration.IsStub)
            {
                // we need to run the real template
                this.AddRealHelixProject();
            }
            else
            {
                // we're done provisioning the real template, let's add the sibiling folders
                this.ProvisionSiblingFolders();
                this.CleanupFolders();
            }
        }

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            try
            {
                this._currentDTE = automationObject as EnvDTE._DTE;

                this.GetSitecoreConfiguration(replacementsDictionary, customParams);
                this.RememberTemplateInformation(replacementsDictionary);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.ToString()}. Stack Trace: {ex.StackTrace}");
            }
        }

        // This method is only called for item templates,
        // not for project templates.
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        private void GetSitecoreConfiguration(Dictionary<string, string> replacementsDictionary, object[] customParams)
        {
            if (customParams.Length <= 0) return;

            // Create physical folders
            this._vsTemplatePath = (string)customParams[0];
            this._sitecoreConfiguration = GetSitecoreConfigurationFromVsTemplateFile(replacementsDictionary);
        }

        private void RememberTemplateInformation(Dictionary<string, string> replacementsDictionary)
        {
            // website folder path
            string layerWebsiteDir = string.Concat(
                $"{replacementsDictionary["$solutiondirectory$"]}",
                "src",
                @"\",
                this._sitecoreConfiguration.HelixConfiguration.Layer,
                @"\",
                this._sitecoreConfiguration.HelixConfiguration.BusinessObjectiveName,
                @"\",
                $"{this._sitecoreConfiguration.HelixConfiguration.Layer}.{this._sitecoreConfiguration.HelixConfiguration.BusinessObjectiveName}.Website"
            );

            // take note of original and new parameters
            this._originalDestinationDirectory = replacementsDictionary["$destinationdirectory$"];
            this._newDestinationDirectory = layerWebsiteDir;
            this._desiredNamespace = replacementsDictionary["$safeprojectname$"];
            this._originalSolutionDirectory = replacementsDictionary["$solutiondirectory$"];
        }

        private void ProvisionSiblingFolders()
        {
            // create website folder
            string businessObjDir = string.Concat(
                $"{this._originalSolutionDirectory}",
                @"\",
                "src",
                @"\",
                this._sitecoreConfiguration.HelixConfiguration.Layer,
                @"\",
                this._sitecoreConfiguration.HelixConfiguration.BusinessObjectiveName
            );

            // create sibling folders
            if (this._sitecoreConfiguration.HelixConfiguration.ProvisionEngineFolder)
            {
                this.CreateFolder(businessObjDir, $"{this._sitecoreConfiguration.HelixConfiguration.Layer}.{ this._sitecoreConfiguration.HelixConfiguration.BusinessObjectiveName}.Engine");
                this.CreateFolder(businessObjDir, "postman");
            }

            if (this._sitecoreConfiguration.HelixConfiguration.ProvisionSerializationFolder)
            {
                this.CreateFolder(businessObjDir, "serialization");
            }

            if (this._sitecoreConfiguration.HelixConfiguration.ProvisionTestsFolder)
            {
                this.CreateFolder(businessObjDir, "tests");
            }
        }

        private void CleanupFolders()
        {
            // delete App Start from project
            var appStartFolder = this._currentProject.ProjectItems.Cast<ProjectItem>().FirstOrDefault(pi => pi.Name == "App_Start");
            if(appStartFolder != null)
            {
                appStartFolder.Remove();
                this._currentProject.Save();
            }

            // delete app_start folder if it's still there
            string appStartDir = string.Concat(
                $"{_newDestinationDirectory}",
                @"\",
                "App_Start"
            );

            this.DeleteFolder(appStartDir);
        }

        private SitecoreConfiguration GetSitecoreConfigurationFromVsTemplateFile(Dictionary<string, string> replacementsDictionary)
        {
            XDocument document = this.LoadDocument(this._vsTemplatePath);

            return this.GetSitecoreConfigurationFromVsTemplateFile(document, replacementsDictionary);
        }

        internal virtual XDocument LoadDocument(string filePath)
        {
            var xmlReaderSettings = new XmlReaderSettings
            {
                XmlResolver = null,
                DtdProcessing = DtdProcessing.Prohibit,
                IgnoreWhitespace = false
            };

            using (var reader = XmlReader.Create(filePath, xmlReaderSettings))
            {
                return XDocument.Load(reader);
            }
        }

        private SitecoreConfiguration GetSitecoreConfigurationFromVsTemplateFile(
            XDocument document, Dictionary<string, string> replacementsDictionary)
        {
            // Get elements with no namespaces
            var sitecoreElement = document.Root
                .GetXElementWithNoNamespace("WizardData")
                .GetXElementWithNoNamespace("sitecore");
            var helixElement = sitecoreElement
                .GetXElementWithNoNamespace("helix");
            var packageElements = sitecoreElement
                .GetXElementWithNoNamespace("NuGet")
                .GetXElementWithNoNamespace("packages")
                .GetXElementsWithNoNamespace("package");

            // build Sitecore configuration
            var sitecoreConfiguration = new SitecoreConfiguration()
            {
                IsStub = Convert.ToBoolean(sitecoreElement.Attribute("isStub")?.Value),
                HelixConfiguration = new HelixConfiguration()
                {
                    Layer = helixElement?.Attribute("layer")?.Value,
                    BusinessObjectiveName = helixElement?.Attribute("businessObjectiveName")?.Value,
                    ProvisionTestsFolder = Convert.ToBoolean(helixElement?.Attribute("provisionTestsFolder")?.Value),
                    ProvisionEngineFolder = Convert.ToBoolean(helixElement?.Attribute("provisionEngineFolder")?.Value),
                    ProvisionSerializationFolder = Convert.ToBoolean(helixElement?.Attribute("provisionSerializationFolder")?.Value)
                },
                NuGetConfiguration = new NuGetConfiguration()
            };

            // infer business objective name if nothing is specified
            if (string.IsNullOrWhiteSpace(sitecoreConfiguration.HelixConfiguration.BusinessObjectiveName))
            {
                var projectNameParts = replacementsDictionary["$safeprojectname$"]?
                    .Replace(".stub", "")
                    .Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                sitecoreConfiguration.HelixConfiguration.BusinessObjectiveName = projectNameParts.Last();
            }

            if (packageElements != null)
            {
                foreach (var packageElement in packageElements)
                {
                    var package = new NuGetPackage()
                    {
                        Name = packageElement?.Attribute("name")?.Value,
                        Version = packageElement?.Attribute("version")?.Value,
                        IgnoreDependencies = Convert.ToBoolean(packageElement?.Attribute("ignoreDependencies")?.Value)
                    };

                    sitecoreConfiguration.NuGetConfiguration.Packages.Add(package);
                }
            }

            return sitecoreConfiguration;
        }

        private string CreateFolder(string basePath, string folderName)
        {
            string dir = string.Concat(
                   basePath,
                   @"\",
                   $"{folderName}"
               );

            Directory.CreateDirectory(dir);
            return dir;
        }

        private void DeleteFolder(string folderName, bool deleteParent = false, bool isRecursive = true)
        {
            DirectoryInfo appStartDirectoryInfo = new DirectoryInfo(folderName);
            if (appStartDirectoryInfo.Exists)
            {
                if(deleteParent) Directory.Delete(appStartDirectoryInfo.Parent.FullName, isRecursive);
                else Directory.Delete(appStartDirectoryInfo.FullName, isRecursive);
            }
        }

        public void AddRealHelixProject()
        {
            // get solution folder
            var solutionFolder = this._currentDTE.Solution.Projects.Cast<Project>().FirstOrDefault(p => p.Name.Equals(this._sitecoreConfiguration.HelixConfiguration.Layer, StringComparison.InvariantCultureIgnoreCase))?.Object as SolutionFolder;

            // get real helix VS template path
            string realVSHelixTemplatePath = this._vsTemplatePath.ToLowerInvariant().Replace(".stub", "");

            // scrub namespaceS
            string scrubbedNamespace = this._desiredNamespace.Replace(".stub", "");

            // add real helix project from template
            var newProject = solutionFolder.AddFromTemplate(
                realVSHelixTemplatePath,
                this._newDestinationDirectory,
                scrubbedNamespace);

            // delete stub directory
            DirectoryInfo originalDestinationDirectoryInfo = new DirectoryInfo(this._originalDestinationDirectory.TrimEnd('\\'));
            DirectoryInfo originalSolutionDirectoryInfo = new DirectoryInfo(this._originalSolutionDirectory.TrimEnd('\\'));

            if(originalDestinationDirectoryInfo.Parent.FullName.Equals(originalSolutionDirectoryInfo.FullName, StringComparison.InvariantCultureIgnoreCase))
            {
                Directory.Delete(originalDestinationDirectoryInfo.FullName, true);
            }
            else
            {
                Directory.Delete(originalDestinationDirectoryInfo.Parent.FullName, true);
            }
        }
    }
}
