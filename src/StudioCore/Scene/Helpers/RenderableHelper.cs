﻿using StudioCore.Scene.DebugPrimitives;
using StudioCore.Scene.Enums;
using StudioCore.Scene.Framework;
using StudioCore.Scene.Meshes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StudioCore.Scene.Helpers;

public static class RenderableHelper
{
    private static DbgPrimWireBox? _regionBox;
    private static DbgPrimSolidBox? _regionSolidBox;

    private static DbgPrimWireCylinder? _regionCylinder;
    private static DbgPrimSolidCylinder? _regionSolidCylinder;

    private static DbgPrimWireSphere? _regionSphere;
    private static DbgPrimSolidSphere? _regionSolidSphere;

    private static DbgPrimWirePoint? _regionPoint;
    private static DbgPrimSolidPoint? _regionSolidPoint;

    private static DbgPrimWireSphere? _pointLight;
    private static DbgPrimSolidSphere? _pointLightSolid;
    private static DbgPrimWireSpotLight? _spotLight;
    private static DbgPrimWireSpheroidWithArrow? _directionalLight;

    private static DbgPrimWireSpheroidWithArrow? _modelMarkerChr;
    private static DbgPrimWireWallBox? _modelMarkerObj;
    private static DbgPrimWireSpheroidWithArrow? _modelMarkerPlayer;
    private static DbgPrimWireWallBox? _modelMarkerOther;

    private static DbgPrimWireSphere? _dmyPoint;
    private static DbgPrimWireSphereForwardUp? _dmySphereFwdUp;
    private static DbgPrimWireSphere? _jointSphere;

    private static DbgPrimTree? _modelMarkerTree;

    /// <summary>
    /// List of assets starting IDs that will always receive a tree marker.
    /// Contains speedtree AEGs, which currently do not render in Smithbox.
    /// Can be removed when speedtree rendering is functional.
    /// </summary>
    public static readonly HashSet<string> SpeedTree_Assets = new()
    {
        "AEG801",
        "AEG805",
        "AEG810",
        "AEG811",
        "AEG813",
        "AEG814",
        "AEG815",
        "AEG816"
    };

