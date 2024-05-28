using UnityEngine;

namespace VMFramework.UI
{
    public interface IUGUIPanelPreset : IUIPanelPreset
    {
        public GameObject prefab { get; }
    }
}