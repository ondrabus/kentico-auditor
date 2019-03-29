using Auditor.Core.Actions;
using Auditor.Core.Actions.Object;
using Auditor.Core.Models;
using CMS.Base;
using CMS.CustomTables;
using CMS.DataEngine;
using CMS.DocumentEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auditor.Core.Helpers
{
    public static class ObjectHelper
    {
        private static readonly string[] _timestampAndNotRelevantColumns = {
            "LastModified",
            "LastLogon",
            "UserLastLogonInfo",
            "DocumentCheckedOutByUserID",
            "DocumentCheckedOutWhen",
            "DocumentModifiedWhen",
            "DocumentCheckedOutVersionHistoryID",
            "DocumentLastVersionNumber",
            "DocumentPublishedVersionHistoryID",
            "DocumentWorkflowStepID",
            "DocumentLastPublished",
            "DocumentWorkflowCycleGUID",
            "AnswerCount"
        };

        public static ObjectEventArgs GetObjectEventArgs(CMSEventArgs e)
        {
            return GetEventArgs<ObjectEventArgs>(e);
        }
        public static DocumentEventArgs GetDocumentEventArgs(CMSEventArgs e)
        {
            var args = e as DocumentEventArgs;
            if (args == null)
                throw new InvalidOperationException("DocumentBaseAction must have DocumentEventArgs as parameter.");

            return args;
        }
        public static TInfo GetObject<TInfo>(CMSEventArgs e) where TInfo : BaseInfo
        {
            var args = GetObjectEventArgs(e);
            var obj = args?.Object as TInfo;

            if (obj == null)
                throw new InvalidOperationException($"{typeof(ObjectHelper).FullName}: Cannot convert event arguments of type '{e.GetType().Name}' to {typeof(TInfo).Name}");

            return obj;
        }

        internal static Type GetObjectType(BaseInfo baseInfo)
        {
            return baseInfo.GetType();
        }

        public static TArgs GetEventArgs<TArgs>(CMSEventArgs e) where TArgs : CMSEventArgs
        {
            var args = e as TArgs;
            if (args == null)
                throw new InvalidOperationException($"{typeof(ObjectHelper).FullName}: Invalid object in e variable - not {typeof(TArgs).Name}!");

            return args;
        }


        public static List<DataField> GetBaseInfoDefaultData(BaseInfo infoObject)
        {
            var list = new List<DataField>();

            var properties = GetBaseInfoDefaultDataColumns(infoObject);

            if (properties != null)
            {
                properties.ForEach(p => list.Add(new DataField { Name = p, Value = infoObject?.GetValue(p)?.ToString() }));
            }

            return list;
        }
        public static List<string> GetBaseInfoDefaultDataColumns(BaseInfo infoObject)
        {
            List<string> properties;
            ObjectSettings.DefaultObjectData.TryGetValue(infoObject.Generalized.TypeInfo.ObjectType, out properties);

            return properties;
        }


        public static List<DataField> GetBaseCustomTableItemData(CustomTableItem item)
        {
            return new List<DataField>
            {
                new DataField { Name = "ClassName", Value = item.CustomTableClassName }
            };
        }

        public static string GetActionName(Type objectType, ActionType type)
        {
            var actionName = objectType.Name;
            return actionName + type.ToString();
        }

        public static bool ActionEnabled(CMSEventArgs e, ActionType actionType)
        {
            // check object type
            var infoObject = (e as ObjectEventArgs)?.Object;
            if (infoObject == null)
                return false;

            var type = GetObjectType(infoObject);
            if (type == null)
                return false;

            var objectAction = GetActionName(type, actionType);
            if (objectAction == null)
                return false;

            // check if specific action exists - if so, event is handled by specific action
            if (ActionsHelper.Actions.Contains(objectAction))
                return false;

            // check if action is allowed
            if (!SettingsHelper.Instance.AllowedActions.Contains(objectAction))
                return false;

            return true;
        }


        public static List<KeyValuePair<string, object>> GetChangedColumns(BaseInfo infoObject)
        {
            var changedColumns = infoObject.ChangedColumns();

            changedColumns = ExcludeTimestampsAndNotRelevantFields(changedColumns);
            if (!changedColumns.Any())
                return null;

            var data = new List<KeyValuePair<string, object>>();
            foreach (string column in changedColumns)
            {
                if (data.Any(d => d.Key == column))
                    continue;

                data.Add(new KeyValuePair<string, object>(column, infoObject.GetOriginalValue(column)));
            }

            return data;
        }

        public static bool HasChanged(CMSEventArgs e)
        {
            var key = GetKey(GetBaseObject(e));
            return CMS.Helpers.RequestStockHelper.Contains(key);
        }

        public static string GetKey(BaseInfo infoObject)
        {
            return infoObject.Generalized.GetObjectKey();
        }
        public static BaseInfo GetBaseObject(CMSEventArgs e)
        {
            if (e is ObjectEventArgs objectEventArgs)
                return objectEventArgs.Object;

            if (e is DocumentEventArgs documentEventArgs)
                return documentEventArgs.Node;

            if (e is CustomTableItemEventArgs customTableItemEventArgs)
                return customTableItemEventArgs.Item;

            throw new NotSupportedException("Type " + e.ToString() + " is not supported for getting base object.");
        }

        private static List<string> ExcludeTimestampsAndNotRelevantFields(List<string> columns)
        {
            return columns.Where(c => !c.EndsWithAny(StringComparison.OrdinalIgnoreCase, _timestampAndNotRelevantColumns)).ToList();
        }

        public static List<DataField> AppendChangedColumns(CMSEventArgs e)
        {
            var infoObject = GetBaseObject(e);
            var data = new List<DataField>();

            var beforeUpdate = CMS.Helpers.RequestStockHelper.GetItem(GetKey(infoObject)) as List<KeyValuePair<string, object>>;
            if (beforeUpdate == null || beforeUpdate.Count == 0)
                return data;

            foreach (var field in beforeUpdate)
            {
                var dataField = data.SingleOrDefault(f => f.Name == field.Key);
                if (dataField == null)
                {
                    // add new field
                    var newDataField = new DataField
                    {
                        Name = field.Key,
                        Value = infoObject.GetValue(field.Key)?.ToString() ?? string.Empty,
                        OldValue = field.Value?.ToString() ?? string.Empty
                    };

                    if (!DataFieldHelper.ValuesAreIdentical(newDataField))
                        data.Add(newDataField);
                }
                else
                {
                    dataField.OldValue = field.Value?.ToString() ?? string.Empty;
                }
            }

            return data;
        }

        public static AssignmentTypeConfiguration TryGetAssignmentType(Type type)
        {
            return ObjectSettings.AssignmentTypes.SingleOrDefault(a => a.Type == type);
        }

        public static Guid GetObjectGuid(BaseInfo infoObject)
        {
            return infoObject.Generalized.ObjectGUID;
        }
        public static string GetObjectName(BaseInfo infoObject)
        {
            return infoObject.Generalized.ObjectCodeName;
        }
        public static string GetObjectTypeName(BaseInfo infoObject)
        {
            return infoObject.GetType().Name;
        }
    }
}