﻿using HKLib.hk2018.hkAsyncThreadPool;
using Silk.NET.OpenGL;
using StudioCore.Core.Project;
using StudioCore.Editors.TextEditor.Enums;
using StudioCore.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioCore.Editors.TextEditor;

public static class TextUtils
{
    /// <summary>
    /// Whether the current project type supports the Text Editor
    /// </summary>
    public static bool IsSupportedProjectType()
    {
        return true;
    }

    /// <summary>
    /// Whether the current project supports the passed category
    /// </summary>
    public static bool IsSupportedLanguage(TextContainerCategory category)
    {
        switch (Smithbox.ProjectType)
        {
            case ProjectType.DES:
                if (CategoryGroupings.DES_Languages.Contains(category))
                {
                    return true;
                }
                break;
            case ProjectType.DS1:
            case ProjectType.DS1R:
                if(CategoryGroupings.DS1_Languages.Contains(category))
                {
                    return true;
                }
                break;
            case ProjectType.DS2:
            case ProjectType.DS2S:
                if (CategoryGroupings.DS2_Languages.Contains(category))
                {
                    return true;
                }
                break;
            case ProjectType.BB:
                if (CategoryGroupings.BB_Languages.Contains(category))
                {
                    return true;
                }
                break;
            case ProjectType.DS3:
                if (CategoryGroupings.DS3_Languages.Contains(category))
                {
                    return true;
                }
                break;
            case ProjectType.SDT:
                if (CategoryGroupings.SDT_Languages.Contains(category))
                {
                    return true;
                }
                break;
            case ProjectType.ER:
                if (CategoryGroupings.ER_Languages.Contains(category))
                {
                    return true;
                }
                break;
            case ProjectType.AC6:
                if (CategoryGroupings.AC6_Languages.Contains(category))
                {
                    return true;
                }
                break;

            default: break;
        }

        return false;
    }

