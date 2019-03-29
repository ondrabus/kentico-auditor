using System.Collections.Generic;
using Auditor.Core.Models;
using CMS.Base;
using Auditor.Core.Helpers;
using System;
using CMS.SiteProvider;

namespace Auditor.Core.Actions.Object
{
    internal abstract class ObjectBaseAction : BaseAction
    {
        public override bool IsGeneric => true;
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            if (CancelAction)
                return null;

            var args = ObjectHelper.GetObjectEventArgs(e);

            AuditDataObjectName = args.Object.Generalized.ObjectDisplayName;
            AuditDataObjectGuid = args.Object.Generalized.ObjectGUID;
            AuditDataAction = ObjectHelper.GetActionName(ObjectHelper.GetObjectType(args.Object), Type);
            AuditDataSiteGuid = Guid.Empty;

            var data = ObjectHelper.GetBaseInfoDefaultData(args.Object);

            var assignmentType = ObjectHelper.TryGetAssignmentType(ObjectHelper.GetObjectType(args.Object));
            if (assignmentType != null)
            {
                var objectId = assignmentType.GetObjectId(args.Object);
                var objectInfo = CMS.DataEngine.ProviderHelper.GetInfoById(assignmentType.ObjectType, objectId);
                var secondObjectId = assignmentType.GetSecondObjectId(args.Object);
                var secondObjectInfo = CMS.DataEngine.ProviderHelper.GetInfoById(assignmentType.SecondObjectType, secondObjectId);

                AuditDataObjectName = ObjectHelper.GetObjectName(objectInfo);
                AuditDataObjectGuid = ObjectHelper.GetObjectGuid(objectInfo);

                AuditDataSecondObjectName = ObjectHelper.GetObjectName(secondObjectInfo);
                AuditDataSecondObjectGuid = ObjectHelper.GetObjectGuid(secondObjectInfo);

                if (assignmentType.ObjectType == SiteInfo.OBJECT_TYPE)
                    AuditDataSiteGuid = AuditDataObjectGuid;
                else if (assignmentType.SecondObjectType == SiteInfo.OBJECT_TYPE)
                    AuditDataSiteGuid = AuditDataSecondObjectGuid;
            }

            if (args.Object.Generalized.TypeInfo.ObjectType == SiteInfo.OBJECT_TYPE)
                AuditDataSiteGuid = AuditDataObjectGuid;
            else if (args.Object.Generalized.ObjectSiteID != 0)
            {
                AuditDataSiteGuid = SiteInfoProvider.GetSiteInfo(args.Object.Generalized.ObjectSiteID).SiteGUID;
            }

            return data;
        }
    }
}
