using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace StackLandsLike.UI
{
    [RequireComponent(typeof(Camera))]
    [DisallowMultipleComponent]
    public class CameraScaleController : UniqueMonoBehaviour<CameraScaleController>
    {
        [MinValue(0)]
        [SerializeField]
        private float speed = 10f;
        
        [MaxValue(0)]
        [SerializeField]
        private float minScroll = -10f;
        
        [MinValue(0)]
        [SerializeField]
        private float maxScroll = 10f;
        
        [ShowInInspector]
        private float currentScroll = 0;
        
        private Camera _camera;
        
        private static int disableCount = 0;

        private void Start()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (disableCount > 0)
            {
                return;
            }
            
            if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width ||
                Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
            {
                return;
            }
            
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                scroll *= speed;
                float newScroll = currentScroll + scroll;
                newScroll = newScroll.Clamp(minScroll, maxScroll);
                float actualScroll = newScroll - currentScroll;
                currentScroll = newScroll;
                
                _camera.transform.Translate(0, 0, actualScroll);
            }
        }

        public static void Disable()
        {
            disableCount++;
        }
        
        public static void Enable()
        {
            disableCount--;
        }
    }
}