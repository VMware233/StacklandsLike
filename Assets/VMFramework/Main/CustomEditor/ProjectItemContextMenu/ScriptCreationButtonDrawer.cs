#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace VMFramework.Editor.ProjectItemContextMenu
{
    internal sealed class ScriptCreationButtonDrawer : GeneralButtonDrawer
    {
        protected override Icon icon => new Icon(SdfIconType.Code);

        protected override string tooltip => "Create Scripts";

        public override bool CanDrawButton(ProjectItem item)
        {
            return true;
        }
    }
}
#endif