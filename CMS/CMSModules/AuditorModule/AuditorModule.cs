using Auditor.Core;
using Auditor.Core.Actions;
using Auditor.Core.Helpers;
using CMS;
using CMS.DataEngine;
using CMS.Modules;
using System.Linq;

[assembly: RegisterModule(typeof(AuditorModule))]
public class AuditorModule : Module
{
    public AuditorModule() : base(PredefinedConstants.ModuleName)
    {
    }

    protected override void OnInit()
    {
        base.OnInit();

        // check if installed
        var module = ResourceInfoProvider.GetResourceInfo(PredefinedConstants.ModuleName);
        if (module == null)
            return;
        
        Auditor.Mappings.Config.Register();

        CMS.Base.ApplicationEvents.PostStart.Execute += PostStart_Execute;
        CMS.Base.ApplicationEvents.End.Execute += End_Execute;
    }

    private void PostStart_Execute(object sender, System.EventArgs e)
    {
        // register all tasks
        TaskHelper.RegisterTasks();

        // check database table
        Auditor.Core.Helpers.DatabaseHelper.Install();

        // register all audit log actions
        var actions = InterfaceHelper
            .GetImplementingClassesInstances<IAuditableAction>()
            .Where(action => action.IsEnabled || action.IsGeneric)
            .ToList();
        
        actions.ForEach(action => action.Register());
    }

    private void End_Execute(object sender, System.EventArgs e)
    {
        StorageHelper.Instance.Process();
    }
}