    /// <summary>
    /// Returns true if the passed mesh provider is a Speed Tree asset
    /// </summary>
    public static bool IsSpeedTreeAsset(MeshProvider _meshProvider)
    {
        if (_meshProvider is FlverMeshProvider fProvider)
        {
            if (SpeedTree_Assets.FirstOrDefault(n => fProvider.MeshName.ToUpper().StartsWith(n)) != null)
            {
                if (fProvider.MeshName.ToUpper() != "AEG801_224")
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// These are initialized explicitly to ensure these meshes are created at startup time so that they don't share
    /// vertex buffer memory with dynamically allocated resources and cause the megabuffers to not be freed.
    /// </summary>
    public static void InitializeDebugMeshes()
    {
        // BOX
        _regionSolidBox = new DbgPrimSolidBox(
            Transform.Default,
            new Vector3(-0.5f, 0.0f, -0.5f),
            new Vector3(0.5f, 1.0f, 0.5f),
            Color.Blue);

        _regionBox = new DbgPrimWireBox(
            Transform.Default,
            new Vector3(-0.5f, 0.0f, -0.5f),
            new Vector3(0.5f, 1.0f, 0.5f),
            Color.Blue);

        // CYLINDER
        _regionSolidCylinder = new DbgPrimSolidCylinder(
            Transform.Default,
            1.0f,
            1.0f,
            12,
            Color.Blue);

        _regionCylinder = new DbgPrimWireCylinder(
            Transform.Default,
            1.0f,
            1.0f,
            12,
            Color.Blue);

        // SPHERE
        _regionSolidSphere = new DbgPrimSolidSphere(
            Transform.Default,
            1.0f,
            Color.Blue);

        _regionSphere = new DbgPrimWireSphere(
            Transform.Default,
            1.0f,
            Color.Blue);

        // POINT
        _regionPoint = new DbgPrimWirePoint(
            Transform.Default,
            1.0f,
            Color.Yellow,
            1,
            4);

        _regionSolidPoint = new DbgPrimSolidPoint(
            Transform.Default,
            1.0f,
            Color.Blue);

        // DUMMY POLYGON
        _dmyPoint = new DbgPrimWireSphere(
            Transform.Default,
            0.05f,
            Color.Blue,
            1,
            4);

        _dmySphereFwdUp = new DbgPrimWireSphereForwardUp(
            Transform.Default,
            0.05f,
            Color.Yellow,
            Color.Blue,
            Color.White,
            1,
            4);

        // BONE
        _jointSphere = new DbgPrimWireSphere(
            Transform.Default,
            0.05f,
            Color.Blue,
            6,
            6);

        // MODEL MARKER
        _modelMarkerChr = new DbgPrimWireSpheroidWithArrow(
            Transform.Default,
            0.9f,
            Color.Firebrick,
            4,
            10,
            true);

        _modelMarkerObj = new DbgPrimWireWallBox(
            Transform.Default,
            new Vector3(-1.5f, 0.0f, -0.75f),
            new Vector3(1.5f, 2.5f, 0.75f),
            Color.Firebrick);

        _modelMarkerPlayer = new DbgPrimWireSpheroidWithArrow(
            Transform.Default,
            0.75f,
            Color.Firebrick,
            1,
            6,
            true);

        _modelMarkerOther = new DbgPrimWireWallBox(
            Transform.Default,
            new Vector3(-0.3f, 0.0f, -0.3f),
            new Vector3(0.3f, 1.8f, 0.3f),
            Color.Firebrick);

        _modelMarkerTree = new DbgPrimTree(
            Transform.Default,
            6f,  // Trunk height
            0.5f,   // Trunk width
            3f,   // Cluster radius
            Color.Brown,
            Color.Green);

        // LIGHT
        _pointLight = new DbgPrimWireSphere(
            Transform.Default,
            1.0f,
            Color.Yellow,
            6,
            6);

        _pointLightSolid = new DbgPrimSolidSphere(
            Transform.Default,
            1.0f,
            Color.Yellow,
            6,
            6);

        _spotLight = new DbgPrimWireSpotLight(
            Transform.Default,
            1.0f,
            1.0f,
            Color.Yellow);

        _directionalLight = new DbgPrimWireSpheroidWithArrow(
            Transform.Default,
            5.0f,
            Color.Yellow,
            4,
            2,
            false,
            true);
    }

    // BOX REGION
    public static DebugPrimitiveRenderableProxy GetBoxRegionProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_Box_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_Box_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Default_Wireframe_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _regionBox);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }
    public static DebugPrimitiveRenderableProxy GetSolidBoxRegionProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_Box_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_Box_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Box_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _regionSolidBox);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }


    // CYLINDER REGION
    public static DebugPrimitiveRenderableProxy GetCylinderRegionProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_Cylinder_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_Cylinder_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Default_Wireframe_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _regionCylinder);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }
    public static DebugPrimitiveRenderableProxy GetSolidCylinderRegionProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_Cylinder_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_Cylinder_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Cylinder_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _regionSolidCylinder);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }


    // SPHERE REGION
    public static DebugPrimitiveRenderableProxy GetSphereRegionProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_Sphere_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_Sphere_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Default_Wireframe_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _regionSphere);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }
    public static DebugPrimitiveRenderableProxy GetSolidSphereRegionProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_Sphere_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_Sphere_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Sphere_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _regionSolidSphere);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }

    // POINT REGION
    public static DebugPrimitiveRenderableProxy GetPointRegionProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_Point_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_Point_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Default_Wireframe_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _regionPoint);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }

    public static DebugPrimitiveRenderableProxy GetSolidPointRegionProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_Point_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_Point_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Point_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _regionSolidPoint);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }

    // LIGHT
    public static DebugPrimitiveRenderableProxy GetPointLightProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_PointLight_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_PointLight_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Default_Wireframe_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _pointLight);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }
    public static DebugPrimitiveRenderableProxy GetSolidPointLightProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_PointLight_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_PointLight_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_PointLight_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _pointLightSolid);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }

    public static DebugPrimitiveRenderableProxy GetSpotLightProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_SpotLight_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_SpotLight_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Default_Wireframe_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _spotLight);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }
    public static DebugPrimitiveRenderableProxy GetSolidSpotLightProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_SpotLight_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_SpotLight_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_SpotLight_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _spotLight);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }

    public static DebugPrimitiveRenderableProxy GetDirectionalLightProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_DirectionalLight_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_DirectionalLight_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Default_Wireframe_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _directionalLight);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }
    public static DebugPrimitiveRenderableProxy GetSolidDirectionalLightProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_DirectionalLight_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_DirectionalLight_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_DirectionalLight_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _directionalLight);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }

    // MODEL
    public static DebugPrimitiveRenderableProxy GetModelMarkerProxy(MeshRenderables renderables,
        ModelMarkerType type)
    {
        // Model markers are used as placeholders for meshes that would not otherwise render in the editor
        IDbgPrim? prim;
        Color baseColor;
        Color selectColor;

        switch (type)
        {
            case ModelMarkerType.Enemy:
                prim = _modelMarkerChr;
                baseColor = ColorHelper.GetSolidColor(CFG.Current.GFX_Renderable_ModelMarker_Chr_BaseColor);
                selectColor = ColorHelper.GetSolidColor(CFG.Current.GFX_Renderable_ModelMarker_Chr_HighlightColor);
                break;
            case ModelMarkerType.Object:
                prim = _modelMarkerObj;
                baseColor = ColorHelper.GetSolidColor(CFG.Current.GFX_Renderable_ModelMarker_Object_BaseColor);
                selectColor = ColorHelper.GetSolidColor(CFG.Current.GFX_Renderable_ModelMarker_Object_HighlightColor);
                break;
            case ModelMarkerType.Player:
                prim = _modelMarkerPlayer;
                baseColor = ColorHelper.GetSolidColor(CFG.Current.GFX_Renderable_ModelMarker_Player_BaseColor);
                selectColor = ColorHelper.GetSolidColor(CFG.Current.GFX_Renderable_ModelMarker_Player_HighlightColor);
                break;
            case ModelMarkerType.Other:
            default:
                prim = _modelMarkerOther;
                baseColor = ColorHelper.GetSolidColor(CFG.Current.GFX_Renderable_ModelMarker_Other_BaseColor);
                selectColor = ColorHelper.GetSolidColor(CFG.Current.GFX_Renderable_ModelMarker_Other_HighlightColor);
                break;
        }

        DebugPrimitiveRenderableProxy r = new(renderables, prim, false);
        r.RenderOverlay = true;
        r.BaseColor = baseColor;
        r.HighlightedColor = selectColor;

        return r;
    }

    // MODEL EDITOR
    public static DebugPrimitiveRenderableProxy GetDummyPolyRegionProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_DummyPoly_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_DummyPoly_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Default_Wireframe_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OverlayRenderables, _dmyPoint);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }

    public static DebugPrimitiveRenderableProxy GetDummyPolyForwardUpProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_DummyPoly_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_DummyPoly_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Default_Wireframe_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OpaqueRenderables, _dmySphereFwdUp);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }

    public static DebugPrimitiveRenderableProxy GetBonePointProxy(RenderScene scene)
    {
        var baseColor = CFG.Current.GFX_Renderable_BonePoint_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_BonePoint_HighlightColor;
        var transparency = CFG.Current.GFX_Renderable_Default_Wireframe_Alpha;

        DebugPrimitiveRenderableProxy r = new(scene.OverlayRenderables, _jointSphere);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }

    // Tree
    public static DebugPrimitiveRenderableProxy GetTreeProxy(MeshRenderables renderables)
    {
        var baseColor = CFG.Current.GFX_Renderable_Box_BaseColor;
        var highlightColor = CFG.Current.GFX_Renderable_Box_HighlightColor;
        var transparency = 50.0f;

        DebugPrimitiveRenderableProxy r = new(renderables, _modelMarkerTree, false);

        r.RenderOverlay = true;
        r.BaseColor = ColorHelper.GetTransparencyColor(baseColor, transparency);
        r.HighlightedColor = ColorHelper.GetTransparencyColor(highlightColor, transparency);
        //ColorHelper.ApplyColorVariance(r);

        return r;
    }
}
