
using CMS.DataEngine;
using System;

namespace Auditor.Core.Actions.Object
{
    public abstract class AssignmentTypeConfiguration
    {
        public abstract Type Type { get; }
        public abstract string ObjectType { get; }
        public abstract string SecondObjectType { get; }
        public abstract int GetObjectId(BaseInfo infoObject);
        public abstract int GetSecondObjectId(BaseInfo infoObject);
    }
    internal class AssignmentTypeConfiguration<TInfoObject> : AssignmentTypeConfiguration where TInfoObject : BaseInfo
    {
        private string _objectType;
        private string _secondObjectType;
        private Func<TInfoObject, int> _objectIdSelector;
        private Func<TInfoObject, int> _secondObjectIdSelector;

        public AssignmentTypeConfiguration(Func<TInfoObject, int> objectIdSelector, string objectType, Func<TInfoObject, int> secondObjectIdSelector, string secondObjectType)
        {
            _objectIdSelector = objectIdSelector;
            _objectType = objectType;

            _secondObjectIdSelector = secondObjectIdSelector;
            _secondObjectType = secondObjectType;
        }
        public override Type Type
        {
            get
            {
                return typeof(TInfoObject);
            }
        }

        public override int GetObjectId(BaseInfo infoObject)
        {
            if (infoObject is TInfoObject)
                return _objectIdSelector((TInfoObject)infoObject);

            throw new InvalidOperationException($"Unable to get object ID of {infoObject.GetType()} when TInfoObject si {typeof(TInfoObject)}.");
        }

        public override string ObjectType
        {
            get
            {
                return _objectType;
            }
        }

        public override int GetSecondObjectId(BaseInfo infoObject)
        {
            if (infoObject is TInfoObject)
                return _secondObjectIdSelector((TInfoObject)infoObject);

            throw new InvalidOperationException($"Unable to get second object ID of {infoObject.GetType()} when TInfoObject si {typeof(TInfoObject)}.");
        }

        public override string SecondObjectType
        {
            get
            {
                return _secondObjectType;
            }
        }
    }
}
