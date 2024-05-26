using Sirenix.OdinInspector;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField]
    [Required]
    private Camera cameraToFace;

    private void Start()
    {
        if (cameraToFace == null)
        {
            cameraToFace = Camera.main;
        }

        if (cameraToFace == null)
        {
            cameraToFace = CameraManager.mainCamera;
        }
    }

    void Update()
    {
        transform.rotation =
            Quaternion.LookRotation(transform.position -
                                    cameraToFace.transform.position);
    }
}
