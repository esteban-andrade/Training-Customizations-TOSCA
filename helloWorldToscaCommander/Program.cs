using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAPI;
using Tricentis.TCAPIObjects.Objects;

namespace helloWorldToscaCommander
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TCAPI tcapi = TCAPI.CreateInstance())
            {
                TCWorkspace workspace = tcapi.OpenWorkspace(
                    twsPath: @"C:\Users\estan\Desktop\trainings\TOSCA Customizations\Tosca Workspace\Training Customizations\Training Customizations.tws",
                     loginName: "Admin", loginPassword: "");
                TCProject project = workspace.GetProject();
                TCFolder folder = project.CreateComponentFolder();
                folder.Name = "Created folder With Script";
                folder.EnsureUniqueName();
               // workspace.Save();

                List<TCObject> search_result = project.Search(tqlString: "=>SUBPARTS:TestCase[Name==\"Test\"]");
                TestCase copyTestCase = (TestCase)search_result.First();
                OwnedItem parentFolder = copyTestCase.ParentFolder;
               // parentFolder.CheckoutTree();
                
                for (int i =0;i<10;i++)
                {
                    parentFolder.Copy(copyTestCase);
                }
                //workspace.CheckInAll("");
                workspace.Save();

                Console.WriteLine(value: "Created folder and Test Cases Copied");
         
            }
        }
    }
}
