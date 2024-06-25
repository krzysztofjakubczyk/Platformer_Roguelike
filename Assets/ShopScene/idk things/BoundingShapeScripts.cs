using Cinemachine;
using UnityEngine;

public class BoundingShapeScripts : MonoBehaviour
{
    private CinemachineConfiner2D mainCameraConfiner;
    private SceneController sceneController;
    [SerializeField] private CompositeCollider2D colliderForCamera;
    [SerializeField] private CompositeCollider2D transitionCollider;

    
    private void Start()
    {
        sceneController = GetComponent<SceneController>();
        mainCameraConfiner = FindObjectOfType<CinemachineConfiner2D>();
    }
    public void SetBoundingShape(CompositeCollider2D collider)
    {
        mainCameraConfiner.m_BoundingShape2D = collider;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        if (gameObject.name == "CameraLoadTrigger")
        {
            if (CameraController.instiate.GetisTransitionCollider())
            {
                SetBoundingShape(colliderForCamera);
                CameraController.instiate.SetTransitionCollider(false);
            }
            else
            {
                SetBoundingShape(colliderForCamera);
            }
            CameraController.instiate.SetNormalCollider(true);
        }
        else if (gameObject.name == "LoadTrainstionCameraTrigger")
        {
            if (CameraController.instiate.GetisNormalCollider())
            {
                SetBoundingShape(transitionCollider);
                CameraController.instiate.SetTransitionCollider(true);
            }
            else
            {
                SetBoundingShape(transitionCollider);
            }
            CameraController.instiate.SetNormalCollider(false);
        }
    }   
}
