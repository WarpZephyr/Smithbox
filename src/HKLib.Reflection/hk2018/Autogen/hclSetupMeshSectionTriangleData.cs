// Automatically Generated

using System.Diagnostics.CodeAnalysis;
using HKLib.hk2018;

namespace HKLib.Reflection.hk2018;

internal class hclSetupMeshSectionTriangleData : HavokData<hclSetupMeshSection.Triangle> 
{
    private static readonly System.Reflection.FieldInfo _indicesInfo = typeof(hclSetupMeshSection.Triangle).GetField("m_indices")!;
    public hclSetupMeshSectionTriangleData(HavokType type, hclSetupMeshSection.Triangle instance) : base(type, instance) {}

    public override bool TryGetField<TGet>(string fieldName, [MaybeNull] out TGet value)
    {
        value = default;
        switch (fieldName)
        {
            case "m_indices":
            case "indices":
            {
                if (instance.m_indices is not TGet castValue) return false;
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
            case "m_indices":
            case "indices":
            {
                if (value is not uint[] castValue || castValue.Length != 3) return false;
                try
                {
                    _indicesInfo.SetValue(instance, value);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            default:
            return false;
        }
    }

}
