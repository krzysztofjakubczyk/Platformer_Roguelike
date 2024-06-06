using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSnake : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    [SerializeField] float speed;
    [SerializeField] float walkTime;
    bool rightMove;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        StartCoroutine(SwitchDir());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("armHit");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject throwdart = Instantiate(transform.GetChild(0).gameObject);
            throwdart.transform.parent = gameObject.transform;
            throwdart.transform.localScale = Vector3.one;
            throwdart.transform.position = transform.GetChild(0).position;
            throwdart.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if(rightMove)
            rb.velocity = Vector2.right * speed;
        else 
            rb.velocity = Vector2.left * speed;
    }

    IEnumerator SwitchDir()
    {
        while (true)
        {
            if(rightMove)
                rightMove = false;
            else 
                rightMove = true;
            yield return new WaitForSeconds(walkTime);
        }
    }
}
