using System;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace VMFramework.OdinExtensions
{
    public class LocalizedLabelTextAttribute : Attribute
    {

    }

#if UNITY_EDITOR
    
    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    public class LocalizedLabelTextAttributeDrawer : OdinAttributeDrawer<LocalizedLabelTextAttribute>
    {
        
    }
    
#endif
}
