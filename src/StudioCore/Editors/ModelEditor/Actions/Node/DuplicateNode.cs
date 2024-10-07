﻿using SoulsFormats;
using StudioCore.Editors.MapEditor;
using StudioCore.Editors.ModelEditor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static SoulsFormats.FLVER2;

namespace StudioCore.Editors.ModelEditor.Actions.Node;

public class DuplicateNode : ViewportAction
{
    private ModelEditorScreen Screen;
    private ModelSelectionManager Selection;
    private ModelViewportManager ViewportManager;

    private FLVER2 CurrentFLVER;

    private FLVER.Node DupedObject;
    private int PreviousSelectionIndex;
    private int Index;

    public DuplicateNode(ModelEditorScreen screen, FLVER2 flver, int index)
    {
        Screen = screen;
        Selection = screen.Selection;
        ViewportManager = screen.ViewportManager;

        PreviousSelectionIndex = screen.Selection._selectedNode;

        CurrentFLVER = flver;
        DupedObject = CurrentFLVER.Nodes[index].Clone();
        Index = flver.Nodes.Count;
    }

    public override ActionEvent Execute(bool isRedo = false)
    {
        CurrentFLVER.Nodes.Insert(Index, DupedObject);
        Selection._selectedNode = Index;

        ViewportManager.UpdateRepresentativeModel(Index);

        return ActionEvent.NoEvent;
    }

    public override ActionEvent Undo()
    {
        Selection._selectedNode = PreviousSelectionIndex;
        CurrentFLVER.Nodes.RemoveAt(Index);

        ViewportManager.UpdateRepresentativeModel(PreviousSelectionIndex);

        return ActionEvent.NoEvent;
    }
}