// Automatically Generated

using System.Diagnostics.CodeAnalysis;
using HKLib.hk2018;
using HKLib.hk2018.hkaiWorldReplayViewer;

namespace HKLib.Reflection.hk2018;

internal class hkaiWorldReplayViewerConnectedWorldData : HavokData<ConnectedWorld> 
{
    public hkaiWorldReplayViewerConnectedWorldData(HavokType type, ConnectedWorld instance) : base(type, instance) {}

    public override bool TryGetField<TGet>(string fieldName, [MaybeNull] out TGet value)
    {
        value = default;
        switch (fieldName)
        {
            case "m_world":
            case "world":
            {
                if (instance.m_world is null)
                {
                    return true;
                }
                if (instance.m_world is TGet castValue)
                {
                    value = castValue;
                    return true;
                }
                return false;
            }
            case "m_navigators":
            case "navigators":
            {
                if (instance.m_navigators is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            case "m_volumeNavigators":
            case "volumeNavigators":
            {
                if (instance.m_volumeNavigators is not TGet castValue) return false;
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
            case "m_world":
            case "world":
            {
                if (value is null)
                {
                    instance.m_world = default;
                    return true;
                }
                if (value is hkaiWorld castValue)
                {
                    instance.m_world = castValue;
                    return true;
                }
                return false;
            }
            case "m_navigators":
            case "navigators":
            {
                if (value is not List<hkaiNavigator?> castValue) return false;
                instance.m_navigators = castValue;
                return true;
            }
            case "m_volumeNavigators":
            case "volumeNavigators":
            {
                if (value is not List<hkaiVolumeNavigator?> castValue) return false;
                instance.m_volumeNavigators = castValue;
                return true;
            }
            default:
            return false;
        }
    }

}
