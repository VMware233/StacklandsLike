using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using EnumsNET;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.GameEvents
{
    [ManagerCreationProvider(ManagerType.EventCore)]
    public sealed partial class ColliderMouseEventManager : ManagerBehaviour<ColliderMouseEventManager>
    {
        private const string DEBUGGING_CATEGORY = "Only For Debugging";
        
        [SerializeField]
        private Camera fixedBindCamera;

        [ShowInInspector]
        [HideInEditorMode]
        public static Camera bindCamera;

        public static float detectDistance2D => GameCoreSetting.colliderMouseEventGeneralSetting.detectDistance2D;

        public static ObjectDimensions dimensionsDetectPriority =>
            GameCoreSetting.colliderMouseEventGeneralSetting.dimensionsDetectPriority;

        [BoxGroup(DEBUGGING_CATEGORY), ReadOnly, ShowInInspector]
        private static ColliderMouseEventTrigger currentHoverTrigger;

        [BoxGroup(DEBUGGING_CATEGORY), ReadOnly, ShowInInspector]
        private static ColliderMouseEventTrigger lastHoverTrigger;

        [BoxGroup(DEBUGGING_CATEGORY), ReadOnly, ShowInInspector]
        private static ColliderMouseEventTrigger leftMouseUpDownTrigger;

        [BoxGroup(DEBUGGING_CATEGORY), ReadOnly, ShowInInspector]
        private static ColliderMouseEventTrigger rightMouseUpDownTrigger;

        [BoxGroup(DEBUGGING_CATEGORY), ReadOnly, ShowInInspector]
        private static ColliderMouseEventTrigger middleMouseUpDownTrigger;

        [BoxGroup(DEBUGGING_CATEGORY), ReadOnly, ShowInInspector]
        private static ColliderMouseEventTrigger dragTrigger;

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();
            
            if (fixedBindCamera != null)
            {
                bindCamera = fixedBindCamera;
            }
            else
            {
                bindCamera = Camera.main;
            }
        }

        private void Update()
        {
            currentHoverTrigger = DetectTrigger();

            var currentHoverTriggerIsNull = currentHoverTrigger == null;
            var lastHoverTriggerIsNull = lastHoverTrigger == null;

            #region Pointer Enter & Leave & Hover

            if (currentHoverTriggerIsNull)
            {
                // Pointer Leave
                if (lastHoverTriggerIsNull == false)
                {
                    Invoke(MouseEventType.PointerLeave, lastHoverTrigger);
                }
            }
            else
            {
                // Pointer Leave & Enter
                if (currentHoverTrigger != lastHoverTrigger)
                {
                    if (lastHoverTriggerIsNull == false)
                    {
                        Invoke(MouseEventType.PointerLeave, lastHoverTrigger);
                    }

                    Invoke(MouseEventType.PointerEnter, currentHoverTrigger);
                }

                // Pointer Hover
                Invoke(MouseEventType.PointerHover, currentHoverTrigger);
            }

            #endregion

            #region Left Mouse Button Up & Down

            if (leftMouseUpDownTrigger == null)
            {
                if (currentHoverTriggerIsNull == false)
                {
                    //Down
                    if (Input.GetMouseButtonDown(0))
                    {
                        leftMouseUpDownTrigger = currentHoverTrigger;

                        Invoke(MouseEventType.LeftMouseButtonDown, leftMouseUpDownTrigger);
                        Invoke(MouseEventType.LeftMouseButtonStay, leftMouseUpDownTrigger);
                    }
                }
            }
            else
            {
                if (currentHoverTrigger == leftMouseUpDownTrigger)
                {
                    //Up & Click
                    if (Input.GetMouseButtonUp(0))
                    {
                        Invoke(MouseEventType.LeftMouseButtonUp, leftMouseUpDownTrigger);
                        Invoke(MouseEventType.LeftMouseButtonClick, leftMouseUpDownTrigger);

                        leftMouseUpDownTrigger = null;
                    }
                    //Stay
                    else if (Input.GetMouseButton(0))
                    {
                        Invoke(MouseEventType.LeftMouseButtonStay, leftMouseUpDownTrigger);
                    }
                }
                else
                {
                    //Up
                    if (Input.GetMouseButtonUp(0))
                    {
                        Invoke(MouseEventType.LeftMouseButtonUp, leftMouseUpDownTrigger);

                        leftMouseUpDownTrigger = null;
                    }
                }
            }

            #endregion

            #region Right Mouse Button Up & Down

            if (rightMouseUpDownTrigger == null)
            {
                if (currentHoverTriggerIsNull == false)
                {
                    //Down
                    if (Input.GetMouseButtonDown(1))
                    {
                        rightMouseUpDownTrigger = currentHoverTrigger;

                        Invoke(MouseEventType.RightMouseButtonDown, rightMouseUpDownTrigger);
                        Invoke(MouseEventType.RightMouseButtonStay, rightMouseUpDownTrigger);
                    }
                }
            }
            else
            {
                if (currentHoverTrigger == rightMouseUpDownTrigger)
                {
                    //Up & Click
                    if (Input.GetMouseButtonUp(1))
                    {
                        Invoke(MouseEventType.RightMouseButtonUp, rightMouseUpDownTrigger);
                        Invoke(MouseEventType.RightMouseButtonClick, rightMouseUpDownTrigger);

                        rightMouseUpDownTrigger = null;
                    }
                    //Stay
                    else if (Input.GetMouseButton(1))
                    {
                        Invoke(MouseEventType.RightMouseButtonStay, rightMouseUpDownTrigger);
                    }
                }
                else
                {
                    //Up
                    if (Input.GetMouseButtonUp(1))
                    {
                        Invoke(MouseEventType.RightMouseButtonUp, rightMouseUpDownTrigger);

                        rightMouseUpDownTrigger = null;
                    }
                }
            }

            #endregion

            #region Middle Mouse Button Up & Down

            if (middleMouseUpDownTrigger == null)
            {
                if (currentHoverTriggerIsNull == false)
                {
                    //Down
                    if (Input.GetMouseButtonDown(2))
                    {
                        middleMouseUpDownTrigger = currentHoverTrigger;

                        Invoke(MouseEventType.MiddleMouseButtonDown, middleMouseUpDownTrigger);
                        Invoke(MouseEventType.MiddleMouseButtonStay, middleMouseUpDownTrigger);
                    }
                }
            }
            else
            {
                if (currentHoverTrigger == middleMouseUpDownTrigger)
                {
                    //Up & Click
                    if (Input.GetMouseButtonUp(2))
                    {
                        Invoke(MouseEventType.MiddleMouseButtonUp, middleMouseUpDownTrigger);
                        Invoke(MouseEventType.MiddleMouseButtonClick, middleMouseUpDownTrigger);

                        middleMouseUpDownTrigger = null;
                    }
                    //Stay
                    else if (Input.GetMouseButton(2))
                    {
                        Invoke(MouseEventType.MiddleMouseButtonStay, middleMouseUpDownTrigger);
                    }
                }
                else
                {
                    //Up
                    if (Input.GetMouseButtonUp(2))
                    {
                        Invoke(MouseEventType.MiddleMouseButtonUp, middleMouseUpDownTrigger);

                        middleMouseUpDownTrigger = null;
                    }
                }
            }

            #endregion

            #region Any Mouse Button Up & Down

            if (currentHoverTriggerIsNull == false)
            {
                //Down
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
                {
                    Invoke(MouseEventType.AnyMouseButtonDown, currentHoverTrigger);
                }

                //Up
                if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
                {
                    Invoke(MouseEventType.AnyMouseButtonUp, currentHoverTrigger);
                }

                //Stay
                if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButtonUp(2))
                {
                    Invoke(MouseEventType.AnyMouseButtonStay, currentHoverTrigger);
                }
            }

            #endregion

            #region Drag Begin & Stay & End

            if (dragTrigger == null)
            {
                // Drag Begin
                if (currentHoverTriggerIsNull == false && currentHoverTrigger.draggable)
                {
                    var triggerDrag = false;

                    foreach (var mouseType in currentHoverTrigger.dragButton.GetFlags())
                    {
                        if (mouseType == MouseButtonType.LeftButton && Input.GetMouseButton(0))
                        {
                            triggerDrag = true;
                            break;
                        }

                        if (mouseType == MouseButtonType.RightButton && Input.GetMouseButton(1))
                        {
                            triggerDrag = true;
                            break;
                        }

                        if (mouseType == MouseButtonType.MiddleButton && Input.GetMouseButton(2))
                        {
                            triggerDrag = true;
                            break;
                        }
                    }

                    if (triggerDrag)
                    {
                        dragTrigger = currentHoverTrigger;

                        Invoke(MouseEventType.DragBegin, dragTrigger);
                    }
                }
            }
            else
            {
                var keepDragging = false;

                foreach (var mouseType in dragTrigger.dragButton.GetFlags())
                {
                    if (mouseType == MouseButtonType.LeftButton && Input.GetMouseButton(0))
                    {
                        keepDragging = true;
                        break;
                    }

                    if (mouseType == MouseButtonType.RightButton && Input.GetMouseButton(1))
                    {
                        keepDragging = true;
                        break;
                    }

                    if (mouseType == MouseButtonType.MiddleButton && Input.GetMouseButton(2))
                    {
                        keepDragging = true;
                        break;
                    }
                }

                if (keepDragging)
                {
                    Invoke(MouseEventType.DragStay, dragTrigger);
                }
                else
                {
                    Invoke(MouseEventType.DragEnd, dragTrigger);

                    dragTrigger = null;
                }
            }

            #endregion

            lastHoverTrigger = currentHoverTrigger;
        }

        private ColliderMouseEventTrigger DetectTrigger()
        {
            if (dimensionsDetectPriority == ObjectDimensions.TWO_D)
            {

                ColliderMouseEventTrigger detected2D = Detect2DTrigger();
                if (detected2D != null)
                {
                    return detected2D;
                }

                return Detect3DTrigger();
            }

            ColliderMouseEventTrigger detected3D = Detect3DTrigger();
            if (detected3D != null)
            {
                return detected3D;
            }

            return Detect2DTrigger();
        }

        private static ColliderMouseEventTrigger Detect3DTrigger()
        {
            var ray = bindCamera.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction, Color.green);

            if (Physics.Raycast(ray, out var hit3D))
            {
                ColliderMouseEventTrigger detectResult = hit3D.collider.gameObject.GetComponent<ColliderMouseEventTrigger>();

                return detectResult;
            }

            return default;
        }

        private static ColliderMouseEventTrigger Detect2DTrigger()
        {
            RaycastHit2D hit2D = default;

            Ray ray = bindCamera.ScreenPointToRay(Input.mousePosition);

            float distance = -1;

            //2D射线检测
            var hitDirections = new List<Vector2>
            {
                Vector2.left,
                Vector2.right,
                Vector2.down,
                Vector2.up
            };

            foreach (Vector2 direction in hitDirections)
            {

                RaycastHit2D newHit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), direction,
                    detectDistance2D);
                if (newHit.collider)
                {
                    Vector3 colliderPos =
                        bindCamera.WorldToScreenPoint(newHit.collider.gameObject.transform.position);
                    colliderPos.z = 0;

                    float newDistance = Vector3.Distance(colliderPos, Input.mousePosition);

                    if (newDistance < distance || distance < 0)
                    {
                        distance = newDistance;
                        hit2D = newHit;
                    }
                }

            }

            if (distance >= 0)
            {
                ColliderMouseEventTrigger detectResult = hit2D.collider.gameObject.GetComponent<ColliderMouseEventTrigger>();

                return detectResult;
            }

            return default;
        }
    }
}
