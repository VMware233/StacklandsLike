using System;
using UnityEngine;

namespace VMFramework.UI
{
    public struct TooltipPropertyInfo
    {
        public Func<string> attributeValueGetter;
        public bool isStatic;
        public Sprite icon;
        public string groupName;
    }
}