using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemOnShop : MonoBehaviour
{
    [SerializeField] protected Items m_ScriptableObject;

    MoneyManager m_MoneyManager;
    TMP_Text name, description;

    string itemName, itemDescription;
    int itemCost;
    PlayerStatEnum playerStat;
    float power;

    bool playerIsClose = false;
    GameObject player;


    private void Start()
    {
        m_MoneyManager=FindAnyObjectByType<MoneyManager>();
        name = GameObject.Find("NameOfItemInShop").GetComponent<TMP_Text>();
        description = GameObject.Find("DescriptionOfItemInShop").GetComponent<TMP_Text>();
        player = GameObject.Find("Player");

        GetComponent<SpriteRenderer>().sprite = m_ScriptableObject.ImageItem;
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

        name.text = itemName + "  Cost: " + itemCost.ToString();
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

}