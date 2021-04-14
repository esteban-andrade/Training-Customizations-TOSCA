//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Diagnostics;
//using System.Collections.Generic;
//using Tricentis.Automation.AutomationInstructions.Dynamic.Values;
//using Tricentis.Automation.AutomationInstructions.TestActions;
//using Tricentis.Automation.Engines;
//using Tricentis.Automation.Engines.SpecialExecutionTasks;
//using Tricentis.Automation.Engines.SpecialExecutionTasks.Attributes;
//using Tricentis.Automation.Creation;
//using Tricentis.Automation.AutomationInstructions.Configuration;
//using Tricentis.Automation.Execution.Results;


//namespace StartProgramSpecialExe
//{
//    [SpecialExecutionTaskName("SetBuffer")]
//    public class SetBuffer : SpecialExecutionTaskEnhanced
//    {
//        public SetBuffer(Validator validator) : base(validator)
//        {
//        }

//        public override void ExecuteTask(ISpecialExecutionTaskTestAction testAction)
//        {
//            iterate over each TestStepCalue
//            foreach (IParameter parameter in testAction.Parameters)
//            {
//                actionmode input meansset to buffer.
//                if (parameter.ActionMode == ActionMode.Input)
//                {
//                    IInputValue inputValue = parameter.GetAsInputValue();
//                    Buffers.Instance.SetBuffer(parameter.Name, inputValue.Value, false);
//                    testAction.SetResultForParameter(parameter, SpecialExecutionTaskResultState.Ok, string.Format("Buffer {0} set to value {1}"), parameter.Name, inputValue.Value);
//                }
//                otherwise we let tbox handle the verification.Other activities list wait on will lead to exception.
//                else
//                {
//                    Don't need the return value of HandleActualValue in this case.
//                    HandleActualValue(testAction, parameter, Buffers.Instance.GetBuffer(parameter.Name));
//                }
//            }
//        }
//    }
//}

using Tricentis.Automation.AutomationInstructions.Configuration;
using Tricentis.Automation.AutomationInstructions.Dynamic.Values;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.SpecialExecutionTasks;
using Tricentis.Automation.Engines.SpecialExecutionTasks.Attributes;
using Tricentis.Automation.Execution.Results;

namespace SetBuffer
{
    [SpecialExecutionTaskName("SetBuffer")]
    public class SetBuffer : SpecialExecutionTaskEnhanced
    {
        public SetBuffer(Validator validator) : base(validator)
        {
        }

        public override void ExecuteTask(ISpecialExecutionTaskTestAction testAction)
        {
            //Iterate over each TestStepValue
            foreach (IParameter parameter in testAction.Parameters)
            {
                //ActionMode input means set the buffer
                if (parameter.ActionMode == ActionMode.Input)
                {
                    IInputValue inputValue = parameter.GetAsInputValue();
                    Buffers.Instance.SetBuffer(parameter.Name, inputValue.Value, false);
                    testAction.SetResultForParameter(parameter, SpecialExecutionTaskResultState.Ok, string.Format("Buffer {0} set to value {1}.", parameter.Name, inputValue.Value));
                }
                //Otherwise we let TBox handle the verification. Other ActionModes like WaitOn will lead to an exception.
                else
                {
                    //Don't need the return value of HandleActualValue in this case.
                    HandleActualValue(testAction, parameter, Buffers.Instance.GetBuffer(parameter.Name));
                }
            }
        }
    }
}