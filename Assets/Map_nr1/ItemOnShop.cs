using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnShop : MonoBehaviour
{
    [SerializeField] Items m_ScriptableObject;
    MoneyManager m_MoneyManager;
    private int costOfItem;
    bool isItemBoughtByPlayer = false;
    private void Start()
    {
        costOfItem = m_ScriptableObject.Cost;
        m_MoneyManager=FindAnyObjectByType<MoneyManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Player") && isItemBoughtByPlayer == false) //jezeli to player i nie ma kupionego przedmiotu to wyswietl jego opis
        {
            //odpal na kompue opis
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