using UnityEngine;

namespace VMFramework.UI
{
    public interface IPopupTextController : IPopupController
    {
        public string text { get; set; }
        
        public Color textColor { get; set; }
    }
}