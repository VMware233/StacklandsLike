using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public new Camera camera => GetComponent<Camera>();

    [SerializeField]
    private float distance = 13f;

    [SerializeField]
    [OnValueChanged(nameof(OnCameraEulerAnglesGUI))]
    private Vector3 _cameraEulerAngles = Vector3.zero;

    public Vector3 cameraEulerAngles => _cameraEulerAngles;

    [SerializeField]
    [OnValueChanged(nameof(OnCameraEulerAnglesGUI))]
    private bool _isCameraRotationLocal;

    public bool isCameraRotationLocal => _isCameraRotationLocal;

    private Quaternion cameraRotation;

    [ShowInInspector]
    private float targetFOV = 60f;

    [ShowInInspector]
    private Transform targetTransform;

    [SerializeField]
    private Transform fixedTargetTransform;

    #region GUI

    private void OnCameraEulerAnglesGUI()
    {
        cameraRotation = Quaternion.Euler(_cameraEulerAngles);
    }

    #endregion

    private void Start()
    {
        targetFOV = camera.fieldOfView;
        cameraRotation = Quaternion.Euler(_cameraEulerAngles);
        _isCameraRotationLocal = false;

        if (fixedTargetTransform != null)
        {
            SetFollowTarget(fixedTargetTransform);
        }
    }

    private void FixedUpdate()
    {
        var currentFOV = camera.fieldOfView;

        if (currentFOV.Distance(targetFOV) > 0.1f)
        {
            camera.fieldOfView = currentFOV.Lerp(targetFOV,
                GameCoreSetting.cameraGeneralSetting.fovLerpSpeed *
                Time.deltaTime);
        }

        if (targetTransform == null)
        {
            return;
        }

        var targetPosition = targetTransform.position + GetRelativeDisplacement();

        transform.position = transform.position.Lerp(targetPosition,
            GameCoreSetting.cameraGeneralSetting.positionLerpSpeed *
            Time.deltaTime);

        if (_isCameraRotationLocal)
        {
            transform.localRotation = transform.localRotation.Lerp(cameraRotation,
                GameCoreSetting.cameraGeneralSetting.angleLerpSpeed *
                Time.deltaTime);
        }

        transform.rotation = transform.rotation.Lerp(cameraRotation,
            GameCoreSetting.cameraGeneralSetting.angleLerpSpeed *
            Time.deltaTime);
    }

    private Vector3 GetRelativeDisplacement()
    {
        Vector3 displacement = transform.rotation * Vector3.forward * distance;

        return -displacement;
    }

    public void SetFollowTarget(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }

    public void SetFOV(float fov)
    {
        targetFOV = fov;
    }

    public void SetCameraRotation(Vector3 eulerAngles, bool isLocal)
    {
        _cameraEulerAngles = eulerAngles;
        cameraRotation = Quaternion.Euler(_cameraEulerAngles);
        _isCameraRotationLocal = isLocal;
    }

    [Button]
    public void FollowTargetImmediately()
    {
        Transform target = null;

        if (targetTransform != null)
        {
            target = targetTransform;
        }

        if (fixedTargetTransform != null)
        {
            target = fixedTargetTransform;
        }

        if (target == null)
        {
            return;
        }

        if (_isCameraRotationLocal)
        {
            transform.eulerAngles = _cameraEulerAngles;
        }
        else
        {
            transform.localEulerAngles = _cameraEulerAngles;
        }

        var targetPosition = target.position + GetRelativeDisplacement();

        transform.position = targetPosition;
    }
}
