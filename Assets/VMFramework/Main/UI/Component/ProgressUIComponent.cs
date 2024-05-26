using Sirenix.OdinInspector;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using UnityEngine;
using UnityEngine.UI;

namespace VMFramework.UI.Components
{
    public class ProgressUIComponent : UIComponent
    {
        [Required]
        [SerializeField]
        private Image progressImage;

        private float _minValue;
        private float _maxValue;

        private float _currentValue;

        [ShowInInspector]
        public float minValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _minValue;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                _minValue = value;
                UpdateProgress();
            }
        }

        [ShowInInspector]
        public float maxValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _maxValue;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                _maxValue = value;
                UpdateProgress();
            }
        }

        [ShowInInspector]
        public float currentValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _currentValue;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                _currentValue = value;
                UpdateProgress();
            }
        }

        private void UpdateProgress()
        {
            var progress = currentValue.Normalize01(minValue, maxValue);

            progressImage.type = Image.Type.Filled;
            progressImage.fillAmount = progress;
        }
    }
}
