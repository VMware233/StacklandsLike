#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

namespace VMFramework.OdinExtensions
{
    public abstract class SimpleOdinEditorWindow<TContainer> : OdinEditorWindow
        where TContainer : SimpleOdinEditorWindowContainer
    {
        private TContainer container;

        protected static TWindow GetSimpleWindow<TWindow>(string editorName)
            where TWindow : SimpleOdinEditorWindow<TContainer>
        {
            bool hasOpenedWindow = HasOpenInstances<TWindow>();
            var window = GetWindow<TWindow>(editorName);

            if (hasOpenedWindow == false)
            {
                window.position = GUIHelper.GetEditorWindowRect().AlignCenter(900, 600);
            }

            return window;
        }

        protected override void Initialize()
        {
            base.Initialize();

            if (container == null)
            {
                container = CreateInstance<TContainer>();
            }

            container.Init();
        }

        protected override object GetTarget()
        {
            return container;
        }
    }
}
#endif