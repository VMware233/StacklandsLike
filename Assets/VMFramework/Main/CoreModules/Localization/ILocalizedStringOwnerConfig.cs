namespace VMFramework.Localization
{
    public struct LocalizedStringAutoConfigSettings
    {
        public string defaultTableName;
        public bool save;
    }
    
    public interface ILocalizedStringOwnerConfig
    {
        public void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings);
        
        public void SetKeyValueByDefault();
    }
}
