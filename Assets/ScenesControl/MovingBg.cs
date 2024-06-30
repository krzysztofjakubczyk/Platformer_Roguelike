using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MovingBg : MonoBehaviour
{
    float maxPos = 800;
    float minPos = 200;
    [SerializeField] float speed = 10;

    bool moveUp;

    void Start()
    {
        if (GetComponent<RectTransform>().position.y >= maxPos)
            moveUp = true;
        else
            moveUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveUp)
        {
            GetComponent<RectTransform>().position = new Vector2(transform.position.x, transform.position.y - 1 * Time.deltaTime * speed);
            if(GetComponent<RectTransform>().position.y <= minPos)
            {
                moveUp = false;
                print(GetComponent<RectTransform>().position.y);

            }
        }
        else
        {
            GetComponent<RectTransform>().position = new Vector2(transform.position.x, transform.position.y + 1 * Time.deltaTime * speed);
            if (GetComponent<RectTransform>().position.y >= maxPos)
                moveUp = true;
        }
    }
}
