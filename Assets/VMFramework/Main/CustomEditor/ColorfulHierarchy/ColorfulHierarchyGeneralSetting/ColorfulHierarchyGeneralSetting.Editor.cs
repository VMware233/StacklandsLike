#if UNITY_EDITOR
using VMFramework.Core;

namespace VMFramework.Editor
{
    public partial class ColorfulHierarchyGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            colorPresets ??= new();
            
            if (colorPresets.Count == 0)
            {
                colorPresets.Add(new()
                {
                    keyChar = "$",
                    textColor = ColorDefinitions.white,
                    backgroundColor = ColorDefinitions.orange
                });
                
                colorPresets.Add(new()
                {
                    keyChar = "^",
                    textColor = ColorDefinitions.white,
                    backgroundColor = ColorDefinitions.green
                });
                
                colorPresets.Add(new()
                {
                    keyChar = "#",
                    textColor = ColorDefinitions.deepSkyBlue,
                    backgroundColor = ColorDefinitions.yellow
                });
                
                colorPresets.Add(new()
                {
                    keyChar = "@",
                    textColor = ColorDefinitions.hotPink,
                    backgroundColor = ColorDefinitions.aqua
                });
                
                colorPresets.Add(new()
                {
                    keyChar = "/",
                    textColor = ColorDefinitions.white,
                    backgroundColor = ColorDefinitions.magenta
                });
                
                colorPresets.Add(new()
                {
                    keyChar = "%",
                    textColor = ColorDefinitions.white,
                    backgroundColor = ColorDefinitions.purple
                });
                
                colorPresets.Add(new()
                {
                    keyChar = "!",
                    textColor = ColorDefinitions.white,
                    backgroundColor = ColorDefinitions.red
                });
            }
        }
    }
}
#endif