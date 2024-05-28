using System;
using System.Collections;
using VMFramework.Core;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VMFramework.UI
{
    public class UGUIPanelPreset : UIPanelPreset, IUGUIPanelPreset
    {
        protected const string UGUI_PANEL_CATEGORY = "UGUI";

        public override Type controllerType => typeof(UGUIPanelController);

        [TabGroup(TAB_GROUP_NAME, UGUI_PANEL_CATEGORY)]
        [AssetsOnly]
        [Required]
        public GameObject prefab;

        GameObject IUGUIPanelPreset.prefab => prefab;

        public override void CheckSettings()
        {
            base.CheckSettings();

            prefab.AssertIsNotNull(nameof(prefab));
        }
    }
}
