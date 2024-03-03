using UnityEngine;

public class SpiderEnemy : EnemyMovementInterface
{
    Rigidbody2D _rb;
    float _movementSpeed = 3f;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        StartRightToLeftMove(_rb, _movementSpeed);
    }
    


}
