#if ODIN_INSPECTOR
using System;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.OdinExtensions
{
    public sealed class UGUINameAttribute : GeneralValueDropdownAttribute
    {
        public Type[] UGUITypes { get; }

        public UGUINameAttribute(params Type[] uguiTypes)
        {
            UGUITypes = uguiTypes;
            
            Check();
        }

        public UGUINameAttribute()
        {
            UGUITypes = new[] { typeof(RectTransform) };
            
            Check();
        }

        private void Check()
        {
            foreach (var type in UGUITypes)
            {
                if (type.IsDerivedFrom<Component>(true) == false)
                {
                    Debug.LogError($"Type {type.Name} is not a derived class of Component.");
                }
            }
        }
    }
}
#endif