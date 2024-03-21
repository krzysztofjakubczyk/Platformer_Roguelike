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
    bool notGrounded;

    bool canCheck;

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    private void Start()
    {
        notGrounded = true;

        Invoke(nameof(camCancheck), 1);
    }

    private void Update()
    {
        if (target.GetComponent<mvmnt>().IsGrounded() && notGrounded)// && notYetChanged_Y)
        {

            Invoke(nameof(Ychange), 0.1f);
            notGrounded = false;
            Invoke(nameof(moznajuzskakacznowu), 0.1f);

        }

        Vector3 targetPosition = new Vector3(target.position.x + offset.x, offset.y, -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        Vector2 camPos = transform.position;
        Vector2 playPos = target.position;

        if(Mathf.Abs((camPos - playPos).magnitude) > 15 && canCheck)
        {
            print(transform.position + " cam poz");
            print(target.position + " player poz");
            print((transform.position - target.position).magnitude);
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

    void camCancheck()
    {
        canCheck = true;
    }
}
