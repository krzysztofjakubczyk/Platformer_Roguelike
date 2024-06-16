using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsCloneManager : MonoBehaviour
{
    GameObject bounding;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        SetNewPositionForCameraCollider();
    }
    private void SetNewPositionForCameraCollider()
    {
        bounding = GameObject.Find("CameraLoadTrigger");
        bounding.gameObject.transform.position = gameObject.transform.position - new Vector3(0, -1.53f, 0);
        Debug.Log("zmieniono pozycje triggera do kamery");
    }
}
