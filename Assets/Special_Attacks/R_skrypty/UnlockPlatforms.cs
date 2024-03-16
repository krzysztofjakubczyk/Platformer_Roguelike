using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPlatforms : MonoBehaviour
{

    [SerializeField] GameObject Platform1;
    [SerializeField] GameObject Platform2;
    [SerializeField] GameObject Platform3;
    [SerializeField] GameObject Platform4;
    [SerializeField] GameObject Platform5;
    [SerializeField] GameObject Platform6;
    [SerializeField] GameObject Platform7;
    [SerializeField] GameObject Platform8;
    [SerializeField] GameObject Platform9;

    List<GameObject> platforms = new List<GameObject>();

    private void Start()
    {
        platforms.Add(Platform1);
        platforms.Add(Platform2);
        platforms.Add(Platform3);
        platforms.Add(Platform4);
        platforms.Add(Platform5);
        platforms.Add(Platform6);
        platforms.Add(Platform7);
        platforms.Add(Platform8);
        platforms.Add(Platform9);

        foreach (GameObject platform in platforms)
        {
            if (platform != null)
                platform.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject platform in platforms)
        {
            if (platform != null)
                platform.SetActive(true);
        }
    }
}
