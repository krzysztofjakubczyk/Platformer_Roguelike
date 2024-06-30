//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class FollowTarget : MonoBehaviour
//{
//    private Vector3 offset = new Vector3(1f, 0, -10f);
//    private Vector3 velocity = Vector3.zero;

//    [SerializeField] float smoothTime = 0.15f;
//    [SerializeField] Transform target;

//    bool camSide;
//    bool notYetChanged_Y;
//    bool notGrounded;

//    private void Awake()
//    {
//        Application.targetFrameRate = 120;
//    }

//    private void Start()
//    {

//        camSide = true;
//        notYetChanged_Y = true;
//        notGrounded = true;
//    }

//    private void Update()
//    {
//        if(target.GetComponent<PlayerMovement>().IsGrounded() && notGrounded)// && notYetChanged_Y)
//        {

//            Invoke(nameof(Ychange), 0.1f);
//            notGrounded = false;
//            Invoke(nameof(moznajuzskakacznowu), 0.1f);

//        }

       
//        Vector3 targetPosition = new Vector3( target.position.x + offset.x,  offset.y, -10);
//        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
//    }

//    void timeBack()
//    {
//        smoothTime = 0.15f;
//    }

//    void Ychange()
//    {
//        offset.y = target.transform.position.y + 2f;
//    }

//    void moznajuzskakacznowu()
//    {
//        notGrounded = true;
//    }
//}
