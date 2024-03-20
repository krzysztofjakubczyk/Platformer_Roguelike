using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpell : Spell
{
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask platformLayer;
    [SerializeField] float delay;

    float wallDistance;

    void Start()
    {
        Invoke(nameof(Attack), delay);

    }

    public override void Attack()
    {

        wallDistance = maxDistance;

        //12 to przewidywa
        if (Physics2D.Raycast(player.transform.position, castDirection, maxDistance, platformLayer))
        {
            RaycastHit2D wall = Physics2D.Raycast(player.transform.position, castDirection, maxDistance, platformLayer);

            wallDistance = Mathf.Abs(wall.transform.position.x - player.transform.position.x);
        }

        RaycastHit2D[] laserShoted = Physics2D.RaycastAll(player.transform.position, castDirection, wallDistance, enemyLayer);

        foreach (RaycastHit2D laserShoted2 in laserShoted)
        {
            laserShoted2.rigidbody.AddForce(Vector2.up * 50, ForceMode2D.Impulse);
            print(laserShoted2.transform.name);
        }

        Destroy(gameObject);
    }


}
