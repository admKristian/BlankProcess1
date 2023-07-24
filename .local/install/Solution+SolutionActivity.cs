using System.Activities;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Orchestrator.Client.Models;
using UiPath.Testing;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;

namespace BlankProcess1
{
    public class SolutionActivity : Activity
    {
        public InArgument<string> s { get; set; }

        public OutArgument<bool> Output { get; set; }

        public SolutionActivity()
        {
            this.Implementation = () =>
            {
                return new SolutionActivityChild()
                {s = (this.s == null ? (InArgument<string>)Argument.CreateReference((Argument)new InArgument<string>(), "s") : (InArgument<string>)Argument.CreateReference((Argument)this.s, "s")), Output = (this.Output == null ? (OutArgument<bool>)Argument.CreateReference((Argument)new OutArgument<bool>(), "Output") : (OutArgument<bool>)Argument.CreateReference((Argument)this.Output, "Output")), };
            };
        }
    }

    internal class SolutionActivityChild : CodeActivity
    {
        public InArgument<string> s { get; set; }

        public OutArgument<bool> Output { get; set; }

        public SolutionActivityChild()
        {
            DisplayName = "Solution";
        }

        protected override void Execute(CodeActivityContext context)
        {
            var codedWorkflow = new BlankProcess1.Solution();
            CodedWorkflowHelper.Initialize(codedWorkflow, context);
            var result = CodedWorkflowHelper.RunWithExceptionHandling(() =>
            {
                codedWorkflow.Before(new BeforeRunContext()
                {RelativeFilePath = "Solution.cs"});
            }, () =>
            {
                var result = codedWorkflow.isPalindrome(s.Get(context));
                var newResult = new Dictionary<string, object>{{"Output", result}};
                return newResult;
            }, (exception, outArgs) =>
            {
                codedWorkflow.After(new AfterRunContext()
                {RelativeFilePath = "Solution.cs", Exception = exception});
            });
            Output.Set(context, (bool)result["Output"]);
        }
    }
}