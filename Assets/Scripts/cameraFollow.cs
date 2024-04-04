using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(1f, 0, -35f);

    void Update()
    {
        Vector3 desiredPosition = player.position + offset;
        transform.position = desiredPosition;
    }
}