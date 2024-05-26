using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core.Linq;

namespace VMFramework.Configuration.Animation
{
    public sealed partial class GameObjectAnimation : BaseConfig
    {
        [LabelText("片段列表")]
        [SerializeField]
        private List<IGameObjectAnimationClip> clips = new();

        [LabelText("总持续时间")]
        [ShowInInspector]
        public float totalDuration => GetTotalDuration();
        
        private float GetTotalDuration()
        {
            if (clips.IsNullOrEmpty())
            {
                return 0;
            }

            return clips.Max(clip => clip != null ? clip.GetStartTime() + clip.GetDuration() : 0);
        }
        
        public void Run(Transform target, CancellationToken token = default)
        {
            if (IsEmpty())
            {
                Debug.LogWarning("动画片段列表为空，无法播放");
                return;
            }
            
            foreach (var clip in clips)
            {
                if (clip.IsRequirementSatisfied(target) == false)
                {
                    continue;
                }
                
                clip.Start(target);
                
                if (target != null && target.gameObject.activeSelf)
                {
                    clip.Run(target, token);
                }
            }
        }
        
        public async UniTask RunAndAwait(Transform target, CancellationToken token = default)
        {
            if (IsEmpty())
            {
                Debug.LogWarning("动画片段列表为空，无法播放");
                return;
            }
            
            var awaitList = new List<UniTask>();

            foreach (var clip in clips)
            {
                if (clip.IsRequirementSatisfied(target) == false)
                {
                    continue;
                }
                
                clip.Start(target);
                
                if (target != null && target.gameObject.activeSelf)
                {
                    awaitList.Add(clip.Run(target, token));
                }
            }

            if (awaitList.Count == 0)
            {
                Debug.LogWarning("动画片段列表中没有满足条件的片段，无法播放");
                return;
            }
            
            await UniTask.WhenAll(awaitList);
        }

        public void Kill(Transform target)
        {
            if (target == null)
            {
                return;
            }

            foreach (var clip in clips)
            {
                clip.Kill(target);
            }
        }
        
        public bool IsEmpty()
        {
            return clips == null || clips.Count == 0;
        }

        #region Init & Check

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            clips.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            clips.Init();
        }

        #endregion
    }
}