#if UNITY_EDITOR
using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using VMFramework.Procedure;
using VMFramework.Procedure.Editor;

namespace VMFramework.Editor.GameEditor
{
    internal sealed class GameEditorInitializer : IEditorInitializer
    {
        async void IInitializer.OnInitComplete(Action onDone)
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (EditorWindow.HasOpenInstances<GameEditor>() == false)
            {
                return;
            }

            var gameEditor = EditorWindow.GetWindow<GameEditor>();

            if (gameEditor == null)
            {
                return;
            }

            await UniTask.Delay(1000);

            gameEditor.Repaint();
            gameEditor.ForceMenuTreeRebuild();

            await UniTask.Delay(1000);

            gameEditor.Repaint();
            gameEditor.ForceMenuTreeRebuild();
        }
    }
}

#endif