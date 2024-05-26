namespace VMFramework.UI
{
    public partial class UIPanelPreset
    {
        public bool ShouldSerializecloseInputMappingID()
        {
            return enableUICloseGameEvent;
        }

        public bool ShouldSerializetoggleInputMappingID()
        {
            return enableUIGameEvent;
        }
    }
}