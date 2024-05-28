#if UNITY_EDITOR


namespace StackLandsLike.Cards
{
    public partial class CardGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            initialCards ??= new();
        }
    }
}
#endif