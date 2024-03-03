using UnityEngine;

public abstract class EnemyMovementInterface : MonoBehaviour
{
    public void StartRightToLeftMove(Rigidbody2D rb, float _movementSpeed)
    {
        rb.velocity = transform.right * _movementSpeed;
    }

}
