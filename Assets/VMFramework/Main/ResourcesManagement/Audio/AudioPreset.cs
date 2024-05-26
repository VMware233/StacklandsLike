using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.ResourcesManagement
{
    public class AudioPreset : GameTypedGamePrefab
    {
        protected override string idSuffix => "audio";

        [LabelText("音效片段")]
        [Required]
        public AudioClip audioClip;

        [LabelText("音量")]
        [PropertyRange(0, 1)]
        public float volume = 1;

        [LabelText("自动检查是否停止")]
        public bool autoCheckStop = true;

        [LabelText("从特定时间开始播放")]
        public bool enablePlayFromTime = false;

        [LabelText("特定播放的时间")]
        [ShowIf(nameof(enablePlayFromTime))]
        [PropertyRange(0, nameof(GetMaxTimeToPlay))]
        public float timeToPlay = 0;

        private float GetMaxTimeToPlay()
        {
            if (audioClip != null)
            {
                return audioClip.length;
            }

            return 0;
        }

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            audioClip.AssertIsNotNull(nameof(audioClip));
        }
    }
}