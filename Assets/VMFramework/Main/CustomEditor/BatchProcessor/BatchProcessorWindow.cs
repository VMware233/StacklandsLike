#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Editor;
using Sirenix.Utilities;
using UnityEngine;
using Sirenix.Utilities.Editor;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using VMFramework.Core.Editor;

public sealed class BatchProcessorWindow : OdinEditorWindow
{
    private BatchProcessorContainer container;

    private static BatchProcessorWindow GetWindow()
    {
        bool hasOpenedWindow = HasOpenInstances<BatchProcessorWindow>();
        var window = GetWindow<BatchProcessorWindow>(BatchProcessorNames.batchProcessorName);

        if (hasOpenedWindow == false)
        {
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(900, 600);
        }

        return window;
    }

    [MenuItem("Tools/" + BatchProcessorNames.BATCH_PROCESSOR_DEFAULT_NAME)]
    public static void OpenWindow()
    {
        GetWindow();
    }

    public static void OpenWindow(IEnumerable<object> selectedObjects)
    {
        GetWindow().container.SetSelectedObjects(selectedObjects);
    }

    public static void AddToWindow(IEnumerable<object> additionalObjects)
    {
        GetWindow().container.AddSelectedObjects(additionalObjects);
    }

    [MenuItem("Assets/" + BatchProcessorNames.BATCH_PROCESSOR_DEFAULT_NAME)]
    [MenuItem("GameObject/" + BatchProcessorNames.BATCH_PROCESSOR_DEFAULT_NAME)]
    public static void OpenWindowFromContextMenu()
    {
        var selectedObjects = Selection.objects;

        if (selectedObjects.Length == 1 && selectedObjects[0].IsFolder())
        {
            OpenWindow(selectedObjects[0].GetAllAssetsInFolder());
            return;
        }

        OpenWindow(Selection.objects);
    }

    protected override void Initialize()
    {
        base.Initialize();

        if (container == null)
        {
            container = CreateInstance<BatchProcessorContainer>();
        }

        container.Init();
    }

    protected override object GetTarget()
    {
        return container;
    }

    protected override void OnImGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            container.UpdateValidUnits();
        }

        base.OnImGUI();
    }
}
#endif