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
        
        //StartCoroutine(ShowLoaderAndLoadNextFloor());
    }

    //private IEnumerator ShowLoaderAndLoadNextFloor()
    //{
    //    loaderCanvas.SetActive(true);
    //    slider.value = 0f;
    //    SceneController sceneManager = FindObjectOfType<SceneController>();
    //    var loadOperation = sceneManager.AfterBossDeathAsync();
        
    //    float loadTime = 5f;  // Czas ³adowania
    //    float elapsedTime = 0f;

    //    player.transform.position = gameObject.transform.position + new UnityEngine.Vector3(26, 0, 0);
    //    // Pêtla oczekuj¹ca na zakoñczenie ³adowania
    //    while (!loadOperation.isDone || elapsedTime < loadTime)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        slider.value = Mathf.Clamp01(elapsedTime / loadTime);
    //        yield return null;
    //    }

    //    // Wy³¹cz ekran ³adowania
    //    loaderCanvas.SetActive(false);
    //}
}
