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
        GetCamera();
        GetCollider();
    }
    private CinemachineConfiner2D GetCamera()
    {
        mainCameraConfiner = GameObject.Find("MainCamera").GetComponent<CinemachineConfiner2D>();
        return mainCameraConfiner;
    }
    private CompositeCollider2D GetCollider()
    {
        colliderForCamera = GameObject.Find("ColliderForCamera").GetComponent<CompositeCollider2D>();
        return colliderForCamera;
    }
    public void SetBoundingShape(CinemachineConfiner2D mainCameraConfiner, CompositeCollider2D collider)
    {
        mainCameraConfiner.m_BoundingShape2D = collider;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("wykryto gracza");
            Debug.Log(mainCameraConfiner.name);
            Debug.Log(GetCollider().name);
            SetBoundingShape(mainCameraConfiner,GetCollider());
            Destroy(gameObject);
        }
    }
}
