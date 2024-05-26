using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.OdinExtensions;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public class PopupManager : ManagerBehaviour<PopupManager>
    {
        #region Popup Text

        [Button]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PopupText(
            [UIPresetID(typeof(IPopupTextPreset))]
            string damagePopupID, Transform target, string content, Color? textColor = null)
        {
            var popup = ITracingUIPanel.OpenOnTargetTransform<IPopupTextController>(damagePopupID, target);

            popup.text = content;

            if (textColor.HasValue)
            {
                popup.textColor = textColor.Value;
            }
        }

        [Button]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PopupText(
            [UIPresetID(typeof(IPopupTextPreset))]
            string damagePopupID, Transform target, float value, int decimalPlaces = 0, Color? textColor = null)
        {
            var popup = ITracingUIPanel.OpenOnTargetTransform<IPopupTextController>(damagePopupID, target);

            popup.text = value.ToString(decimalPlaces);
            
            if (textColor.HasValue)
            {
                popup.textColor = textColor.Value;
            }
        }

        [Button]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PopupText(
            [UIPresetID(typeof(IPopupTextPreset))]
            string damagePopupID, Transform target, int value, Color? textColor = null)
        {
            var popup = ITracingUIPanel.OpenOnTargetTransform<IPopupTextController>(damagePopupID, target);

            popup.text = value.ToString();
            
            if (textColor.HasValue)
            {
                popup.textColor = textColor.Value;
            }
        }
        
        [Button]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PopupText(
            [UIPresetID(typeof(IPopupTextPreset))]
            string damagePopupID, Vector3 position, string content, Color? textColor = null)
        {
            var popup = ITracingUIPanel.OpenOnTargetPosition<UGUIPopupTextController>(damagePopupID, position);

            popup.text = content;
            
            if (textColor.HasValue)
            {
                popup.textColor = textColor.Value;
            }
        }

        [Button]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PopupText(
            [UIPresetID(typeof(IPopupTextPreset))]
            string damagePopupID, Vector3 position, float value, int decimalPlaces = 0, Color? textColor = null)
        {
            var popup = ITracingUIPanel.OpenOnTargetPosition<UGUIPopupTextController>(damagePopupID, position);

            popup.text = value.ToString(decimalPlaces);
            
            if (textColor.HasValue)
            {
                popup.textColor = textColor.Value;
            }
        }

        [Button]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PopupText(
            [UIPresetID(typeof(IPopupTextPreset))]
            string damagePopupID, Vector3 position, int value, Color? textColor = null)
        {
            var popup = ITracingUIPanel.OpenOnTargetPosition<UGUIPopupTextController>(damagePopupID, position);

            popup.text = value.ToString();
            
            if (textColor.HasValue)
            {
                popup.textColor = textColor.Value;
            }
        }

        #endregion
    }
}
