using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ItemOnShop : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] public Items m_ScriptableObject;
    [SerializeField] GameObject soundToPlay;

    MoneyManager m_MoneyManager;
    TMP_Text name, description, cost;

    string itemName, itemDescription;
    int itemCost;
    PlayerStatEnum playerStat;
    float power;

    bool playerIsClose = false;
    GameObject player;

    public delegate void UpdateGUIDelegate(GameObject prefab);

    public static event UpdateGUIDelegate updateGUIUpgrades;

    public static event UpdateGUIDelegate showDescription;

    private void Start()
    {
        m_MoneyManager=FindAnyObjectByType<MoneyManager>();
        name = GameObject.Find("NameOfItemInShop").GetComponent<TMP_Text>();
        description = GameObject.Find("DescriptionOfItemInShop").GetComponent<TMP_Text>();
        cost = GameObject.Find("CostOfItemInShop").GetComponent<TMP_Text>();
        player = GameObject.Find("Player");

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.sprite = m_ScriptableObject.ImageItem;
        else Debug.Log("Obiekt gry nie ma komponentu SpriteRenderer");
        
        itemName = m_ScriptableObject.Name;
        itemDescription = m_ScriptableObject.Description;
        playerStat = m_ScriptableObject.stat;
        power = m_ScriptableObject.power;
        itemCost = m_ScriptableObject.Cost;
    }

    private void Update()
    {
        if(!playerIsClose)
            return;

        if (!Input.GetKeyDown(KeyCode.B))
            return;

        if (CheckIfEnoughMoney(itemCost))
        {
            m_MoneyManager.SubMoney(itemCost);
      
            player.GetComponent<PlayerStatsManager>().UpdateStat(playerStat, power);
            updateGUIUpgrades?.Invoke(gameObject);
            Instantiate(soundToPlay);
            Destroy(gameObject);
        }
        else
            print("Player doesnt have enough money to buy item"); //koncowo wyswietla sie na kompie
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerIsClose = true;

        name.gameObject.GetComponent<MeshRenderer>().enabled = true;
        description.gameObject.GetComponent<MeshRenderer>().enabled = true;
        cost.gameObject.GetComponent<MeshRenderer>().enabled = true;

        cost.text = "Cost: " + itemCost.ToString();
        name.text = itemName;
        description.text = itemDescription;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerIsClose = false;
    }


    bool CheckIfEnoughMoney(float cost)
    {
        if (cost > m_MoneyManager.GetMoney())
            return false;
        else 
            return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("wchodzi na item myszek");
        showDescription?.Invoke(gameObject);
        
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        showDescription?.Invoke(gameObject);
        Debug.Log("wychodzi na item myszek");
       
    }
}