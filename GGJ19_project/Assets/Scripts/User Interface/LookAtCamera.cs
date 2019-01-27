using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(PlayerController.Instance.PlayerCamera.transform.position);
    }
}
