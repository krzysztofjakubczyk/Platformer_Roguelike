using Cinemachine;
using UnityEngine;

public class BoundingShapeScripts : MonoBehaviour
{
    private CinemachineConfiner2D mainCameraConfiner;
    [SerializeField] private CompositeCollider2D colliderForCamera;
    [SerializeField] private CompositeCollider2D transitionCollider;

    private void Start()
    {
        mainCameraConfiner = FindObjectOfType<CinemachineConfiner2D>();
    }
    public void SetBoundingShape(CompositeCollider2D collider)
    {
        mainCameraConfiner.m_BoundingShape2D = collider;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            return;
        }
        if (gameObject.name == "CameraLoadTrigger")
        {
            Debug.Log(collision.name);
            SetBoundingShape(colliderForCamera);
        }
        else if (gameObject.name == "LoadTrainstionCameraTrigger")
        {
            Debug.Log(collision.name);
            SetBoundingShape(transitionCollider);
        }
    }
}
