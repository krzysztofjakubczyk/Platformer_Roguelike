using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpell : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float staminaCost;
    [SerializeField] float damage;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask platformLayer;

    float wallDistance;

    Vector2 dir;

    Rigidbody2D rb;


    void Start()
    {
        // ustawic kierunek rzucania
        Vector2 dir = player.transform.right;

        wallDistance = maxDistance;

        //12 to przewidywa
        if(Physics2D.Raycast(player.transform.position, dir, maxDistance, platformLayer))
        {
            RaycastHit2D wall = Physics2D.Raycast(player.transform.position, dir, maxDistance, platformLayer);

            wallDistance = Mathf.Abs(wall.transform.position.x - player.transform.position.x);
        }

        RaycastHit2D[] laserShoted = Physics2D.RaycastAll(player.transform.position, dir, wallDistance, enemyLayer);

        foreach(RaycastHit2D laserShoted2 in laserShoted)
        {
            laserShoted2.rigidbody.AddForce(Vector2.up * 50, ForceMode2D.Impulse);
            print(laserShoted2.transform.name);
            print(wallDistance);
        }

    }


}
