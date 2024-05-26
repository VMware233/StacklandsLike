using System;
using System.Collections;
using System.Collections.Generic;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface IChooserConfig : IConfig, IChooser
    {
        public void RegenerateObjectChooser();
        
        public IChooser GetObjectChooser();
        
        public IChooser GenerateNewObjectChooser();

        public IEnumerable GetAvailableValues();
    }
    
    public interface IChooserConfig<T> : IChooserConfig, IChooser<T>
    {
        public new IChooser<T> GetObjectChooser();

        IChooser IChooserConfig.GetObjectChooser()
        {
            return GetObjectChooser();
        }

        public new IChooser<T> GenerateNewObjectChooser();

        IChooser IChooserConfig.GenerateNewObjectChooser()
        {
            return GenerateNewObjectChooser();
        }

        public new IEnumerable<T> GetAvailableValues();

        IEnumerable IChooserConfig.GetAvailableValues()
        {
            return GetAvailableValues();
        }

        public void SetAvailableValues(Func<T, T> setter);
    }
}