    /// <summary>
    /// Get the display name for a FMG based on the BND ID
    /// </summary>
    public static string GetFmgDisplayName(TextContainerInfo info, int id, string fmgName)
    {
        var name = $"Unknown";

        switch(Smithbox.ProjectType)
        {
            case ProjectType.DES:
                if (IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_DES), id))
                    {
                        var enumObj = (Item_MsgBndID_DES)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_DES: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_DES), id))
                    {
                        var enumObj = (Menu_MsgBndID_DES    )id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_DS1: {id} not defined");
                    }
                }
                break;
            case ProjectType.DS1: 
            case ProjectType.DS1R: 
                if(IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_DS1), id))
                    {
                        var enumObj = (Item_MsgBndID_DS1)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_DS1: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_DS1), id))
                    {
                        var enumObj = (Menu_MsgBndID_DS1)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_DS1: {id} not defined");
                    }
                }
                break;
            case ProjectType.DS2: 
            case ProjectType.DS2S:
                if (IsTalkFmg(info))
                {
                    foreach(var entry in Enum.GetValues(typeof(TalkFmgName_DS2)))
                    {
                        var enumVal = (TalkFmgName_DS2)Enum.Parse(typeof(TalkFmgName_DS2), entry.ToString());
                        var internalName = enumVal.ToString();

                        if(internalName == Path.GetFileNameWithoutExtension(fmgName))
                        {
                            name = enumVal.GetDisplayName();
                        }
                    }
                }
                else if (IsBloodMessageFmg(info))
                {
                    foreach (var entry in Enum.GetValues(typeof(BloodMessageFmgName_DS2)))
                    {
                        var enumVal = (BloodMessageFmgName_DS2)Enum.Parse(typeof(BloodMessageFmgName_DS2), entry.ToString());
                        var internalName = enumVal.ToString();

                        if (internalName == Path.GetFileNameWithoutExtension(fmgName))
                        {
                            name = enumVal.GetDisplayName();
                        }
                    }
                }
                else
                {
                    foreach (var entry in Enum.GetValues(typeof(CommonFmgName_DS2)))
                    {
                        var enumVal = (CommonFmgName_DS2)Enum.Parse(typeof(CommonFmgName_DS2), entry.ToString());
                        var internalName = enumVal.ToString();

                        if (internalName == Path.GetFileNameWithoutExtension(fmgName))
                        {
                            name = enumVal.GetDisplayName();
                        }
                    }
                }
                break;
            case ProjectType.BB:
                if (IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_BB), id))
                    {
                        var enumObj = (Item_MsgBndID_BB)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_BB: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_BB), id))
                    {
                        var enumObj = (Menu_MsgBndID_BB)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_BB: {id} not defined");
                    }
                }
                break;
            case ProjectType.DS3:
                if (IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_DS3), id))
                    {
                        var enumObj = (Item_MsgBndID_DS3)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_DS3: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_DS3), id))
                    {
                        var enumObj = (Menu_MsgBndID_DS3)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_DS3: {id} not defined");
                    }
                }
                else if (IsNgWordContainer(info))
                {
                    if (Enum.IsDefined(typeof(NgWord_MsgBndID_DS3), id))
                    {
                        var enumObj = (NgWord_MsgBndID_DS3)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"NgWord_MsgBndID_DS3: {id} not defined");
                    }
                }
                break;
            case ProjectType.SDT:
                if (IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_SDT), id))
                    {
                        var enumObj = (Item_MsgBndID_SDT)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_SDT: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_SDT), id))
                    {
                        var enumObj = (Menu_MsgBndID_SDT)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_SDT: {id} not defined");
                    }
                }
                else if (IsSellRegionContainer(info))
                {
                    if (Enum.IsDefined(typeof(SellRegion_MsgBndID_SDT), id))
                    {
                        var enumObj = (SellRegion_MsgBndID_SDT)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"SellRegion_MsgBndID_SDT: {id} not defined");
                    }
                }
                break;
            case ProjectType.ER:
                if (IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_ER), id))
                    {
                        var enumObj = (Item_MsgBndID_ER)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_ER: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_ER), id))
                    {
                        var enumObj = (Menu_MsgBndID_ER)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_ER: {id} not defined");
                    }
                }
                else if (IsNgWordContainer(info))
                {
                    if (Enum.IsDefined(typeof(NgWord_MsgBndID_ER), id))
                    {
                        var enumObj = (NgWord_MsgBndID_ER)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"NgWord_MsgBndID_ER: {id} not defined");
                    }
                }
                else if (IsSellRegionContainer(info))
                {
                    if (Enum.IsDefined(typeof(SellRegion_MsgBndID_ER), id))
                    {
                        var enumObj = (SellRegion_MsgBndID_ER)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"SellRegion_MsgBndID_ER: {id} not defined");
                    }
                }
                break;
            case ProjectType.AC6:
                if (IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_AC6), id))
                    {
                        var enumObj = (Item_MsgBndID_AC6)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_AC6: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_AC6), id))
                    {
                        var enumObj = (Menu_MsgBndID_AC6)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_AC6: {id} not defined");
                    }
                }
                else if (IsNgWordContainer(info))
                {
                    if (Enum.IsDefined(typeof(NgWord_MsgBndID_AC6), id))
                    {
                        var enumObj = (NgWord_MsgBndID_AC6)id;
                        name = enumObj.GetDisplayName();
                    }
                    else
                    {
                        TaskLogs.AddLog($"NgWord_MsgBndID_AC6: {id} not defined");
                    }
                }
                break;

            default: break;
        }

        return name;
    }

    /// <summary>
    /// Get the internal name for a FMG based on the BND ID
    /// </summary>
    public static string GetFmgInternalName(TextContainerInfo info, int id, string fmgName)
    {
        var name = $"";

        switch (Smithbox.ProjectType)
        {
            case ProjectType.DES:
                if (IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_DES), id))
                    {
                        var enumObj = (Item_MsgBndID_DES)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_DES: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_DES), id))
                    {
                        var enumObj = (Menu_MsgBndID_DES)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_DES: {id} not defined");
                    }
                }
                break;
            case ProjectType.DS1:
            case ProjectType.DS1R:
                if (IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_DS1), id))
                    {
                        var enumObj = (Item_MsgBndID_DS1)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_DS1: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_DS1), id))
                    {
                        var enumObj = (Menu_MsgBndID_DS1)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_DS1: {id} not defined");
                    }
                }
                break;
            case ProjectType.DS2:
            case ProjectType.DS2S:
                if (IsTalkFmg(info))
                {
                    foreach (var entry in Enum.GetValues(typeof(TalkFmgName_DS2)))
                    {
                        var enumVal = (TalkFmgName_DS2)entry;
                        name = enumVal.ToString();
                    }
                }
                else if (IsBloodMessageFmg(info))
                {
                    foreach (var entry in Enum.GetValues(typeof(BloodMessageFmgName_DS2)))
                    {
                        var enumVal = (BloodMessageFmgName_DS2)entry;
                        name = enumVal.ToString();
                    }
                }
                else
                {
                    foreach (var entry in Enum.GetValues(typeof(CommonFmgName_DS2)))
                    {
                        var enumVal = (CommonFmgName_DS2)entry;
                        name = enumVal.ToString();
                    }
                }
                break;
            case ProjectType.BB:
                if (IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_BB), id))
                    {
                        var enumObj = (Item_MsgBndID_BB)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_BB: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_BB), id))
                    {
                        var enumObj = (Menu_MsgBndID_BB)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_BB: {id} not defined");
                    }
                }
                break;
            case ProjectType.DS3:
                if (IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_DS3), id))
                    {
                        var enumObj = (Item_MsgBndID_DS3)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_DS3: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_DS3), id))
                    {
                        var enumObj = (Menu_MsgBndID_DS3)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_DS3: {id} not defined");
                    }
                }
                else if (IsNgWordContainer(info))
                {
                    if (Enum.IsDefined(typeof(NgWord_MsgBndID_DS3), id))
                    {
                        var enumObj = (NgWord_MsgBndID_DS3)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"NgWord_MsgBndID_DS3: {id} not defined");
                    }
                }
                break;
            case ProjectType.SDT:
                if (IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_SDT), id))
                    {
                        var enumObj = (Item_MsgBndID_SDT)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_SDT: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_SDT), id))
                    {
                        var enumObj = (Menu_MsgBndID_SDT)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_SDT: {id} not defined");
                    }
                }
                else if (IsSellRegionContainer(info))
                {
                    if (Enum.IsDefined(typeof(SellRegion_MsgBndID_SDT), id))
                    {
                        var enumObj = (SellRegion_MsgBndID_SDT)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"SellRegion_MsgBndID_SDT: {id} not defined");
                    }
                }
                break;
            case ProjectType.ER:
                if (IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_ER), id))
                    {
                        var enumObj = (Item_MsgBndID_ER)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_ER: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_ER), id))
                    {
                        var enumObj = (Menu_MsgBndID_ER)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_ER: {id} not defined");
                    }
                }
                else if (IsNgWordContainer(info))
                {
                    if (Enum.IsDefined(typeof(NgWord_MsgBndID_ER), id))
                    {
                        var enumObj = (NgWord_MsgBndID_ER)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"NgWord_MsgBndID_ER: {id} not defined");
                    }
                }
                else if (IsSellRegionContainer(info))
                {
                    if (Enum.IsDefined(typeof(SellRegion_MsgBndID_ER), id))
                    {
                        var enumObj = (SellRegion_MsgBndID_ER)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"SellRegion_MsgBndID_ER: {id} not defined");
                    }
                }
                break;
            case ProjectType.AC6:
                if (IsItemContainer(info))
                {
                    if (Enum.IsDefined(typeof(Item_MsgBndID_AC6), id))
                    {
                        var enumObj = (Item_MsgBndID_AC6)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Item_MsgBndID_AC6: {id} not defined");
                    }
                }
                else if (IsMenuContainer(info))
                {
                    if (Enum.IsDefined(typeof(Menu_MsgBndID_AC6), id))
                    {
                        var enumObj = (Menu_MsgBndID_AC6)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"Menu_MsgBndID_AC6: {id} not defined");
                    }
                }
                else if (IsNgWordContainer(info))
                {
                    if (Enum.IsDefined(typeof(NgWord_MsgBndID_AC6), id))
                    {
                        var enumObj = (NgWord_MsgBndID_AC6)id;
                        name = enumObj.ToString();
                    }
                    else
                    {
                        TaskLogs.AddLog($"NgWord_MsgBndID_AC6: {id} not defined");
                    }
                }
                break;

            default: break;
        }

        return name;
    }

    /// <summary>
    /// Check if FMG container is an item FMG container
    /// </summary>
    public static bool IsItemContainer(TextContainerInfo info)
    {
        if (info.AbsolutePath.Contains("item"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Check if FMG container is an menu FMG container
    /// </summary>
    public static bool IsMenuContainer(TextContainerInfo info)
    {
        if (info.AbsolutePath.Contains("menu"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Check if FMG container is an ngword FMG container
    /// </summary>
    public static bool IsNgWordContainer(TextContainerInfo info)
    {
        if (info.AbsolutePath.Contains("ngword"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// BB onwards: check if FMG container is a sellregion 
    /// </summary>
    public static bool IsSellRegionContainer(TextContainerInfo info)
    {
        if (info.AbsolutePath.Contains("sellregion"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// DS2 only: check if FMG is part of the bloodmes folder
    /// </summary>
    public static bool IsBloodMessageFmg(TextContainerInfo info)
    {
        if (info.AbsolutePath.Contains("bloodmes"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// DS2 only: check if FMG is part of the ta;l folder
    /// </summary>
    public static bool IsTalkFmg(TextContainerInfo info)
    {
        if (info.AbsolutePath.Contains("talk"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Determine the container category based on the container filepath.
    /// Used to determine the 'language' grouping each container belongs to.
    /// </summary>
    public static TextContainerCategory GetLanguageCategory(string path)
    {
        var group = TextContainerCategory.None;

        // Special-case: DES the msg folder has Japanese, so default to Japanese.
        if(Smithbox.ProjectType is ProjectType.DES)
        {
            group = TextContainerCategory.Japanese;
        }

        // Common
        if (path.Contains("common"))
        {
            group = TextContainerCategory.Common;
        }

        // English (US)
        if (path.Contains("ENGLISH") ||
            path.Contains("english") ||
            path.Contains("engus"))
        {
            group = TextContainerCategory.English;
        }

        // English (UK)
        if (path.Contains("enggb"))
        {
            group = TextContainerCategory.EnglishUK;
        }

        // French
        if (path.Contains("FRENCH") ||
            path.Contains("french") ||
            path.Contains("frafr"))
        {
            group = TextContainerCategory.French;
        }

        // German
        if (path.Contains("GERMAN") ||
            path.Contains("germany") ||
            path.Contains("deude"))
        {
            group = TextContainerCategory.German;
        }

        // Italian
        if (path.Contains("ITALIAN") ||
            path.Contains("italian") ||
            path.Contains("itait"))
        {
            group = TextContainerCategory.Italian;
        }

        // Japanese
        if (path.Contains("JAPANESE") ||
            path.Contains("japanese") ||
            path.Contains("jpnjp"))
        {
            group = TextContainerCategory.Japanese;
        }

        // Korean
        if (path.Contains("KOREAN") ||
            path.Contains("korean") ||
            path.Contains("korkr"))
        {
            group = TextContainerCategory.Korean;
        }

        // Polish
        if (path.Contains("POLISH") ||
            path.Contains("polish") ||
            path.Contains("polpl"))
        {
            group = TextContainerCategory.Polish;
        }

        // Russian
        if (path.Contains("RUSSIAN") ||
            path.Contains("russian") ||
            path.Contains("rusru"))
        {
            group = TextContainerCategory.Russian;
        }

        // Spanish
        if (path.Contains("SPANISH") ||
            path.Contains("spanish") ||
            path.Contains("spaes"))
        {
            group = TextContainerCategory.Spanish;
        }

        // Spanish (Neutral)
        if (path.Contains("neutralspanish"))
        {
            group = TextContainerCategory.SpanishNeutral;
        }

        // Spanish (Latin)
        if (path.Contains("spaar"))
        {
            group = TextContainerCategory.SpanishLatin;
        }

        // Traditional Chinese
        if (path.Contains("TCHINESE") ||
            path.Contains("chinese") ||
            path.Contains("zhotw"))
        {
            group = TextContainerCategory.TraditionalChinese;
        }

        // Simplified Chinese
        if (path.Contains("zhocn"))
        {
            group = TextContainerCategory.SimplifiedChinese;
        }

        // Danish
        if (path.Contains("dandk"))
        {
            group = TextContainerCategory.Danish;
        }

        // Finnish
        if (path.Contains("finfi"))
        {
            group = TextContainerCategory.Finnish;
        }

        // Dutch
        if (path.Contains("nldnl"))
        {
            group = TextContainerCategory.Dutch;
        }

        // Norwegian
        if (path.Contains("norno"))
        {
            group = TextContainerCategory.Norwegian;
        }

        // Swedish
        if (path.Contains("swese"))
        {
            group = TextContainerCategory.Swedish;
        }

        // Turkish
        if (path.Contains("turtr"))
        {
            group = TextContainerCategory.Turkish;
        }

        // Portuguese (Latin)
        if (path.Contains("portuguese") ||
            path.Contains("porbr"))
        {
            group = TextContainerCategory.PortugueseLatin;
        }

        // Portuguese 
        if (path.Contains("porpt"))
        {
            group = TextContainerCategory.Portuguese;
        }

        // Arabic 
        if (path.Contains("araae"))
        {
            group = TextContainerCategory.Arabic;
        }

        // Thai 
        if (path.Contains("thath"))
        {
            group = TextContainerCategory.Thai;
        }

        // Sell Region
        if (path.Contains("sellregion"))
        {
            group = TextContainerCategory.SellRegion;
        }

        return group;
    }

    /// <summary>
    /// Get a pretty name for the container row name
    /// </summary>
    public static string GetPrettyContainerName(string name)
    {
        var prettyName = name;

        if (name.Contains("item"))
            prettyName = "Item";

        if (name.Contains("menu"))
            prettyName = "Menu";

        if (name.Contains("sellregion"))
            prettyName = "Sell Region";

        if (name.Contains("ngword"))
            prettyName = "Blocked Words";

        if (name.Contains("dlc01") || name.Contains("dlc1"))
            prettyName = $"{prettyName} - DLC 1";

        if (name.Contains("dlc02") || name.Contains("dlc2"))
            prettyName = $"{prettyName} - DLC 2";

        if(Smithbox.ProjectType is ProjectType.DES)
        {
            if (name.Contains("sample"))
                prettyName = "Sample";

            // DES has compressed and uncompressed versions, so add some extra text so it is more obvious which is which
            if (name.Contains(".dcx"))
            {
                prettyName = $"{prettyName} [Compressed]";
            }
        }

        return prettyName;
    }

    /// <summary>
    /// Get the sub category for DS2 fgms
    /// </summary>
    public static DS2_SubCategory GetSubCategory(string path)
    {
        if(path.Contains("bloodmes"))
        {
            return DS2_SubCategory.bloodmes;
        }

        if (path.Contains("talk"))
        {
            return DS2_SubCategory.talk;
        }

        return DS2_SubCategory.common;
    }

}
