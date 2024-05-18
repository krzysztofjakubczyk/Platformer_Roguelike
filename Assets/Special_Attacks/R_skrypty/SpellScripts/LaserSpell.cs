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

        Vector2 startPos = new Vector2(player.transform.position.x, player.transform.position.y +1);

        Vector2 throwDir;
        if (player.GetComponent<SpriteRenderer>().flipX)
            throwDir = Vector2.right;
        else
            throwDir = -Vector2.right;

        if (Physics2D.Raycast(startPos, throwDir, maxDistance, platformLayer))
        {
            RaycastHit2D wall = Physics2D.Raycast(startPos, throwDir, maxDistance, platformLayer);

            wallDistance = wall.distance;
        }

        RaycastHit2D[] laserShoted = Physics2D.RaycastAll(startPos, throwDir, wallDistance, enemyLayer);

        foreach (RaycastHit2D laserShoted2 in laserShoted)
        {
            AttackDetails attackDetails = new AttackDetails();
            attackDetails.position = transform.position;
            attackDetails.damageAmount = damage;
            attackDetails.stunDamageAmount = stunDamage;
            laserShoted2.transform.parent.GetComponent<Entity>().DamageGet(attackDetails);
            print(laserShoted2.transform.name);
        }

        Destroy(gameObject);
    }

}
