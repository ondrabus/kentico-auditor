using CMS.Localization;
using CMS.Membership;
using CMS.SiteProvider;
using CMS.WorkflowEngine;
using System;
using System.Collections.Generic;
using CMS.DocumentEngine;
using CMS.DataEngine;
using CMS.PortalEngine;
using CMS.Newsletters;
using CMS.OnlineForms;
using CMS.MediaLibrary;
using CMS.Ecommerce;
using CMS.Polls;
using CMS.Synchronization;

namespace Auditor.Core.Actions.Object
{
    public static class ObjectSettings
    {
        public static Dictionary<Type, ActionType> SupportedObjectActions => new Dictionary<Type, ActionType>
        {
            // sites
            { typeof(SiteInfo),             ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(CultureSiteInfo),      ActionType.Insert | ActionType.Update | ActionType.Delete },

            // users
            { typeof(RoleInfo),             ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(UserRoleInfo),         ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(UserSiteInfo),         ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(UserSettingsInfo),     ActionType.Insert | ActionType.Update | ActionType.Delete },

            // content
            { typeof(DocumentTypeInfo),         ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(DocumentTypeScopeClassInfo),ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(DocumentTypeScopeInfo),    ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(AttachmentInfo),           ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(AttachmentHistoryInfo),    ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(PageTemplateInfo),         ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(PageTemplateSiteInfo),     ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(PageTemplateCategoryInfo), ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(WorkflowInfo),             ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(WorkflowScopeInfo),        ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(WorkflowStepInfo),         ActionType.Insert | ActionType.Update | ActionType.Delete },

            { typeof(BizFormInfo),              ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(MediaFileInfo),            ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(MediaLibraryInfo),         ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(DocumentAliasInfo),        ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(DocumentCategoryInfo),     ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(DocumentTagInfo),          ActionType.Insert | ActionType.Update | ActionType.Delete },
            //{ typeof(PersonalizationInfo),      ActionType.Insert | ActionType.Update | ActionType.Delete },

            {typeof(DataClassInfo),             ActionType.Insert | ActionType.Update | ActionType.Delete },
            {typeof(ClassSiteInfo),             ActionType.Insert | ActionType.Update | ActionType.Delete },

            // online marketing
            //{ typeof(CampaignInfo),         ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(NewsletterInfo),       ActionType.Insert | ActionType.Update | ActionType.Delete },
            //{ typeof(ConversionInfo),       ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(IssueInfo),            ActionType.Insert | ActionType.Update | ActionType.Delete },
            //{ typeof(ABTestInfo),           ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(EmailTemplateInfo),    ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(IssueABVariantItem),   ActionType.Insert | ActionType.Update | ActionType.Delete },

            // ecommerce
            //{ typeof(OrderInfo),            ActionType.Insert | ActionType.Update | ActionType.Delete },
            //{ typeof(OrderAddressInfo),     ActionType.Insert | ActionType.Update | ActionType.Delete },
            //{ typeof(OrderStatusUserInfo),  ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(SKUInfo),              ActionType.Insert | ActionType.Update | ActionType.Delete },
            //{ typeof(SKUAllowedOptionInfo), ActionType.Insert | ActionType.Update | ActionType.Delete },
            //{ typeof(SKUOptionCategoryInfo),ActionType.Insert | ActionType.Update | ActionType.Delete },
            //{ typeof(OptionCategoryInfo),   ActionType.Insert | ActionType.Update | ActionType.Delete },

            // polls
            { typeof(PollInfo),         ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(PollAnswerInfo),   ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(PollSiteInfo),     ActionType.Insert | ActionType.Update | ActionType.Delete },

            // resource strings
            { typeof(ResourceStringInfo),       ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(ResourceTranslationInfo),  ActionType.Insert | ActionType.Update | ActionType.Delete },
            { typeof(ResourceTranslatedInfo),   ActionType.Insert | ActionType.Update | ActionType.Delete },
        };

        public static List<AssignmentTypeConfiguration> AssignmentTypes => new List<AssignmentTypeConfiguration>
        {
            new AssignmentTypeConfiguration<CultureSiteInfo>(x => x.CultureID, CultureInfo.OBJECT_TYPE, x => x.SiteID, SiteInfo.OBJECT_TYPE),
            new AssignmentTypeConfiguration<UserRoleInfo>(x => x.UserID, UserInfo.OBJECT_TYPE, x => x.RoleID, RoleInfo.OBJECT_TYPE),
            new AssignmentTypeConfiguration<UserSiteInfo>(x => x.UserID, UserInfo.OBJECT_TYPE, x => x.SiteID, SiteInfo.OBJECT_TYPE),

            new AssignmentTypeConfiguration<PageTemplateSiteInfo>(x => x.PageTemplateID, PageTemplateInfo.OBJECT_TYPE, x => x.SiteID, SiteInfo.OBJECT_TYPE),

            new AssignmentTypeConfiguration<DocumentTypeScopeClassInfo>(x => x.ScopeID, DocumentTypeScopeInfo.OBJECT_TYPE, x => x.ClassID, DocumentTypeInfo.OBJECT_TYPE_DOCUMENTTYPE),

            new AssignmentTypeConfiguration<ClassSiteInfo>(x => x.ClassID, DataClassInfo.OBJECT_TYPE, x => x.SiteID, SiteInfo.OBJECT_TYPE)
        };

        public static Dictionary<string, List<string>> DefaultObjectData => new Dictionary<string, List<string>>
        {
            { WorkflowScopeInfo.OBJECT_TYPE, new List<string> { nameof(WorkflowScopeInfo.ScopeWorkflowID) }},
            { AttachmentInfo.OBJECT_TYPE, new List<string> { nameof(AttachmentInfo.AttachmentDocumentID) }},
            { AttachmentHistoryInfo.OBJECT_TYPE, new List<string> { nameof(AttachmentHistoryInfo.AttachmentDocumentID) }},
            { DocumentAliasInfo.OBJECT_TYPE, new List<string> { nameof(DocumentAliasInfo.AliasNodeID) }},
            { StagingTaskInfo.OBJECT_TYPE, new List<string>{
                nameof(StagingTaskInfo.TaskID),
                nameof(StagingTaskInfo.TaskType),
                nameof(StagingTaskInfo.TaskDocumentID),
                nameof(StagingTaskInfo.TaskTitle),
                nameof(StagingTaskInfo.TaskObjectID),
                nameof(StagingTaskInfo.TaskObjectType),
                nameof(StagingTaskInfo.TaskServers)
            }},
            { ResourceTranslationInfo.OBJECT_TYPE, new List<string>{
                nameof(ResourceTranslationInfo.TranslationStringID),
                nameof(ResourceTranslationInfo.TranslationID)
            }}
        };


        
    }
}
