using Cinemachine;
using UnityEngine;

public class GetCameraDown : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera CameraDown;
    bool isTriggered = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StartCameraMoveDown") && gameObject.CompareTag("Player"))
        {   
            changeCamera();
            isTriggered = false;
        }
        else if (collision.CompareTag("EndCameraMoveDown") && gameObject.CompareTag("Player"))
        {
            backCamera();
            isTriggered = true;
        }
    }
    void changeCamera()
    {
        mainCamera.gameObject.SetActive(false);
        CameraDown.gameObject.SetActive(true);
        CameraDown.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_TrackedObjectOffset.y = -2f;
    }
    void backCamera()
    {
        mainCamera.gameObject.SetActive(true);
        CameraDown.gameObject.SetActive(false);
    }
    public bool GetIsTriggered()
    {
        return isTriggered;
    }
}
