using UnityEngine;

public class EnemyMovement : EnemyMovementInterface
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

    