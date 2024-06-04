// Automatically Generated

using System.Diagnostics.CodeAnalysis;
using HKLib.hk2018;

namespace HKLib.Reflection.hk2018;

internal class hkxAnimatedVectorData : HavokData<hkxAnimatedVector> 
{
    public hkxAnimatedVectorData(HavokType type, hkxAnimatedVector instance) : base(type, instance) {}

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
            case "m_vectors":
            case "vectors":
            {
                if (instance.m_vectors is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            case "m_hint":
            case "hint":
            {
                if (instance.m_hint is TGet castValue)
                {
                    value = castValue;
                    return true;
                }
                if ((byte)instance.m_hint is TGet byteValue)
                {
                    value = byteValue;
                    return true;
                }
                return false;
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
            case "m_vectors":
            case "vectors":
            {
                if (value is not List<float> castValue) return false;
                instance.m_vectors = castValue;
                return true;
            }
            case "m_hint":
            case "hint":
            {
                if (value is hkxAttribute.Hint castValue)
                {
                    instance.m_hint = castValue;
                    return true;
                }
                if (value is byte byteValue)
                {
                    instance.m_hint = (hkxAttribute.Hint)byteValue;
                    return true;
                }
                return false;
            }
            default:
            return false;
        }
    }

}
