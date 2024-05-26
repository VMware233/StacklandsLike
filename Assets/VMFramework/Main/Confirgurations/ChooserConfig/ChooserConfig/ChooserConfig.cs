using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [PreviewComposite]
    public abstract class ChooserConfig<T> : BaseConfig, IChooserConfig<T>
    {
        [ShowInInspector, HideInEditorMode]
        private IChooser<T> objectChooser;

        protected override void OnInit()
        {
            base.OnInit();
            
            objectChooser = GenerateNewObjectChooser();
        }

        public T GetValue()
        {
            return objectChooser.GetValue();
        }

        public IChooser<T> GetObjectChooser()
        {
            return objectChooser;
        }

        public void RegenerateObjectChooser()
        {
            objectChooser = GenerateNewObjectChooser();
        }

        public abstract IEnumerable<T> GetAvailableValues();
        
        public abstract void SetAvailableValues(Func<T, T> setter);

        public abstract IChooser<T> GenerateNewObjectChooser();

        protected virtual string ValueToString(T value)
        {
            return value?.ToString();
        }
    }
}