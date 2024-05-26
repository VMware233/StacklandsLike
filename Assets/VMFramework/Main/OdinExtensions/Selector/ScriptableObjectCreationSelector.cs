#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;
using EditorUtility = UnityEditor.EditorUtility;
using Object = UnityEngine.Object;

namespace VMFramework.OdinExtensions
{
    public class ScriptableObjectCreationSelector : ScriptableObjectCreationSelector<ScriptableObject>
    {
        public ScriptableObjectCreationSelector(
            Type scriptableObjectBaseType,
            string defaultDestinationPath,
            Action<ScriptableObject> onScriptableObjectCreated = null
        ) : base(
            scriptableObjectBaseType
                .GetDerivedClasses(true, false)
                .Where(type => type.IsAbstract == false),
            defaultDestinationPath,
            onScriptableObjectCreated
        )
        {

        }
    }
    
    public class ScriptableObjectCreationSelector<T> : TypeSelector
        where T : ScriptableObject
    {
        private readonly Action<T> onCreated;
        private readonly string defaultDestinationPath;

        #region Constructor
        
        public ScriptableObjectCreationSelector(
            IEnumerable<Type> scriptableObjectTypes,
            string defaultDestinationPath,
            Action<T> onCreated)
        {
            this.onCreated = onCreated;
            this.defaultDestinationPath = defaultDestinationPath;
            
            Init(scriptableObjectTypes, ShowSaveFileDialog);
        }
        
        public ScriptableObjectCreationSelector(
            string defaultDestinationPath,
            Action<T> onCreated = null
        ) : this(
            typeof(T).GetDerivedClasses(true, false).Where(type => type.IsAbstract == false),
            defaultDestinationPath,
            onCreated
        )
        {

        }

        #endregion

        #region Show Save File Dialog

        private void ShowSaveFileDialog(Type selectedType)
        {
            var obj =
                ScriptableObject.CreateInstance(selectedType);

            string destination = defaultDestinationPath.TrimEnd('/');

            if (destination.CreateDirectory())
            {
                AssetDatabase.Refresh();
            }

            if (IOUtility.projectFolderPath.TryMakeRelative(destination, out var relativeDestination))
            {
                destination = IOUtility.projectFolderPath.PathCombine(relativeDestination);
            }
            else
            {
                destination = IOUtility.projectFolderPath.PathCombine(destination);
            }

            destination = EditorUtility.SaveFilePanel("Save object as", destination,
                "New " + obj.GetType().GetNiceName(), "asset");

            if (destination.IsNullOrEmpty())
            {
                Debug.LogWarning($"未选择保存路径，创建{selectedType}失败");

                Object.DestroyImmediate(obj);
                return;
            }

            if (obj.TryCreateAsset(destination) == false)
            {
                Object.DestroyImmediate(obj);
            }

            onCreated?.Invoke(obj as T);
        }

        #endregion
    }
}
#endif