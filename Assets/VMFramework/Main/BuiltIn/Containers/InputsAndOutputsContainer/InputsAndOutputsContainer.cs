using System;
using VMFramework.Core;

namespace VMFramework.Containers
{
    public class InputsAndOutputsContainer : GridContainer, IInputsContainer, IOutputsContainer
    {
        protected InputsAndOutputsContainerPreset inputsAndOutputsContainerPreset =>
            (InputsAndOutputsContainerPreset)gamePrefab;

        public RangeInteger inputsRange { get; private set; }

        public RangeInteger outputsRange { get; private set; }

        IKCube<int> IInputsContainer.inputsRange => inputsRange;

        IKCube<int> IOutputsContainer.outputsRange => outputsRange;

        protected override void OnCreate()
        {
            base.OnCreate();

            inputsRange = new(inputsAndOutputsContainerPreset.inputsRange);
            outputsRange = new(inputsAndOutputsContainerPreset.outputsRange);
        }

        #region Add

        public override bool TryAddItem(IContainerItem item)
        {
            return TryAddItem(item, inputsRange.min, inputsRange.max);
        }

        #endregion

        #region Sort & Stack

        public override void StackItems()
        {
            this.StackItems(inputsRange.min, inputsRange.max);
            this.StackItems(outputsRange.min, outputsRange.max);
        }

        public override void Sort(Comparison<IContainerItem> comparison)
        {
            Sort(inputsRange.min, inputsRange.max, comparison);
            Sort(outputsRange.min, outputsRange.max, comparison);
        }

        #endregion
    }
}
