using Cinemachine;
using UnityEngine;

public class GetCameraDown : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera CameraDown;
    bool isTriggered = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Player") && gameObject.CompareTag("StartCameraMoveDown"))
        {

            changeCamera();
            isTriggered = false;
        }
        else if (collision.CompareTag("Player") && gameObject.CompareTag("EndCameraMoveDown"))
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
