using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ItemOnShop : MonoBehaviour
{
    [SerializeField] protected Items m_ScriptableObject;
    protected MoneyManager m_MoneyManager;
    protected TMP_Text name, description;
    protected GameObject image;
    protected int costOfItem;
    protected SpriteRenderer spriteRenderer;
    protected float power;
    protected bool isItemBoughtByPlayer = false;
    protected bool playerIsClose;
    protected GameObject player;

    private void Start()
    {
        costOfItem = m_ScriptableObject.Cost;
        m_MoneyManager=FindAnyObjectByType<MoneyManager>();
        name = GameObject.Find("NameOfItemInShop").GetComponent<TMP_Text>();
        description = GameObject.Find("DescriptionOfItemInShop").GetComponent<TMP_Text>();
        image = GameObject.Find("ImageOfItemInShop");
        player = GameObject.Find("Player");
        spriteRenderer = image.GetComponent<SpriteRenderer>();
        power = m_ScriptableObject.power;
    }

    private void Update()
    {
        if(!playerIsClose)
            return;

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (CheckIfEnoughMoney(m_ScriptableObject))
            {
                m_MoneyManager.SubMoney(costOfItem);

                // wywolanie uzycia przedmiotu
                UseFunction(power);
                    
                Destroy(gameObject);
                //opcjonalnie dodaj do inventory- polaczenie z Dawidem
            }
            else
            {
                print("Player doesnt have enough money to buy it"); //koncowo wyswietla sie na kompie
            }
        }
    }
    

    bool CheckIfEnoughMoney(Items SOtoComparison)
    {
        if (SOtoComparison.Cost > m_MoneyManager.GetMoney())
            return false;
        else 
            return true;
    }

    public abstract void UseFunction(float x);
}