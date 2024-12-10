﻿using HKLib.hk2018.hkAsyncThreadPool;
using ImGuiNET;
using Microsoft.Extensions.Logging;
using SoulsFormats;
using StudioCore.Core.Project;
using StudioCore.Interface;
using StudioCore.Platform;
using StudioCore.Resource.Locators;
using StudioCore.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudioCore.Editors.ModelEditor;

public class ModelAssetCopyManager
{
    private ModelEditorScreen Screen;

    public ModelAssetCopyManager(ModelEditorScreen screen)
    {
        Screen = screen;
    }

    public bool IsSupportedProjectType()
    {
        if (Smithbox.ProjectType is ProjectType.ER or ProjectType.AC6)
        {
            return true;
        }

        return false;
    }

    private string SourceCharacterName = "";
    private int NewCharacterID = -1;
    private bool ShowNewCharacterMenu = false;

    public void OpenCharacterCopyMenu(string entry)
    {
        SourceCharacterName = entry;
        ShowNewCharacterMenu = true;
    }

    public void CharacterCopyMenu()
    {
        Vector2 buttonSize = new Vector2(200, 24);

        if (ShowNewCharacterMenu)
        {
            ImGui.OpenPopup("Copy as New Character");
        }

        if (ImGui.BeginPopupModal("Copy as New Character", ref ShowNewCharacterMenu, ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoMove))
        {
            ImGui.Text("Target Character:");
            UIHelper.DisplayAlias(SourceCharacterName);

            ImGui.Separator();

            ImGui.Text("New Character ID");
            ImGui.InputInt("##newChrId", ref NewCharacterID, 1);
            UIHelper.ShowHoverTooltip("" +
                "The new ID the copied asset will have.\n\n" +
                "Character IDs must be between 0 and 9999 and not already exist.");

            if (ImGui.Button("Create", buttonSize))
            {
                bool createChr = true;

                string newChrIdStr = $"{NewCharacterID}";

                if (NewCharacterID < 1000)
                    newChrIdStr = $"0{NewCharacterID}";
                if (NewCharacterID < 100)
                    newChrIdStr = $"00{NewCharacterID}";
                if (NewCharacterID < 10)
                    newChrIdStr = $"000{NewCharacterID}";

                if (NewCharacterID >= 0 && NewCharacterID <= 9999)
                {
                    var matchChr = $"c{newChrIdStr}";

                    if (Smithbox.BankHandler.CharacterAliases.Aliases.list.Any(x => x.id == matchChr))
                    {
                        createChr = false;
                        PlatformUtils.Instance.MessageBox($"{matchChr} already exists.", "Warning", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    createChr = false;
                    PlatformUtils.Instance.MessageBox($"{newChrIdStr} is not valid.", "Warning", MessageBoxButtons.OK);
                }

                if (createChr)
                {
                    CreateCharacter(SourceCharacterName, $"c{newChrIdStr}");
                    ShowNewCharacterMenu = false;
                }
            }

            ImGui.SameLine();

            if (ImGui.Button("Close", buttonSize))
            {
                ShowNewCharacterMenu = false;
            }

            ImGui.EndPopup();
        }
    }

    public void CreateCharacter(string copyChr, string newChr)
    {
        if (Smithbox.ProjectType is ProjectType.ER or ProjectType.AC6)
        {
            // ChrBND
            ResourceDescriptor chrBnd = AssetLocator.GetCharacterBinder(copyChr);
            if (chrBnd.AssetPath != null)
                SaveContainer(chrBnd.AssetPath, copyChr, newChr);

            // AniBND
            ResourceDescriptor aniBnd = AssetLocator.GetCharacterAnimationBinder(copyChr);

            if (aniBnd.AssetPath != null)
                SaveContainer(aniBnd.AssetPath, copyChr, newChr);

            // AniBND _div0X
            for (int i = 0; i < 6; i++)
            {
                ResourceDescriptor aniBnd_div0X = AssetLocator.GetCharacterAnimationBinder(copyChr, $"_div0{i}");

                if (aniBnd_div0X.AssetPath != null)
                    SaveContainer(aniBnd_div0X.AssetPath, copyChr, newChr);
            }

            // BehBND
            ResourceDescriptor behBnd = AssetLocator.GetCharacterBehaviorBinder(copyChr);

            if (behBnd.AssetPath != null)
                SaveContainer(behBnd.AssetPath, copyChr, newChr);

            // TexBND _l
            ResourceDescriptor texBnd_l = AssetLocator.GetCharacterTextureBinder(copyChr, "_l");

            if (texBnd_l.AssetPath != null)
                SaveContainer(texBnd_l.AssetPath, copyChr, newChr);

            // TexBND _h
            ResourceDescriptor texBnd_h = AssetLocator.GetCharacterTextureBinder(copyChr, "_h");

            if (texBnd_h.AssetPath != null)
                SaveContainer(texBnd_h.AssetPath, copyChr, newChr);
        }

        // Reload banks so the addition appears in the lists
        Smithbox.BankHandler.ReloadAliasBanks = true;
        Smithbox.AliasCacheHandler.ReloadAliasCaches = true;
    }


    private string SourceAssetName = "";
    private int NewAssetCategoryID = -1;
    private int NewAssetID = -1;
    private bool ShowNewAssetMenu = false;

    public void OpenAssetCopyMenu(string entry)
    {
        SourceAssetName = entry;

        var assetNameSegments = SourceAssetName.Split("_");
        if (assetNameSegments.Length > 1)
        {
            try
            {
                NewAssetCategoryID = int.Parse(assetNameSegments[0]);
            }
            catch(Exception e)
            {
                TaskLogs.AddLog("Failed to convert NewAssetCategoryID string to int.", LogLevel.Error);
            }
        }

        ShowNewAssetMenu = true;
    }

    public void AssetCopyMenu()
    {
        Vector2 buttonSize = new Vector2(200, 24);

        if (ShowNewAssetMenu)
        {
            ImGui.OpenPopup("Copy as New Asset");
        }

        if (ImGui.BeginPopupModal("Copy as New Asset", ref ShowNewAssetMenu, ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoMove))
        {
            ImGui.Text("Target Asset:");
            UIHelper.DisplayAlias(SourceAssetName);

            ImGui.Separator();

            if (Smithbox.ProjectType is ProjectType.ER or ProjectType.AC6)
            {
                ImGui.Text("New Asset Category ID");
                ImGui.InputInt("##newAssetCategoryId", ref NewAssetCategoryID, 1);
                UIHelper.ShowHoverTooltip("" +
                    "The category ID the copied asset will have.\n\n" +
                    "Asset category IDs must be between 0 and 999.");
            }

            ImGui.Text("New Asset ID");
            ImGui.InputInt("##newAssetId", ref NewAssetID, 1);
            UIHelper.ShowHoverTooltip("" +
                "The asset ID the copied asset will have.\n\n" +
                "Asset IDs must be between 0 and 999.");

            if (ImGui.Button("Create", buttonSize))
            {
                bool createAsset = true;

                var prefix = "";
                var matchAsset = "";

                if (Smithbox.ProjectType is ProjectType.ER or ProjectType.AC6)
                {
                    prefix = "aeg";

                    string newAssetCategoryIdStr = $"{NewAssetCategoryID}";
                    if (NewAssetCategoryID < 100)
                        newAssetCategoryIdStr = $"0{NewAssetCategoryID}";
                    if (NewAssetCategoryID < 10)
                        newAssetCategoryIdStr = $"00{NewAssetCategoryID}";

                    string newAssetIdStr = $"{NewAssetID}";
                    if (NewAssetID < 100)
                        newAssetIdStr = $"0{NewAssetID}";
                    if (NewAssetID < 10)
                        newAssetIdStr = $"00{NewAssetID}";

                    matchAsset = $"{newAssetCategoryIdStr}_{newAssetIdStr}";

                    if (matchAsset != "" &&
                        NewAssetID >= 0 && NewAssetID <= 999 &&
                        NewAssetCategoryID >= 0 && NewAssetCategoryID <= 999)
                    {
                        if (Smithbox.BankHandler.AssetAliases.Aliases.list.Any(x => x.id == matchAsset))
                        {
                            createAsset = false;
                            PlatformUtils.Instance.MessageBox($"{matchAsset} already exists.", "Warning", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        createAsset = false;
                        PlatformUtils.Instance.MessageBox($"{matchAsset} is not valid.", "Warning", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    prefix = "o";

                    string newAssetIdStr = $"{NewAssetID}";
                    if (NewAssetID < 100000)
                        newAssetIdStr = $"0{NewAssetID}";
                    if (NewAssetID < 10000)
                        newAssetIdStr = $"00{NewAssetID}";
                    if (NewAssetID < 1000)
                        newAssetIdStr = $"000{NewAssetID}";
                    if (NewAssetID < 100)
                        newAssetIdStr = $"0000{NewAssetID}";
                    if (NewAssetID < 10)
                        newAssetIdStr = $"00000{NewAssetID}";

                    matchAsset = $"{newAssetIdStr}";

                    if (matchAsset != "" &&
                        NewAssetID >= 0 && NewAssetID <= 999999)
                    {
                        if (Smithbox.BankHandler.AssetAliases.Aliases.list.Any(x => x.id == matchAsset))
                        {
                            createAsset = false;
                            PlatformUtils.Instance.MessageBox($"{matchAsset} already exists.", "Warning", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        createAsset = false;
                        PlatformUtils.Instance.MessageBox($"{matchAsset} is not valid.", "Warning", MessageBoxButtons.OK);
                    }
                }

                if (createAsset)
                {
                    CreateAsset(SourceAssetName, $"{prefix}{matchAsset}");
                    ShowNewAssetMenu = false;
                }
            }

            ImGui.SameLine();

            if (ImGui.Button("Close", buttonSize))
            {
                ShowNewAssetMenu = false;
            }

            ImGui.EndPopup();
        }
    }

    public void CreateAsset(string copyAsset, string newAsset)
    {
        if (Smithbox.ProjectType is ProjectType.ER or ProjectType.AC6)
        {
            // GeomBND
            ResourceDescriptor assetGeom = AssetLocator.GetAssetGeomBinder(copyAsset);
            if (assetGeom.AssetPath != null)
                SaveContainer(assetGeom.AssetPath, copyAsset, newAsset, true);

            // GeomHKXBND _l
            ResourceDescriptor assetGeomHKX_l = AssetLocator.GetAssetGeomHKXBinder(copyAsset, "_l");

            if (assetGeomHKX_l.AssetPath != null)
                SaveContainer(assetGeomHKX_l.AssetPath, copyAsset, newAsset, true);

            // GeomHKXBND _h
            ResourceDescriptor assetGeomHKX_h = AssetLocator.GetAssetGeomHKXBinder(copyAsset, "_h");

            if (assetGeomHKX_h.AssetPath != null)
                SaveContainer(assetGeomHKX_h.AssetPath, copyAsset, newAsset, true);
        }

        // Reload banks so the addition appears in the lists
        Smithbox.BankHandler.ReloadAliasBanks = true;
        Smithbox.AliasCacheHandler.ReloadAliasCaches = true;
    }

    private string SourcePartName = "";
    private string NewPartType = "";
    private string NewPartGender = "";
    private int NewPartID = -1;
    private bool ShowNewPartMenu = false;

    public void OpenPartCopyMenu(string entry)
    {
        SourcePartName = entry;

        var partNameSegments = SourcePartName.Split("_");
        if(partNameSegments.Length > 2)
        {
            NewPartType = partNameSegments[0];
            NewPartGender = partNameSegments[1];
        }

        ShowNewPartMenu = true;
    }

    public void PartCopyMenu()
    {
        Vector2 buttonSize = new Vector2(200, 24);

        if (ShowNewPartMenu)
        {
            ImGui.OpenPopup("Copy as New Part");
        }

        if (ImGui.BeginPopupModal("Copy as New Part", ref ShowNewPartMenu, ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoMove))
        {
            ImGui.Text("Target Part:");
            UIHelper.DisplayAlias(SourcePartName);

            ImGui.Separator();

            ImGui.Text("New Part Type");
            ImGui.InputText("##newPartType", ref NewPartType, 1);
            UIHelper.ShowHoverTooltip("" +
                "The part type string the copied part will have.\n\n" +
                "Part Type string should be hd, fc, bd, am or lg in most cases.");

            ImGui.Text("New Part Gender");
            ImGui.InputText("##newPartGender", ref NewPartGender, 1);
            UIHelper.ShowHoverTooltip("" +
                "The part gender string the copied part will have.\n\n" +
                "Part Gender string should be m, f or a most cases.");

            ImGui.Text("New Part ID");
            ImGui.InputInt("##newPartId", ref NewPartID, 1);
            UIHelper.ShowHoverTooltip("" +
                "The part ID the copied part will have.\n\n" +
                "Part IDs must be between 0 and 9999.");

            if (ImGui.Button("Create", buttonSize))
            {
                bool createPart = true;

                string newPartIdStr = $"{NewPartID}";

                if (NewPartID < 1000)
                    newPartIdStr = $"0{NewPartID}";
                if (NewPartID < 100)
                    newPartIdStr = $"00{NewPartID}";
                if (NewPartID < 10)
                    newPartIdStr = $"000{NewPartID}";

                var matchPart = $"{NewPartType}_{NewPartGender}_{newPartIdStr}";

                if (NewPartID >= 0 && NewPartID <= 9999)
                {
                    if (Smithbox.BankHandler.PartAliases.Aliases.list.Any(x => x.id == matchPart))
                    {
                        createPart = false;
                        PlatformUtils.Instance.MessageBox($"{matchPart} already exists.", "Warning", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    createPart = false;
                    PlatformUtils.Instance.MessageBox($"{matchPart} is not valid.", "Warning", MessageBoxButtons.OK);
                }

                if (createPart)
                {
                    CreatePart(SourcePartName, $"{matchPart}");
                    ShowNewPartMenu = false;
                }
            }

            ImGui.SameLine();

            if (ImGui.Button("Close", buttonSize))
            {
                ShowNewPartMenu = false;
            }

            ImGui.EndPopup();
        }
    }

    public void CreatePart(string copyPart, string newPart)
    {
        if (Smithbox.ProjectType is ProjectType.ER or ProjectType.AC6)
        {
            // PartBND
            ResourceDescriptor partBnd = AssetLocator.GetPartBinder(copyPart);
            if (partBnd.AssetPath != null)
                SaveContainer(partBnd.AssetPath, copyPart, newPart, true);

            // PartBND _l
            ResourceDescriptor partBnd_l = AssetLocator.GetPartBinder(copyPart, "_l");
            if (partBnd_l.AssetPath != null)
                SaveContainer(partBnd_l.AssetPath, copyPart, newPart, true);

            // PartBND _u
            ResourceDescriptor partBnd_u = AssetLocator.GetPartBinder(copyPart, "_u");
            if (partBnd_u.AssetPath != null)
                SaveContainer(partBnd_u.AssetPath, copyPart, newPart, true);
        }

        if (Smithbox.ProjectType is ProjectType.AC6)
        {
            // TPF
            ResourceDescriptor partTpf = AssetLocator.GetPartTpf(copyPart, "");
            if (partTpf.AssetPath != null)
                SaveFile(partTpf.AssetPath, copyPart, newPart, true);

            // TPF _l
            ResourceDescriptor partTpf_l = AssetLocator.GetPartTpf(copyPart, "_l");
            if (partTpf_l.AssetPath != null)
                SaveFile(partTpf_l.AssetPath, copyPart, newPart, true);

            // TPF _u
            ResourceDescriptor partTpf_u = AssetLocator.GetPartTpf(copyPart, "_u");
            if (partTpf_u.AssetPath != null)
                SaveFile(partTpf_u.AssetPath, copyPart, newPart, true);
        }

        // Reload banks so the addition appears in the lists
        Smithbox.BankHandler.ReloadAliasBanks = true;
        Smithbox.AliasCacheHandler.ReloadAliasCaches = true;
    }

    public void CreateMapPiece(string copyMapPiece, string newMapPiece, string mapId)
    {
    }

    private void SaveFile(string binderPath, string oldId, string newId, bool uppercaseReplace = false)
    {
        var rootFilePath = binderPath;
        var newFilePath = binderPath.Replace(oldId, newId);
        newFilePath = newFilePath.Replace(Smithbox.GameRoot, Smithbox.ProjectRoot);

        var newBinderDirectory = Path.GetDirectoryName(newFilePath);

        if (!Directory.Exists(newBinderDirectory))
        {
            Directory.CreateDirectory(newBinderDirectory);
        }

        File.Copy(rootFilePath, newFilePath, true);
    }

    private void SaveContainer(string binderPath, string oldId, string newId, bool uppercaseReplace = false)
    {
        var newBinderPath = binderPath.Replace(oldId, newId);
        newBinderPath = newBinderPath.Replace(Smithbox.GameRoot, Smithbox.ProjectRoot);

        var newBinderDirectory = Path.GetDirectoryName(newBinderPath);

        if (!Directory.Exists(newBinderDirectory))
        {
            Directory.CreateDirectory(newBinderDirectory);
        }

        if (Smithbox.ProjectType is ProjectType.DS1 or ProjectType.DS1R)
        {
            byte[] fileBytes = null;

            using (IBinder binder = BND3.Read(DCX.Decompress(binderPath)))
            {
                foreach (var file in binder.Files)
                {
                    if (file.Name.ToLower().Contains(oldId.ToLower()))
                    {
                        if (uppercaseReplace)
                        {
                            file.Name = file.Name.Replace(oldId.ToUpper(), newId.ToUpper());
                        }
                        else
                        {
                            file.Name = file.Name.Replace(oldId, newId);
                        }
                    }
                }

                // Then write those bytes to file
                BND3 writeBinder = binder as BND3;

                switch (Smithbox.ProjectType)
                {
                    case ProjectType.DS1:
                    case ProjectType.DS1R:
                        fileBytes = writeBinder.Write(DCX.Type.DCX_DFLT_10000_24_9);
                        break;
                    default:
                        return;
                }
            }

            if (fileBytes != null)
            {
                File.WriteAllBytes(newBinderPath, fileBytes);
            }
        }
        else
        {
            byte[] fileBytes = null;

            using (IBinder binder = BND4.Read(DCX.Decompress(binderPath)))
            {
                foreach (var file in binder.Files)
                {
                    if (file.Name.ToLower().Contains(oldId.ToLower()))
                    {
                        if (uppercaseReplace)
                        {
                            file.Name = file.Name.Replace(oldId.ToUpper(), newId.ToUpper());
                        }
                        else
                        {
                            file.Name = file.Name.Replace(oldId, newId);
                        }
                    }
                }

                // Then write those bytes to file
                BND4 writeBinder = binder as BND4;

                switch (Smithbox.ProjectType)
                {
                    case ProjectType.DS3:
                        fileBytes = writeBinder.Write(DCX.Type.DCX_DFLT_10000_44_9);
                        break;
                    case ProjectType.SDT:
                        fileBytes = writeBinder.Write(DCX.Type.DCX_KRAK);
                        break;
                    case ProjectType.ER:
                        fileBytes = writeBinder.Write(DCX.Type.DCX_KRAK);
                        break;
                    case ProjectType.AC6:
                        fileBytes = writeBinder.Write(DCX.Type.DCX_KRAK_MAX);
                        break;
                    default:
                        return;
                }
            }

            if (fileBytes != null)
            {
                File.WriteAllBytes(newBinderPath, fileBytes);
            }
        }
    }
}
