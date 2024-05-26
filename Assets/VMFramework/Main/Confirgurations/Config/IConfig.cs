namespace VMFramework.Configuration
{
    public partial interface IConfig
    {
        public bool initDone { get; }

        public void Init();

        public void CheckSettings();
    }
}