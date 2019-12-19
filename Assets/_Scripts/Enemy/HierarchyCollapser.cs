
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;

public static class HierarchyCollapser
{
    [MenuItem("Tools/Collapse Hierarchy _F4")]
    private static void Collapse()
    {
        var nonPublicStatic = BindingFlags.NonPublic | BindingFlags.Static;
        var nonPublicInstance = BindingFlags.NonPublic | BindingFlags.Instance;
        var assemblypath = InternalEditorUtility.GetEditorAssemblyPath();
        var assembly = Assembly.LoadFrom(assemblypath);
        var type = assembly.GetType("UnityEditor.SceneHierarchyWindow");
        var hierarchyfield = type.GetField("s_LastInteractedHierarchy", nonPublicStatic);
        var hierarchy = hierarchyfield.GetValue(type);
        var hierarchyType = hierarchy.GetType();
        var sceneviewfield = hierarchyType.GetField("m_SceneHierarchy", nonPublicInstance);
        var sceneview = sceneviewfield.GetValue(hierarchy);
        var sceneviewType = sceneview.GetType();
        var treestatefield = sceneviewType.GetField("m_TreeViewState", nonPublicInstance);
        var treestate = treestatefield.GetValue(sceneview);
        var expandedIdsField = treestate.GetType().GetField("m_ExpandedIDs", nonPublicInstance);

        expandedIdsField.SetValue(treestate, new List<int>());

        var openscenes = new List<string>();

        for (int i = 0; i < EditorSceneManager.sceneCount; i++)
        {
            openscenes.Add(EditorSceneManager.GetSceneAt(i).name);
        }

        var reloadneeded = sceneviewType.GetField("m_TreeViewReloadNeeded", nonPublicInstance);

        sceneviewType.InvokeMember
        (
            name: "SetScenesExpanded",
            invokeAttr: BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            target: sceneview,
            args: new object[1] { openscenes }
        );

        reloadneeded.SetValue(sceneview, true);

        sceneviewType.InvokeMember
        (
            name: "SyncIfNeeded",
            invokeAttr: BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            target: sceneview,
            args: null
        );

    }
}