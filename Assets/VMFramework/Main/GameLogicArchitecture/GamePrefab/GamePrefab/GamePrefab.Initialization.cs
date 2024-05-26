using System;
using UnityEngine;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefab
    {
        #region Initializer

        void IInitializer.OnPreInit(Action onDone)
        {
            PreInit();
            onDone();
        }

        void IInitializer.OnInit(Action onDone)
        {
            Init();
            onDone();
        }

        void IInitializer.OnPostInit(Action onDone)
        {
            PostInit();
            onDone();
        }

        void IInitializer.OnInitComplete(Action onDone)
        {
            InitComplete();
            onDone();
        }

        #endregion

        public virtual bool isPreInitializationRequired => true;

        public virtual bool isInitializationRequired => true;
        
        public virtual bool isPostInitializationRequired => true;

        public virtual bool isInitializationCompleteRequired => true;

        public virtual void CheckSettings()
        {
            if (gameItemType != null)
            {
                if (gameItemType.IsAbstract)
                {
                    Debug.LogError($"{nameof(gameItemType)} is abstract. " +
                                   $"Please override with a concrete type instead of {gameItemType}");
                }
            }
        }

        public void PreInit()
        {
            if (isPreInitializationRequired)
            {
                Debug.Log($"预加载{this}");
                OnPreInit();
            }
        }

        public void Init()
        {
            if (isInitializationRequired)
            {
                Debug.Log($"加载{this}");
                OnInit();
            }
        }

        public void PostInit()
        {
            if (isPostInitializationRequired)
            {
                Debug.Log($"后加载{this}");
                OnPostInit();
            }
        }

        public void InitComplete()
        {
            if (isInitializationCompleteRequired)
            {
                Debug.Log($"初始化{this}完成");
                OnInitComplete();
            }
            
            initDone = true;
        }

        protected virtual void OnPreInit()
        {
            
        }

        protected virtual void OnInit()
        {
            
        }

        protected virtual void OnPostInit()
        {

        }

        protected virtual void OnInitComplete()
        {
            
        }
    }
}