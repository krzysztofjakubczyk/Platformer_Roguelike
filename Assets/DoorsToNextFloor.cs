using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class DoorsToNextFloor : MonoBehaviour
{
    [SerializeField] private CompositeCollider2D colliderForCamera;
    BoundingShapeScripts boundingShape;
    [SerializeField] private GameObject loaderCanvas;
    private GameObject player;
    [SerializeField]private Slider slider;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name != "Player")
        {
            return;
        }
        OnTrigger();
    }

    private void OnTrigger()
    {
        SceneController sceneManager = FindObjectOfType<SceneController>();
        sceneManager.AfterBossDeath();
        player.transform.position =  gameObject.transform.position + new UnityEngine.Vector3(26 , 0, 0);
        StartCoroutine(ShowLoaderAndLoadNextFloor());
    }

    private IEnumerator ShowLoaderAndLoadNextFloor()
    {
        loaderCanvas.SetActive(true);
        slider.value = 0f;

        float loadTime = 5f;  // Czas ³adowania
        float elapsedTime = 0f;

        while (elapsedTime < loadTime)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Clamp01(elapsedTime / loadTime);
            yield return null;
        }
         // W³¹cz ekran ³adowania
        yield return new WaitForSeconds(5);  // Czekaj 5 sekund
        SceneController sceneManager = FindObjectOfType<SceneController>();
        sceneManager.AfterBossDeath();
        loaderCanvas.SetActive(false);  // Wy³¹cz ekran ³adowania
    }

}