#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using VMFramework.GameEvents;
using VMFramework.OdinExtensions;

namespace VMFramework.Editor.IndependentGameEventsViewer
{
    internal class IndependentGameEventsViewerContainer : SimpleOdinEditorWindowContainer
    {
        private List<Type> types;
        
        [LabelText("显示模式")]
        [OnValueChanged(nameof(OnShowTypeChanged))]
        [ShowInInspector]
        private ShowType showType = ShowType.All;

        [ListDrawerSettings(ShowFoldout = false)]
        [ShowInInspector]
        private List<TypeInfo> infos = new();

        public override void Init()
        {
            base.Init();

            types = IndependentGameEventCollector.Collect().ToList();
            
            OnShowTypeChanged();
        }

        private void OnShowTypeChanged()
        {
            var result = new List<Type>();
            
            foreach (var type in types)
            {
                if (showType.HasFlag(ShowType.CustomOnly))
                {
                    if (type.Namespace == null)
                    {
                        result.Add(type);
                        continue;
                    }

                    if (type.Namespace.StartsWith(nameof(VMFramework)) == false)
                    {
                        result.Add(type);
                        continue;
                    }
                }

                if (showType.HasFlag(ShowType.BuiltInOnly))
                {
                    if (type.Namespace != null && type.Namespace.StartsWith(nameof(VMFramework)))
                    {
                        result.Add(type);
                        continue;
                    }
                }
            }

            infos.Clear();
            
            foreach (var type in result)
            {
                infos.Add(new TypeInfo()
                {
                    type = type
                });
            }
        }
    }
}
#endif