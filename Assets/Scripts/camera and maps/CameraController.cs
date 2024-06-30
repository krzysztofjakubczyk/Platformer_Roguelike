using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instiate;
    public static bool isNormalCollider { get;  set; } = true;
    public static bool isTransitionCollider { get;  set; } = true;
    private void Awake()
    {
        if (instiate == null)
        {
            instiate = this;
        }
    }
    public bool GetisNormalCollider()
    {
        return isNormalCollider;
    }
    public void SetNormalCollider(bool isColliderUsed)
    {
        isNormalCollider = isColliderUsed;
    }
    public bool GetisTransitionCollider()
    {
        return isTransitionCollider;
    }
    public void SetTransitionCollider(bool isColliderUsed)
    {
        isTransitionCollider = isColliderUsed;
    }
}
