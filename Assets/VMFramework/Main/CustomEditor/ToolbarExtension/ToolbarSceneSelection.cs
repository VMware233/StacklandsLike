#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolbarExtender;

namespace VMFramework.Editor
{
    [InitializeOnLoad]
    public class ToolbarSceneSelection
    {
        private static SceneData[] scenesPopupDisplay;
        private static string[] scenesPath;
        private static string[] scenesBuildPath;
        private static int selectedSceneIndex;

        private static readonly List<SceneData> toDisplay = new();
        private static string[] sceneGuids;
        private static Scene activeScene;
        private static int usedIds;
        private static string name;
        private static GUIContent content;
        private static bool isPlaceSeparator;

        static ToolbarSceneSelection()
        {
            RefreshScenesList();
            EditorSceneManager.sceneOpened -= HandleSceneOpened;
            EditorSceneManager.sceneOpened += HandleSceneOpened;
			
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
            DrawSceneDropdown();
            EditorGUI.EndDisabledGroup();
        }

        private static void DrawSceneDropdown()
        {
            selectedSceneIndex = EditorGUILayout.Popup(selectedSceneIndex,
                scenesPopupDisplay.Select(e => e.popupDisplay).ToArray(), GUILayout.Width(200));

            if (GUI.changed && 0 <= selectedSceneIndex && selectedSceneIndex < scenesPopupDisplay.Length)
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    foreach (var scenePath in scenesPath)
                    {
                        if ((scenePath) == scenesPopupDisplay[selectedSceneIndex].path)
                        {
                            EditorSceneManager.OpenScene(scenePath);
                            break;
                        }
                    }
                }
            }

        }

        private static void RefreshScenesList()
        {
            InitScenesData();

            //Scenes in build settings
            foreach (var path in scenesBuildPath)
            {
                AddScene(path);
            }

            //Scenes on Assets/Scenes/
            isPlaceSeparator = false;
            for (int i = 0; i < scenesPath.Length; ++i)
            {
                if (scenesPath[i].Contains("Assets/Scenes"))
                {
                    PlaceSeparatorIfNeeded();
                    AddScene(scenesPath[i]);
                }
            }

            //Scenes on Plugins/Plugins/
            //Consider them as demo scenes from plugins
            isPlaceSeparator = false;
            for (int i = 0; i < scenesPath.Length; ++i)
            {
                if (scenesPath[i].Contains("Assets/Plugins/"))
                {
                    PlaceSeparatorIfNeeded();
                    AddScene(scenesPath[i], "Plugins demo");
                }
            }

            //All other scenes.
            isPlaceSeparator = false;
            for (int i = 0; i < scenesPath.Length; ++i)
            {
                PlaceSeparatorIfNeeded();
                AddScene(scenesPath[i]);
            }

            scenesPopupDisplay = toDisplay.ToArray();
        }

        private static void AddScene(string path, string prefix = null, string overrideName = null)
        {
            if (!path.Contains(".unity"))
                path += ".unity";

            if (toDisplay.Find(data => path == data.path) != null)
                return;

            if (!string.IsNullOrEmpty(overrideName))
            {
                name = overrideName;
            }
            else
            {
                string folderName = Path.GetFileName(Path.GetDirectoryName(path));
                name = $"{folderName}/{GetSceneName(path)}";
            }

            if (!string.IsNullOrEmpty(prefix))
                name = $"{prefix}/{name}";

            if (scenesBuildPath.Contains(path))
                content = new GUIContent(name, EditorGUIUtility.Load("BuildSettings.Editor.Small") as Texture,
                    "Open scene");
            else
                content = new GUIContent(name, "Open scene");

            toDisplay.Add(new SceneData()
            {
                path = path,
                popupDisplay = content,
            });

            if (selectedSceneIndex == -1 && GetSceneName(path) == activeScene.name)
                selectedSceneIndex = usedIds;
            ++usedIds;
        }

        private static void PlaceSeparatorIfNeeded()
        {
            if (!isPlaceSeparator)
            {
                isPlaceSeparator = true;
                PlaceSeparator();
            }
        }

        private static void PlaceSeparator()
        {
            toDisplay.Add(new SceneData()
            {
                path = "\0",
                popupDisplay = new GUIContent("\0"),
            });
            ++usedIds;
        }

        private static void HandleSceneOpened(Scene scene, OpenSceneMode mode)
        {
            RefreshScenesList();
        }

        private static string GetSceneName(string path)
        {
            path = path.Replace(".unity", "");
            return Path.GetFileName(path);
        }

        private static void InitScenesData()
        {
            toDisplay.Clear();
            selectedSceneIndex = -1;
            scenesBuildPath = EditorBuildSettings.scenes.Select(s => s.path).ToArray();

            sceneGuids = AssetDatabase.FindAssets("t:scene", new string[] { "Assets" });
            scenesPath = new string[sceneGuids.Length];
            for (int i = 0; i < scenesPath.Length; ++i)
                scenesPath[i] = AssetDatabase.GUIDToAssetPath(sceneGuids[i]);

            activeScene = SceneManager.GetActiveScene();
            usedIds = 0;
        }

        private class SceneData
        {
            public string path;
            public GUIContent popupDisplay;
        }
    }
}
#endif