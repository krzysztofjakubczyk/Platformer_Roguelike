using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class DoorsToNextFloor : MonoBehaviour
{
    [SerializeField] private CompositeCollider2D colliderForCamera;
    BoundingShapeScripts boundingShape;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name != "Player")
        {
            return;
        }
        OnTrigger();
    }

    private void OnTrigger()
    {
        SceneController sceneManager = FindObjectOfType<SceneController>();
        sceneManager.AfterBossDeath();
        player.transform.position =  gameObject.transform.position + new UnityEngine.Vector3(26 , 0, 0);

    }
}