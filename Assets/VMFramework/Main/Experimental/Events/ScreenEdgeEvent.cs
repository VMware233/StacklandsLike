using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("EventsCustomed/ScreenEdgeEvent", 0)]
public class ScreenEdgeEvent : MonoBehaviour
{
    [Header("ScreenEdgePadding")]
    public float leftPadding = 10;
    public float rightPadding = 10;
    public float topPadding = 10;
    public float bottomPadding = 10;

    public float overflowPadding = 40;

    [Header("鼠标在屏幕左边")]
    public UnityEvent mouseOnLeft;
    
    [Header("鼠标在屏幕右边")]
    public UnityEvent mouseOnRight;

    [Header("鼠标在屏幕上面")]
    public UnityEvent mouseOnUpSide;

    [Header("鼠标在屏幕下面")]
    public UnityEvent mouseOnBottom;

    void Update()
    {
        if (Input.mousePosition.x < leftPadding && Input.mousePosition.x > -overflowPadding) {
            mouseOnLeft.Invoke();
        }
        if (Input.mousePosition.x > Screen.width - rightPadding && Input.mousePosition.x < Screen.width + overflowPadding) {
            mouseOnRight.Invoke();
        }
        if (Input.mousePosition.y > Screen.height - topPadding && Input.mousePosition.y < Screen.height + overflowPadding) {
            mouseOnUpSide.Invoke();
        }
        if (Input.mousePosition.y < bottomPadding && Input.mousePosition.y > -overflowPadding) {
            mouseOnBottom.Invoke();
        }
    }
}
