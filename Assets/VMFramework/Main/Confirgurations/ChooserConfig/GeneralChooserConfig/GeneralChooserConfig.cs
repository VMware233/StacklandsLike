using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class GeneralChooserConfig<T> : ChooserConfig<T>
    {
        [HideLabel]
        [SerializeField]
        private SingleValueChooserConfig<T> singleValueChooserConfig = new();

        [HideLabel]
        [SerializeField]
        private WeightedSelectChooserConfig<T> weightedSelectChooserConfig = new();
        
        [HideLabel]
        [SerializeField]
        private CircularSelectChooserConfig<T> circularSelectChooserConfig = new();

        public override IChooser<T> GenerateNewObjectChooser()
        {
            return null;
        }

        public override IEnumerable<T> GetAvailableValues()
        {
            return null;
        }

        public override void SetAvailableValues(Func<T, T> setter)
        {

        }
    }
}
