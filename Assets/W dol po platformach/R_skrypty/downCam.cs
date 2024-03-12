using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downCam : MonoBehaviour
{
    private Vector3 offset = new Vector3(1f, 0, -10f);
    private Vector3 velocity = Vector3.zero;

    [SerializeField] float smoothTime = 0.15f;
    [SerializeField] Transform target;
    [SerializeField] float yOffset;

    bool camSide;
    bool notYetChanged_Y;
    bool notGrounded;

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    private void Start()
    {
        notYetChanged_Y = true;
        notGrounded = true;
    }

    private void Update()
    {
        if (target.GetComponent<mvmnt>().IsGrounded() && notGrounded)// && notYetChanged_Y)
        {

            Invoke(nameof(Ychange), 0.1f);
            notGrounded = false;
            Invoke(nameof(moznajuzskakacznowu), 0.1f);

        }

        Vector3 targetPosition = new Vector3(0, offset.y, -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if(Mathf.Abs((transform.position - target.position).magnitude) > 15)
        {
            target.position = new Vector2(-5.5f, 3);
        }
    }


    void timeBack()
    {
        smoothTime = 0.15f;
    }

    void Ychange()
    {
        offset.y = target.transform.position.y + yOffset;
    }

    void moznajuzskakacznowu()
    {
        notGrounded = true;
    }
}
