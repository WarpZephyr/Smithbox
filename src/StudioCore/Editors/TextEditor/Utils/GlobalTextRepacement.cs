﻿using ImGuiNET;
using StudioCore.Editor;
using StudioCore.Interface;
using StudioCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StudioCore.Editors.TextEditor.Utils;

public static class GlobalTextReplacement
{
    private static string _globalSearchInput = "";
    private static string _globalSearchReplace = "";
    private static bool IgnoreCase = false;
    private static SearchFilterType FilterType = SearchFilterType.PrimaryCategory;

    private static List<TextResult> SearchResults = new();

    public static void Display()
    {
        var windowWidth = ImGui.GetWindowWidth();
        var defaultButtonSize = new Vector2(windowWidth, 32);

        // TODO: add replace term, search results are preview, add apply button

        if (ImGui.BeginTable($"globalReplacementTable", 2, ImGuiTableFlags.SizingFixedFit))
        {
            ImGui.TableSetupColumn("Title", ImGuiTableColumnFlags.WidthFixed);
            ImGui.TableSetupColumn("Contents", ImGuiTableColumnFlags.WidthStretch);
            //ImGui.TableHeadersRow();

            // Row 1
            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(0);

            ImGui.Text("Conditional Input");
            UIHelper.ShowHoverTooltip("The regex you want to match with.");

            ImGui.TableSetColumnIndex(1);

            ImGui.SetNextItemWidth(ImGui.GetColumnWidth());
            ImGui.InputText("##globalSearchInput", ref _globalSearchInput, 255);

            // Row 2
            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(0);

            ImGui.Text("Replacement Input");
            UIHelper.ShowHoverTooltip("The regex you want to replace with.");

            ImGui.TableSetColumnIndex(1);

            ImGui.SetNextItemWidth(ImGui.GetColumnWidth());
            ImGui.InputText("##globalSearchInput", ref _globalSearchInput, 255);

            // Row 3
            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(0);

            ImGui.Text("Filter Type");

            ImGui.TableSetColumnIndex(1);

            ImGui.SetNextItemWidth(ImGui.GetColumnWidth());
            if (ImGui.BeginCombo("##searchFilterType", FilterType.GetDisplayName()))
            {
                foreach (var entry in Enum.GetValues(typeof(SearchFilterType)))
                {
                    var filterEntry = (SearchFilterType)entry;

                    if (ImGui.Selectable(filterEntry.GetDisplayName()))
                    {
                        FilterType = filterEntry;
                    }
                }

                ImGui.EndCombo();
            }
            UIHelper.ShowHoverTooltip("The search filter to use.");

            // Row 3
            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(0);

            ImGui.Text("Ignore Case");

            ImGui.TableSetColumnIndex(1);

            ImGui.Checkbox("##ignoreCase", ref IgnoreCase);
            UIHelper.ShowHoverTooltip("Ignore case sensitivity if enabled.");

            ImGui.EndTable();
        }

        if (ImGui.Button("Search##executeSearch", UI.GetStandardHalfButtonSize()))
        {
            SearchResults = TextFinder.GetGlobalTextResult(_globalSearchInput, FilterType, IgnoreCase);
        }
        ImGui.SameLine();
        if (ImGui.Button("Clear##clearSearchResults", UI.GetStandardHalfButtonSize()))
        {
            SearchResults.Clear();
        }

        ImGui.Separator();

        ImGui.Separator();

        if (SearchResults.Count > 0)
        {
            UIHelper.WrappedText("Entries that will be affected:");

            var index = 0;

            foreach (var result in SearchResults)
            {
                var foundText = result.Entry.Text;
                if (foundText.Contains("\n"))
                {
                    var firstSection = foundText.Split("\n")[0];
                    foundText = $"{firstSection} <...>";
                }

                var category = result.Info.Category.ToString();

                // Container
                var containerName = result.ContainerName;
                if (CFG.Current.TextEditor_DisplayPrettyContainerName)
                {
                    containerName = TextUtils.GetPrettyContainerName(result.ContainerName);
                }

                // FMG
                var fmgName = result.FmgName;
                if (CFG.Current.TextEditor_DisplayFmgPrettyName)
                {
                    fmgName = TextUtils.GetFmgDisplayName(result.Info, result.FmgID, result.FmgName);
                }

                var displayText = $"{containerName} - {fmgName} - {result.Entry.ID}: {foundText}";

                if (ImGui.Selectable(displayText))
                {
                    EditorCommandQueue.AddCommand($"text/select/{category}/{result.ContainerName}/{result.FmgName}/{result.Entry.ID}");
                }
            }

            ImGui.Separator();

            if(ImGui.Button("Replace"))
            {

            }
        }
    }
}