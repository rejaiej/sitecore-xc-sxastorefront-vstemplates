namespace Customer.Commerce.XA.Storefront.Extension.Wizards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TemplateWizard;

    using EnvDTE;
    using EnvDTE80;
   

    public class SXAStorefrontSolutionWizard : IWizard
    {
        private string _vsTemplatePath;
        private string _originalSolutionDirectory;
        private EnvDTE._DTE _currentDTE;

        private const string SolutionItemsFolderName = ".Solution Items";

        // This method is called before opening any item that
        // has the OpenInEditor attribute.
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
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
            this.AddSolutionItems();
        }

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            try
            {
                this._currentDTE = automationObject as EnvDTE._DTE;
                this._vsTemplatePath = (string)customParams[0];
                this._originalSolutionDirectory = replacementsDictionary["$solutiondirectory$"];
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

        private void AddSolutionItems()
        {
            // get solution folder
            var solutionFolder = this._currentDTE.Solution.Projects.Cast<Project>().FirstOrDefault(p => p.Name.Equals(SolutionItemsFolderName, StringComparison.InvariantCultureIgnoreCase));

            // get VS Template directory
            DirectoryInfo vsTemplateFolder = new FileInfo(this._vsTemplatePath)?.Directory;

            // get solution folder
            DirectoryInfo solutionItemsFolder = vsTemplateFolder.GetDirectories("*SolutionItems").FirstOrDefault();

            // iterate through files
            foreach(FileInfo file in solutionItemsFolder.GetFiles("*"))
            {
                // copy file
                string solutionDirectoryFilePath = Path.Combine(this._originalSolutionDirectory, file.Name);
                File.Copy(file.FullName, solutionDirectoryFilePath);

                // add file to solution folder
                solutionFolder.ProjectItems.AddFromFileCopy(solutionDirectoryFilePath);
            }
        }
    }
}
