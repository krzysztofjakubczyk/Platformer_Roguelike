using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneBetweenTwoScenes : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject loaderCanvas;
    [SerializeField] GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ShowLoaderAndLoadNextFloor());
    }
    public IEnumerator ShowLoaderAndLoadNextFloor()
    {
        slider.value = 0f;

        float loadTime = 5f;  // Czas ³adowania
        float elapsedTime = 0f;

        // Pêtla oczekuj¹ca na zakoñczenie ³adowania
        while (elapsedTime < loadTime)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Clamp01(elapsedTime / loadTime);
            yield return null;
        }
    }
}
