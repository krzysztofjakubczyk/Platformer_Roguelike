using Cinemachine;
using UnityEngine;

public class BoundingShapeScripts : MonoBehaviour
{
    private CinemachineConfiner2D mainCameraConfiner;
    private CompositeCollider2D colliderForCamera;

    private void Start()
    {
        mainCameraConfiner = FindObjectOfType<CinemachineConfiner2D>();
        colliderForCamera = GameObject.Find("ColliderForCamera").GetComponent<CompositeCollider2D>();
        mainCameraConfiner.m_BoundingShape2D = colliderForCamera;
    }

    public void SetBoundingShape(CompositeCollider2D collider)
    {
        mainCameraConfiner.m_BoundingShape2D = collider;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.name == "CameraLoadTrigger")
        {
            var newCollider = GameObject.Find("ColliderForNewCamera").GetComponent<CompositeCollider2D>();
            SetBoundingShape(newCollider);
            newCollider.name = "ColliderForCamera";
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player") && gameObject.name == "LoadTrainstionCameraTrigger")
        {
            var transitionCollider = GameObject.Find("TransitionCameraCollider").GetComponent<CompositeCollider2D>();
            SetBoundingShape(transitionCollider);
            Destroy(gameObject);
        }
    }
}
