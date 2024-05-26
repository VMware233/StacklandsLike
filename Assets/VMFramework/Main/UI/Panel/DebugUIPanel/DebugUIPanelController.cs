using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class DebugUIPanelController : UIToolkitPanelController
    {
        public struct DebugEntryInfo
        {
            public IconLabelVisualElement iconLabel;

            [ShowInInspector]
            private bool display => iconLabel.style.display.value == DisplayStyle.Flex;
        }

        private DebugUIPanelPreset debugUIPanelPreset { get; set; }

        [ShowInInspector]
        private VisualElement leftContainer;

        [ShowInInspector]
        private VisualElement rightContainer;

        [ShowInInspector]
        private VisualTreeAsset debugEntryPrototype;

        [ShowInInspector]
        private List<(IDebugEntry debugEntry, DebugEntryInfo info)> debugEntryInfos = new();

        private float updateInterval;

        private float currentTime;

        private void Update()
        {
            currentTime += Time.deltaTime;

            if (currentTime > updateInterval)
            {
                currentTime = 0;

                foreach (var (debugEntry, info) in debugEntryInfos)
                {
                    if (debugEntry.ShouldDisplay())
                    {
                        info.iconLabel.style.display = DisplayStyle.Flex;
                        info.iconLabel.label.text = debugEntry.GetText();
                    }
                    else
                    {
                        info.iconLabel.style.display = DisplayStyle.None;
                    }
                }
            }
        }

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            debugUIPanelPreset = preset as DebugUIPanelPreset;

            debugUIPanelPreset.AssertIsNotNull(nameof(debugUIPanelPreset));

            updateInterval = GameCoreSetting.debugUIPanelGeneralSetting.updateInterval;
        }

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            leftContainer = rootVisualElement.Q(debugUIPanelPreset.leftContainerVisualElementName);
            rightContainer = rootVisualElement.Q(debugUIPanelPreset.rightContainerVisualElementName);

            leftContainer.AssertIsNotNull(nameof(leftContainer));

            rightContainer.AssertIsNotNull(nameof(rightContainer));

            debugEntryInfos.Clear();

            foreach (var debugEntry in GamePrefabManager.GetAllActiveGamePrefabs<IDebugEntry>())
            {
                AddEntry(debugEntry);
            }
        }

        public void AddEntry(IDebugEntry debugEntry)
        {
            var container = debugEntry.position switch
            {
                LeftRightDirection.Left => leftContainer,
                LeftRightDirection.Right => rightContainer,
                _ => throw new ArgumentOutOfRangeException()
            };

            var debugEntryVisualElement = new IconLabelVisualElement
            {
                label =
                {
                    text = ""
                }
            };

            AddVisualElement(container, debugEntryVisualElement);

            debugEntryInfos.Add((debugEntry, new DebugEntryInfo
            {
                iconLabel = debugEntryVisualElement
            }));
        }

        protected override void OnCurrentLanguageChanged(Locale currentLocale)
        {
            base.OnCurrentLanguageChanged(currentLocale);
            
            
        }

        #region Test

        [Button(nameof(AddEntry))]
        private void AddEntry([GamePrefabID(typeof(IDebugEntry))] string debugEntryID)
        {
            AddEntry(GamePrefabManager.GetGamePrefab<IDebugEntry>(debugEntryID));
        }

        #endregion
    }
}