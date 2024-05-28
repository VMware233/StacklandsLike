using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StackLandsLike.UI
{
    [RequireComponent(typeof(Camera))]
    public sealed class CameraMoveController : MonoBehaviour
    {
        [MinValue(0)]
        [SerializeField]
        private float speed = 0.1f;
        
        private Camera _camera;

        private void Start()
        {
            _camera = GetComponent<Camera>();
        }
        
        private void FixedUpdate()
        {
            if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                var delta = Input.mousePositionDelta;
                
                delta *= speed;
                
                _camera.transform.position -= new Vector3(delta.x, delta.y, 0);
            }
        }
    }
}