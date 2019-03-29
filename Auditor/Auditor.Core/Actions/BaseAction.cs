using System;
using System.Collections.Generic;
using CMS.Membership;
using Auditor.Core.Models;
using Auditor.Core.Helpers;
using CMS.Base;
using CMS.SiteProvider;

namespace Auditor.Core.Actions
{
    internal abstract class BaseAction : IAuditableAction
    {
        protected virtual bool CancelAction { get; set; } = false;
        public string ThreadId
        {
            get
            {
                return System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            }
        }

        public virtual string SettingsKey => this.GetType().Name.Replace("Action", "");
        public bool IsEnabled => Helpers.SettingsHelper.Instance.AllowedActions.Contains(SettingsKey);
        public virtual bool IsGeneric => false;
        public virtual ActionType Type
        {
            get
            {
                var type = GetType();

                if (type.Name.Contains(nameof(ActionType.Insert)))
                    return ActionType.Insert;

                if (type.Name.Contains(nameof(ActionType.Update)))
                    return ActionType.Update;

                if (type.Name.Contains(nameof(ActionType.Delete)))
                    return ActionType.Delete;

                return ActionType.Other;
            }
        }
        public virtual Guid AuditDataUserGUID { get; set; } = MembershipContext.AuthenticatedUser.UserGUID;
        public virtual string AuditDataAction { get; set; }

        public virtual string AuditDataObjectName { get; set; } = string.Empty;
        public virtual string AuditDataSecondObjectName { get; set; } = string.Empty;
        public virtual Guid AuditDataObjectGuid { get; set; } = Guid.Empty;
        public virtual Guid AuditDataSecondObjectGuid { get; set; } = Guid.Empty;

        public virtual Guid AuditDataSiteGuid { get; set; } = SiteContext.CurrentSite?.SiteGUID ?? Guid.Empty;

        public abstract List<DataField> GetAuditData(CMSEventArgs e);

        public void CreateAuditLogItem(object sender, CMSEventArgs e)
        {
            ((BaseAction)Activator.CreateInstance(GetType())).CreateAuditLogItemInternal(sender, e);
        }

        protected void CreateAuditLogItemInternal(object sender, CMSEventArgs e)
        {
            if (IsGeneric)
            {
                if (!Helpers.ObjectHelper.ActionEnabled(e, Type))
                    return;
            }
            else
            {
                AuditDataAction = SettingsKey;
            }

            var data = GetAuditData(e);

            if (CancelAction)
                return;

            var auditData = new AuditData
            {
                SiteGuid = AuditDataSiteGuid,
                UserGuid = AuditDataUserGUID,
                Action = AuditDataAction,
                DateCreated = DateTime.UtcNow,
                ObjectName = AuditDataObjectName,
                SecondObjectName = AuditDataSecondObjectName,
                Data = data
            };

            if (AuditDataObjectGuid != Guid.Empty)
                auditData.ObjectGuid = AuditDataObjectGuid;

            if (AuditDataSecondObjectGuid != Guid.Empty)
                auditData.SecondObjectGuid = AuditDataSecondObjectGuid;

            StorageHelper.Instance.Add(auditData);
        }

        public void PrepareAuditedObjectData(object sender, CMSEventArgs e)
        {
            var key = string.Empty;
            var data = PrepareData(e, ref key);

            if (data == null || string.IsNullOrEmpty(key))
                return;

            CMS.Helpers.RequestStockHelper.Add(key, data);
        }

        protected virtual List<KeyValuePair<string, object>> PrepareData(CMSEventArgs e, ref string key)
        {
            if (Type == ActionType.Update)
                return ActionsHelper.GetChangedData(e, ref key);
            else
                return null;
        }

        public abstract void Register();
    }
}