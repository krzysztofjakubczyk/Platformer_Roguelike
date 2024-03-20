using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] healthController health;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && health.GetHealth() > 1)
        {
            Debug.Log("uderzono gracza");
            health.MinusHP(1);
        }
        else if (collision.transform.tag == "Player" && health.GetHealth() <= 1)
        {
            health.MinusHP(1);
            health.RestartHealth();
            //PlayerTeleport(collision.transform);
        }
        else Debug.Log("Nie udalo sie");
    }
    //private void PlayerTeleport(Transform playerTransform)
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(playerTransform.position, Vector3.down, out hit))
    //    {
    //        playerTransform.position = hit.point + Vector3.up * playerTransform.GetComponent<Collider>().bounds.extents.y;
    //    }
    //}
}
