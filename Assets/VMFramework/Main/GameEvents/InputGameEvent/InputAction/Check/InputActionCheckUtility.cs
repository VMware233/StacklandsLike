using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public static class InputActionCheckUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Check(this IReadOnlyCollection<InputActionGroupRuntime> groups)
        {
            if (groups == null || groups.Count == 0)
            {
                return false;
            }

            foreach (var group in groups)
            {
                if (group.Check())
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Check(this InputActionGroupRuntime group)
        {
            if (group.actions == null || group.actions.Count == 0)
            {
                return false;
            }

            foreach (var action in group.actions)
            {
                if (action.Check() == false)
                {
                    return false;
                }
            }

            return true;
        }
        
        public static bool Check(this InputActionRuntime action)
        {
            KeyCode keyCode = action.inputAction.keyCode;
            switch (action.inputAction.type)
            {
                case InputType.KeyBoardOrMouseOrJoyStick:

                    switch (action.inputAction.keyBoardTriggerType)
                    {
                        case KeyBoardTriggerType.IsPressing:

                            if (Input.GetKey(keyCode) == false)
                            {
                                return false;
                            }

                            break;
                        case KeyBoardTriggerType.PressedDown:

                            if (Input.GetKeyDown(keyCode) == false)
                            {
                                return false;
                            }

                            break;
                        case KeyBoardTriggerType.PressedUp:

                            if (Input.GetKeyUp(keyCode) == false)
                            {
                                return false;
                            }

                            break;
                        case KeyBoardTriggerType.IsHolding:
                            if (Input.GetKey(keyCode) == false)
                            {
                                action.heldTime = 0;
                                return false;
                            }

                            action.heldTime += Time.deltaTime;

                            if (action.heldTime < action.inputAction.holdThreshold)
                            {
                                return false;
                            }

                            break;
                        case KeyBoardTriggerType.IsHoldingAfterThreshold:
                            if (Input.GetKey(keyCode) == false)
                            {
                                action.heldTime = 0;
                                action.hasTriggeredHoldDown = false;

                                return false;
                            }

                            action.heldTime += Time.deltaTime;

                            if (action.heldTime < action.inputAction.holdThreshold)
                            {
                                return false;
                            }

                            if (action.hasTriggeredHoldDown)
                            {
                                return false;
                            }

                            action.hasTriggeredHoldDown = true;
                            break;
                        case KeyBoardTriggerType.HoldAndRelease:
                            if (Input.GetKey(keyCode))
                            {
                                action.heldTime += Time.deltaTime;
                            }

                            if (Input.GetKeyUp(keyCode))
                            {
                                if (action.heldTime > action.inputAction.holdThreshold)
                                {
                                    action.heldTime = 0;
                                }
                                else
                                {
                                    action.heldTime = 0;
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }
    }
}