// Automatically Generated

using System.Diagnostics.CodeAnalysis;
using HKLib.hk2018;

namespace HKLib.Reflection.hk2018;

internal class hkbGetUpModifierInternalStateData : HavokData<hkbGetUpModifierInternalState> 
{
    public hkbGetUpModifierInternalStateData(HavokType type, hkbGetUpModifierInternalState instance) : base(type, instance) {}

    public override bool TryGetField<TGet>(string fieldName, [MaybeNull] out TGet value)
    {
        value = default;
        switch (fieldName)
        {
            case "m_propertyBag":
            case "propertyBag":
            {
                if (instance.m_propertyBag is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            case "m_timeSinceBegin":
            case "timeSinceBegin":
            {
                if (instance.m_timeSinceBegin is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            case "m_timeStep":
            case "timeStep":
            {
                if (instance.m_timeStep is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            case "m_initNextModify":
            case "initNextModify":
            {
                if (instance.m_initNextModify is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            default:
            return false;
        }
    }

    public override bool TrySetField<TSet>(string fieldName, TSet value)
    {
        switch (fieldName)
        {
            case "m_propertyBag":
            case "propertyBag":
            {
                if (value is not hkPropertyBag castValue) return false;
                instance.m_propertyBag = castValue;
                return true;
            }
            case "m_timeSinceBegin":
            case "timeSinceBegin":
            {
                if (value is not float castValue) return false;
                instance.m_timeSinceBegin = castValue;
                return true;
            }
            case "m_timeStep":
            case "timeStep":
            {
                if (value is not float castValue) return false;
                instance.m_timeStep = castValue;
                return true;
            }
            case "m_initNextModify":
            case "initNextModify":
            {
                if (value is not bool castValue) return false;
                instance.m_initNextModify = castValue;
                return true;
            }
            default:
            return false;
        }
    }

}
