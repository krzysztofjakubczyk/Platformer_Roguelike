using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera CameraDown;
    [SerializeField] CinemachineBrain brain;
    [SerializeField][Range(-10f, -5f)] float ClickedObjectOffset;
    [SerializeField][Range(-3f, -1f)] float DefaultObjectOffsetOnSecondCamera;
    [SerializeField] GetCameraDown flag;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && brain.ActiveVirtualCamera.Name == "MainCamera")
        {
            CameraDown.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);
            CameraDown.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_TrackedObjectOffset.y = ClickedObjectOffset;
        }
        else if (Input.GetKeyDown(KeyCode.F) && brain.ActiveVirtualCamera.Name == "LookDown")
        {
            CameraDown.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_TrackedObjectOffset.y = ClickedObjectOffset;
        }
        if (Input.GetKeyUp(KeyCode.F) && flag.GetIsTriggered() == false)    
        {
            CameraDown.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_TrackedObjectOffset.y = DefaultObjectOffsetOnSecondCamera;
        }
        else if (Input.GetKeyUp(KeyCode.F) && flag.GetIsTriggered() == true)
        {
            CameraDown.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            CameraDown.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_TrackedObjectOffset.y = DefaultObjectOffsetOnSecondCamera;
        }
    }
}
