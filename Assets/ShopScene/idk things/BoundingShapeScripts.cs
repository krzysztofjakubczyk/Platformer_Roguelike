using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
            gameObject.SetActive(false);
            print("Prze³¹czono na kamerê: " + newCollider.name);
        }
        else if (collision.CompareTag("Player") && gameObject.name == "LoadCameraTrigger")
        {
            var transitionCollider = GameObject.Find("TransitionCameraCollider").GetComponent<CompositeCollider2D>();
            SetBoundingShape(transitionCollider);
            gameObject.SetActive(false);
        }
    }
}
