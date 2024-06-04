// Automatically Generated

using System.Diagnostics.CodeAnalysis;
using HKLib.hk2018;

namespace HKLib.Reflection.hk2018;

internal class hkMemoryResourceContainerData : HavokData<hkMemoryResourceContainer> 
{
    public hkMemoryResourceContainerData(HavokType type, hkMemoryResourceContainer instance) : base(type, instance) {}

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
            case "m_name":
            case "name":
            {
                if (instance.m_name is null)
                {
                    return true;
                }
                if (instance.m_name is TGet castValue)
                {
                    value = castValue;
                    return true;
                }
                return false;
            }
            case "m_resourceHandles":
            case "resourceHandles":
            {
                if (instance.m_resourceHandles is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            case "m_children":
            case "children":
            {
                if (instance.m_children is not TGet castValue) return false;
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
            case "m_name":
            case "name":
            {
                if (value is null)
                {
                    instance.m_name = default;
                    return true;
                }
                if (value is string castValue)
                {
                    instance.m_name = castValue;
                    return true;
                }
                return false;
            }
            case "m_resourceHandles":
            case "resourceHandles":
            {
                if (value is not List<hkMemoryResourceHandle?> castValue) return false;
                instance.m_resourceHandles = castValue;
                return true;
            }
            case "m_children":
            case "children":
            {
                if (value is not List<hkMemoryResourceContainer?> castValue) return false;
                instance.m_children = castValue;
                return true;
            }
            default:
            return false;
        }
    }

}
