// Automatically Generated

using System.Diagnostics.CodeAnalysis;
using HKLib.hk2018;

namespace HKLib.Reflection.hk2018;

internal class hkContactPointData : HavokData<hkContactPoint> 
{
    public hkContactPointData(HavokType type, hkContactPoint instance) : base(type, instance) {}

    public override bool TryGetField<TGet>(string fieldName, [MaybeNull] out TGet value)
    {
        value = default;
        switch (fieldName)
        {
            case "m_position":
            case "position":
            {
                if (instance.m_position is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            case "m_separatingNormal":
            case "separatingNormal":
            {
                if (instance.m_separatingNormal is not TGet castValue) return false;
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
            case "m_position":
            case "position":
            {
                if (value is not Vector4 castValue) return false;
                instance.m_position = castValue;
                return true;
            }
            case "m_separatingNormal":
            case "separatingNormal":
            {
                if (value is not Vector4 castValue) return false;
                instance.m_separatingNormal = castValue;
                return true;
            }
            default:
            return false;
        }
    }

}
