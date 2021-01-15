using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAPI;
using Tricentis.TCAPIObjects.Objects;

namespace Create_a_Test_Case_With_Script
{
    class Program
    {

        private const string unique_id_test_case_folder = @"<39fa168a-eaef-0ecc-c65f-507bc8110bc9>";
        private const string unique_id_tbox_set_buffer = @"<39e342b2-6ec7-9851-5f18-e98742793c1a>";
        private const string unique_id_tbox_wait = @"<39e342b2-6ec7-3689-30bf-d2a034e90757>";

        static void Main(string[] args)
        {
            using (TCAPI tcapi = TCAPI.CreateInstance())
            {
                TCWorkspace workspace = tcapi.OpenWorkspace(@"C:\Users\estan\Desktop\trainings\TOSCA Customizations\Tosca Workspace\Training Customizations\Training Customizations.tws", "Admin", "");
                TCProject project = workspace.GetProject();
                List<TCObject> result = project.Search($"=>SUBPARTS:TCFolder[UniqueId==\"{unique_id_test_case_folder}\"]");
                TCFolder testCaseFolder = (TCFolder)result.First();
                testCaseFolder.Checkout();

                TestCase testCase = testCaseFolder.CreateTestCase();
                testCase.Name = "Test Creating Test Case";

                XModule tboxSetBuffer = (XModule)project.Search($"=>SUBPARTS:XModule[UniqueId==\"{unique_id_tbox_set_buffer}\"]").First();
                XModule tboxWait = (XModule)project.Search($"=>SUBPARTS:XModule[UniqueId==\"{unique_id_tbox_wait}\"]").First();


                XTestStep setBufferTestStep = testCase.CreateXTestStepFromXModule(tboxSetBuffer);
                XTestStepValue setBUfferStepvalue = setBufferTestStep.CreateXTestStepValue(tboxSetBuffer.Attributes.First());
                setBUfferStepvalue.Name = "Test Buffer Creation";
                setBUfferStepvalue.Value = "42";
                setBUfferStepvalue.ActionMode = XTestStepActionMode.Input;

                XTestStep tboxSetWaitTimeStep = testCase.CreateXTestStepFromXModule(tboxWait);
                XTestStepValue tboxWaitSettimeStepValue = tboxSetWaitTimeStep.TestStepValues.First();
                tboxWaitSettimeStepValue.Value = "500";
            }
        }
    }
}
