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
    private void Start()
    {
        costOfItem = m_ScriptableObject.Cost;
        m_MoneyManager=FindAnyObjectByType<MoneyManager>();
        name = GameObject.Find("NameOfItemInShop").GetComponent<TMP_Text>();
        description = GameObject.Find("DescriptionOfItemInShop").GetComponent<TMP_Text>();
        image = GameObject.Find("ImageOfItemInShop");
        spriteRenderer = image.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Player") && isItemBoughtByPlayer == false) //jezeli to player i nie ma kupionego przedmiotu to wyswietl jego opis
        {
            name.text = m_ScriptableObject.Name;
            description.text = m_ScriptableObject.Description;
            spriteRenderer.sprite = m_ScriptableObject.ImageItem;
        }
        else if (collision != null && collision.CompareTag("Player") && Input.GetKey(KeyCode.B) && isItemBoughtByPlayer == false)//jezeli kliknie b to zabierz kaske i
        {                                                                                                   //daj flage na true
            if (hasPlayerEnoughMoney(m_ScriptableObject))
            {
                m_MoneyManager.SubMoney(costOfItem);
                isItemBoughtByPlayer=true;
                //opcjonalnie dodaj do inventory- polaczenie z Dawidem
            }
            else
            {
                Debug.Log("Player doesnt have enough money for buy it"); //koncowo w grze jest to ze na kompie sie wyswietli napis ze juz to masz
            }
        }
        else if(collision != null && collision.CompareTag("Player") && Input.GetKey(KeyCode.B) && isItemBoughtByPlayer == true)
        {
            //na kompie pojawia sie ze masz juz ten przedmiot
        }
    }
    bool hasPlayerEnoughMoney(Items SOtoComparison)
    {
        int _playersMoney = m_MoneyManager.GetMoney();
        if (SOtoComparison.Cost > _playersMoney) { return false; }
        else { return true; }
    }
}