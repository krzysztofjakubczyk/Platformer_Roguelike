using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{ //oczywiscie sceny do zaladowania beda wybierane losowo, na czas testow sa wybierane przez nazwe podana w inspektorze
    [SerializeField]MapTranistion mapInstance;
    [Header("Name of scenes to make visible or not")]
    [SerializeField] string[] _scenesToLoad;
    [SerializeField] string[] _scenesToUnLoad;
    GameObject _OutsideDoors; //drzwi umozliwiajace graczowi przejscie do nastepnego pokoju
    GameObject _InsideDoors; //drzwi uniemozliwiajace graczowi przejscie do poprzedniego pokoju
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _OutsideDoors = GameObject.Find("OutDoors");
        _InsideDoors = mapInstance.FindDisabledObjectByName<Transform>("InDoors")?.gameObject; //to pewnie nie za dziala jak nie bedzie inside i wywali blad
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.name == "LoadRoomTrigger")
        {//jezeli jest to gracz i jest to tiger ladujacy pokoj
            LoadScene();
        }
        else if (collision.CompareTag("Player") && gameObject.name == "UnLoadRoomTrigger")
        {//jezeli jest to gracz i jest to tiger odladowujacy pokoj
            UnLoadScene();
        }
    }

    private void UnLoadScene()
    {
        _InsideDoors.SetActive(true); //wlacz drzwi zeby nie mozna bylo sie cofnac
        for (int i = 0; i < _scenesToUnLoad.Length; i++) //wylacz scene poprzednia (narazie statycznie podana)
        {
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if (loadedScene.name == _scenesToUnLoad[i])
                {
                    SceneManager.UnloadSceneAsync(_scenesToUnLoad[i]);
                }
            }
        }
    }

    private void LoadScene()
    {
        _OutsideDoors.SetActive(false); //wylacz drzwi zeby mozna bylo przejsc dalej
        for (int i = 0; i < _scenesToLoad.Length; i++)//dograj scene nastepna
        {
            bool isSceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadScene = SceneManager.GetSceneAt(j);
                if (loadScene.name == _scenesToLoad[i])
                {
                    isSceneLoaded = true;
                    break;
                }
            }
            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(_scenesToLoad[i], LoadSceneMode.Additive);
            }   
        }
    }
}
