using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterObstacle : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    bool ifShoot = true;
    [SerializeField] private float speed;

    void Update()
    {
        if(ifShoot) StartCoroutine(nameof(shoot));
    }
    private IEnumerator shoot()
    {
        ifShoot = false;
        yield return new WaitForSeconds(2);
        GameObject go = Instantiate(prefab, transform.position, Quaternion.Euler(0,0,90));
        go.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        Destroy(go, 2);
        ifShoot = true;
    }
}
