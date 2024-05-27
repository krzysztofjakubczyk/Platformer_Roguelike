using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemOnShop : MonoBehaviour
{
    [SerializeField] Items m_ScriptableObject;
    MoneyManager m_MoneyManager;
    TMP_Text name, description;
    GameObject image;
    private int costOfItem;
    SpriteRenderer spriteRenderer;
    bool isItemBoughtByPlayer = false;
    bool playerIsClose;


    private void Start()
    {
        costOfItem = m_ScriptableObject.Cost;
        m_MoneyManager=FindAnyObjectByType<MoneyManager>();
        name = GameObject.Find("NameOfItemInShop").GetComponent<TMP_Text>();
        description = GameObject.Find("DescriptionOfItemInShop").GetComponent<TMP_Text>();
        image = GameObject.Find("ImageOfItemInShop");
        spriteRenderer = image.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(playerIsClose)
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (CheckIfEnoughMoney(m_ScriptableObject))
                {
                    m_MoneyManager.SubMoney(costOfItem);
                    //opcjonalnie dodaj do inventory- polaczenie z Dawidem
                    Destroy(gameObject);
                }
                else
                {
                    print("Player doesnt have enough money to buy it"); //koncowo wyswietla sie na kompie
                }
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;


        playerIsClose = true;

        name.text = m_ScriptableObject.Name;
        name.text += "  Cost: " + m_ScriptableObject.Cost.ToString();
        description.text = m_ScriptableObject.Description;
        spriteRenderer.sprite = m_ScriptableObject.ImageItem;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            playerIsClose = false;
    }

    bool CheckIfEnoughMoney(Items SOtoComparison)
    {
        if (SOtoComparison.Cost > m_MoneyManager.GetMoney())
            return false;
        else 
            return true;
    }
}