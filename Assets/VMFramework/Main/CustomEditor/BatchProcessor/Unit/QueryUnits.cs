// using System.Collections.Generic;
// using System.Linq;
// using VMFramework.UI;
//
// namespace VMFramework.Editor
// {
//     #region UI Toolkit Panel Preset
//
//     public class QueryTracingUIPanelPresetsUnit : SingleButtonBatchProcessorUnit
//     {
//         protected override string processButtonName => "查询追踪UI面板预设";
//
//         public override bool IsValid(IList<object> selectedObjects)
//         {
//             return selectedObjects.Any(obj =>
//                 obj is UIToolkitTracingUIPanelPreset or UGUITracingUIPanelPreset);
//         }
//
//         protected override IEnumerable<object> OnProcess(
//             IEnumerable<object> selectedObjects)
//         {
//             return selectedObjects.Where(obj => obj is UIToolkitTracingUIPanelPreset
//                 or UGUITracingUIPanelPreset);
//         }
//     }
//
//     public class QueryContainerUIBasePresetsUnit : SingleButtonBatchProcessorUnit
//     {
//         protected override string processButtonName => "查询容器UI面板预设";
//
//         public override bool IsValid(IList<object> selectedObjects)
//         {
//             return selectedObjects.Any(obj =>
//                 obj is ContainerUIBasePreset);
//         }
//
//         protected override IEnumerable<object> OnProcess(
//             IEnumerable<object> selectedObjects)
//         {
//             return selectedObjects.Where(obj => obj is ContainerUIBasePreset);
//         }
//     }
//
//     #endregion
// }