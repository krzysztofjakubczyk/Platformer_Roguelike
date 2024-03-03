using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Vector3 offset = new Vector3(2.5f, 0, -10f);
    private Vector3 velocity = Vector3.zero;

    [SerializeField] float smoothTime = 0.15f;
    [SerializeField] Transform target;

    bool camSide;
    bool notYetChanged_Y;

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    private void Start()
    {
        target.GetComponent<PlayerMovement>().directionChange += dirChange;

        camSide = true;
        notYetChanged_Y = true;
    }

    private void Update()
    {
        if(target.GetComponent<PlayerMovement>().IsGrounded() && notYetChanged_Y)
        {
            offset.y = target.transform.position.y + 2f;
            notYetChanged_Y=false;
            Invoke(nameof(Ychange), 0.3f);

        }
        Vector3 targetPosition = new Vector3( target.position.x + offset.x,  offset.y, -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    void dirChange()
    {
        if (camSide == true)        camSide = false;
        else if (camSide == false)  camSide = true;

        if (camSide)
            offset += new Vector3(5, 0, 0);
        else
            offset -= new Vector3(5, 0, 0);

        //smoothTime = 0.5f;
        //Invoke(nameof(timeBack), 1f);

    }

    void timeBack()
    {
        smoothTime = 0.15f;
    }

    void Ychange()
    {
        notYetChanged_Y = true;
    }
}
