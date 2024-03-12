using UnityEngine;

public class EnemyMovementInterface : MonoBehaviour
{
    public void StartRightToLeftMove(Rigidbody2D rb, float _movementSpeed)
    {
        rb.velocity = transform.right * _movementSpeed;
    }
    public void EnemySawPlayer(GameObject player, Rigidbody2D enemy, float _movementSpeed)
    {
        Vector3 direction = (player.transform.position - enemy.transform.position).normalized;

        if (Mathf.Abs(player.transform.position.x - enemy.transform.position.x) <= 5)
        {
            Debug.Log("sa blisko");
            enemy.velocity = direction * _movementSpeed;
        }
    }
}