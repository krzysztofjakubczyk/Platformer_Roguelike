using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyMovement : EnemyMovementInterface
{
    GameObject player;
    Rigidbody2D _rb;
    float _movementSpeed = 1f;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        StartRightToLeftMove(_rb, _movementSpeed);
        EnemySawPlayer(player, _rb, _movementSpeed);
    }
}

