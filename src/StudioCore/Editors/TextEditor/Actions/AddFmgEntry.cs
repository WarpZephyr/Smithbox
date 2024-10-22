﻿using SoulsFormats;
using StudioCore.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioCore.Editors.TextEditor;

public class AddFmgEntry : EditorAction
{
    private FMG Fmg;
    private FMG.Entry NewEntry;
    private int InsertionIndex;

    private TextContainerWrapper Info;

    public AddFmgEntry(TextContainerWrapper info, FMG.Entry sourceEntry, FMG.Entry newEntry, int newID)
    {
        Info = info;
        Fmg = sourceEntry.Parent;
        NewEntry = newEntry;
        NewEntry.ID = newID;
        InsertionIndex = -1;

        for (int i = 0; i < Fmg.Entries.Count; i++)
        {
            var curEntry = Fmg.Entries[i];

            // Insert below this entry
            if (curEntry.ID > newID)
            {
                InsertionIndex = i;
                break;
            }
        }
    }

    public override ActionEvent Execute()
    {
        if (InsertionIndex != -1)
        {
            Fmg.Entries.Insert(InsertionIndex, NewEntry);
        }
        else
        {
            Fmg.Entries.Add(NewEntry);
        }

        Info.IsModified = true;

        Smithbox.EditorHandler.TextEditor.DifferenceManager.TrackFmgDifferences();

        return ActionEvent.NoEvent;
    }

    public override ActionEvent Undo()
    {
        if (InsertionIndex != -1)
        {
            Fmg.Entries.RemoveAt(InsertionIndex);
        }
        else
        {
            Fmg.Entries.Remove(NewEntry);
        }

        Info.IsModified = false;

        Smithbox.EditorHandler.TextEditor.DifferenceManager.TrackFmgDifferences();

        return ActionEvent.NoEvent;
    }
}