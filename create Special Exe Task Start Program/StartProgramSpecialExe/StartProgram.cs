using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
//using System.Collections.Generic;
using Tricentis.Automation.AutomationInstructions.Dynamic.Values;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.Engines;
using Tricentis.Automation.Engines.SpecialExecutionTasks;
using Tricentis.Automation.Engines.SpecialExecutionTasks.Attributes;
using Tricentis.Automation.Creation;

namespace StartProgramSpecialExe
{

    [SpecialExecutionTaskName("StartProgram")]
    public class StartProgram : SpecialExecutionTask
    {
        public StartProgram(Validator validator) : base(validator)
        {
        }
        public override ActionResult Execute(ISpecialExecutionTaskTestAction testAction)
        {
            IInputValue path = testAction.GetParameterAsInputValue("Path", false);
            IParameter parameter = testAction.GetParameter("Arguments", true);
            string processArguments = string.Empty;

            if (path == null || string.IsNullOrEmpty(path.Value))
                throw new ArgumentException(string.Format("Mandatory parameter'{0}' not set.", path));


            if (parameter != null)
            {
                IEnumerable<IParameter> arguments = parameter.GetChildParameters("Argument");

                foreach (IParameter argument in arguments)
                {
                    IInputValue processArgument = argument.Value as IInputValue;
                    processArguments += processArgument.Value + " ";
                }
            }
            try
            {
                Process.Start(path.Value, processArguments);
            }
            catch
            {
                return new UnknownFailedActionResult("Could not start program",
                    string.Format("Failed while trying to start : \nPath :{0} \r\nArguments: {1}", path.Value, processArguments),
                    " ");

            }

            //return new PassedActionResult("Started Program" + path.Value + "sucessfully");
            return new PassedActionResult("Started Successfully");
        }
    }
}
