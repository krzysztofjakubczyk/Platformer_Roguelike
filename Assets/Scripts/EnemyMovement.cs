using UnityEngine;

public class EnemyMovement : EnemyMovementInterface
{
    [SerializeField] GameObject player;
    Rigidbody2D _rb;
    float _movementSpeed = 3f;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        StartRightToLeftMove(_rb, _movementSpeed);
        EnemySawPlayer(player, _rb, _movementSpeed);
    }
}

    