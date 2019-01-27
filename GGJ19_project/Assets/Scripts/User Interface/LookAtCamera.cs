using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(PlayerController.Instance.CameraTransform.position);
    }
}
