using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay((transform.position + transform.right), transform.right, Color.red, 0.2f);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit2D hit = Physics2D.Raycast((transform.position + transform.right), transform.right, 0.2f);
            
            if (hit.collider != null && hit.collider.tag == "ObstacleWall")
            {
                // Zniszcz collider, który jest na drodze
                Destroy(hit.collider.gameObject);   
            }
        }
    }
}
