// Automatically Generated

using System.Diagnostics.CodeAnalysis;

namespace HKLib.Reflection.hk2018;

internal class hkaiNavVolumeInstanceDataCellInstanceData : HavokData<HKLib.hk2018.hkaiNavVolumeInstanceData.CellInstance> 
{
    public hkaiNavVolumeInstanceDataCellInstanceData(HavokType type, HKLib.hk2018.hkaiNavVolumeInstanceData.CellInstance instance) : base(type, instance) {}

    public override bool TryGetField<TGet>(string fieldName, [MaybeNull] out TGet value)
    {
        value = default;
        switch (fieldName)
        {
            case "m_startEdgeIndex":
            case "startEdgeIndex":
            {
                if (instance.m_startEdgeIndex is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            case "m_numEdges":
            case "numEdges":
            {
                if (instance.m_numEdges is not TGet castValue) return false;
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
            case "m_startEdgeIndex":
            case "startEdgeIndex":
            {
                if (value is not int castValue) return false;
                instance.m_startEdgeIndex = castValue;
                return true;
            }
            case "m_numEdges":
            case "numEdges":
            {
                if (value is not int castValue) return false;
                instance.m_numEdges = castValue;
                return true;
            }
            default:
            return false;
        }
    }

}
