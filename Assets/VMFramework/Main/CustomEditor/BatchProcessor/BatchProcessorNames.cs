#if UNITY_EDITOR
using VMFramework.Localization;

namespace VMFramework.Editor
{
    public static class BatchProcessorNames
    {
        #region Batch Processor Name

        public const string BATCH_PROCESSOR_DEFAULT_NAME = "Batch Processor";
        public const string BATCH_PROCESSOR_NAME_KEY = "BatchProcessorName";

        public static string batchProcessorName => LocalizationEditorManager.GetStringOfEditorTable(BATCH_PROCESSOR_NAME_KEY,
            BATCH_PROCESSOR_DEFAULT_NAME);

        #endregion
    }
}
#endif