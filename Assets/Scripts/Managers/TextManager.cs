using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class TextManager : MonoBehaviour
{
    
    [SerializeField]
    GameObject inventory;
    [SerializeField]
    List<TMP_Text> StatsTexts;
    [SerializeField]
    List<Image> SpellUIHolders;
    [SerializeField]
    List<Image> SpellUIHoldersInventory;

    Dictionary<string, float> StatsValues;
    Dictionary<string, string> SpellValues;
    public Dictionary<int, Sprite> SpellIcons;
    private int currentSpellIndex = 0;
    
    [SerializeField]
    GameObject spellListParent;
    [SerializeField]
    GameObject descriptionParent;
    [SerializeField]
    GameObject upgradeListParent;
    [SerializeField]
    GameObject player;

    PlayerStatsManager playerStatsManager;
    MoneyManager moneyManager;
    SpellManager spellManager;
    ItemOnShop itemOnShop;
    Coin coin;


    // Start is called before the first frame update
    void Start()
    {
        playerStatsManager = player.GetComponent<PlayerStatsManager>();
        moneyManager = player.GetComponent<MoneyManager>();
        spellManager = player.GetComponent<SpellManager>();
        
        SpellValues = new Dictionary<string, string>();
        SpellIcons = new Dictionary<int, Sprite>();
        for (int i = 0; i < 5; i++)
        {
            SpellIcons.Add(i, spellManager.spells[i].GetComponent<Spell>().spellData.ImageItem);
        }
            for (int i = 0; i < 3; i++)
        {
            Sprite sprite;
            SpellIcons.TryGetValue(i, out sprite);
            SpellUIHolders[i].sprite = sprite;
            inventory.SetActive(true);
            SpellUIHoldersInventory[i].sprite = sprite;
            inventory.SetActive(false);
        }
        

        foreach(var spell in spellManager.spells)
        {
            Items spellInstance = spell.GetComponent<Spell>().spellData;
            //dodawanie stringów do s³ownika nazw spelli i ich opisów
            string spellName = spellInstance.Name;
            string spellDescription = spellInstance.Description;
            SpellValues.Add(spellName, spellDescription);
            //dodawanie ikony i ustawianie w odpowiednim miejscu w UI
            Image newImage = new GameObject(spellName).AddComponent<Image>();
            newImage.transform.SetParent(spellListParent.transform); // ustaw parentTransform na odpowiedni transform rodzica dla obiektów Image
            Sprite sprite = spellInstance.ImageItem;
            newImage.sprite = sprite;
            //dodanie triggeru najechania na obrazek i wyœwietlenie opisu spella
            EventTrigger eventTrigger = newImage.gameObject.AddComponent<EventTrigger>();

            // Tworzenie nowego wpisu dla zdarzenia PointerEnter
            EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
            pointerEnterEntry.eventID = EventTriggerType.PointerEnter;

            // Dodawanie funkcji do wywo³ania, gdy zdarzenie PointerEnter zostanie wywo³ane
            pointerEnterEntry.callback.AddListener((eventData) => {
                // Tutaj umieœæ kod, który ma byæ wykonany podczas zdarzenia PointerEnter
                Debug.Log("Pointer entered on the image");
                ShowDescription(newImage.gameObject);
            });

            // Dodawanie wpisu do EventTrigger
            eventTrigger.triggers.Add(pointerEnterEntry);
        }
        inventory.SetActive(!inventory.activeSelf);
        StatsValues = new Dictionary<string, float>()
        {
            {"HealthValue", playerStatsManager.playerStats.stats[0].value},
            {"StaminaValue", playerStatsManager.playerStats.stats[3].value},
            {"AttackDamageValue",playerStatsManager.playerStats.stats[6].value},
            {"AttackSpeedValue", playerStatsManager.playerStats.stats[8].value},
            {"MovementValue", playerStatsManager.playerStats.stats[15].value},
            {"JumpPowerValue", playerStatsManager.playerStats.stats[14].value},
            {"DashPowerValue", playerStatsManager.playerStats.stats[16].value},
            {"GoldValue", moneyManager.GetMoney()},
            {"GoldValueInterface", moneyManager.GetMoney() }
        };
        playerStatsManager.updateGUI += UpdateGUI;
        ItemOnShop.updateGUIUpgrades += UpdateUpgrades;
        ItemOnShop.showDescription += ShowDescription;
        moneyManager.updateGUI += UpdateGUI;
        UpdateGUI();
        inventory.SetActive(false);
    }
    private void UpdateGUI()
    {
        foreach (var stat in StatsTexts)
        {
            StatsValues["HealthValue"] = playerStatsManager.playerStats.stats[0].value;
            StatsValues["StaminaValue"] = playerStatsManager.playerStats.stats[3].value;
            StatsValues["AttackDamageValue"] = playerStatsManager.playerStats.stats[6].value;
            StatsValues["AttackSpeedValue"] = playerStatsManager.playerStats.stats[8].value;
            StatsValues["MovementValue"] = playerStatsManager.playerStats.stats[15].value;
            StatsValues["JumpPowerValue"] = playerStatsManager.playerStats.stats[14].value;
            StatsValues["DashPowerValue"] = playerStatsManager.playerStats.stats[16].value;
            StatsValues["GoldValue"] = moneyManager.GetMoney();
            StatsValues["GoldValueInterface"] = moneyManager.GetMoney();

            float value = 0;
            StatsValues.TryGetValue(stat.name, out value);

            stat.text = value.ToString();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
        { 
            UpdateGUI();
            inventory.SetActive(!inventory.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.D)) ChangeSpellUI();
        if (Input.GetKeyDown(KeyCode.A)) ChangeSpellUILeft();
    }
    private void UpdateUpgrades(GameObject prefabOfItem)
    {
        GameObject newUpgrade = Instantiate(prefabOfItem);
        Destroy(newUpgrade.GetComponent<SpriteRenderer>());
        newUpgrade.transform.SetParent(upgradeListParent.transform);
        Image image = newUpgrade.AddComponent<Image>();
        Items newItem = newUpgrade.GetComponent<ItemOnShop>().m_ScriptableObject;
        image.sprite = newItem.ImageItem;
    }


        public void ShowDescription(GameObject prefabOfItem)
    {
        print("wykryto mysz");
        if(prefabOfItem.GetComponent<ItemOnShop>()!=null)
            descriptionParent.GetComponent<TMP_Text>().text = prefabOfItem.GetComponent<ItemOnShop>().m_ScriptableObject.Description;
        else 
        {
            foreach(var spell in SpellValues)
            {
                if(spell.Key == prefabOfItem.gameObject.name)
                {
                    descriptionParent.GetComponent<TMP_Text>().text = spell.Value;
                }
            }
        }
            
    }
    public void ChangeSpellUI()
    {
        if (currentSpellIndex == -2) currentSpellIndex=3;
        if (currentSpellIndex < 4) currentSpellIndex++;
        else currentSpellIndex = 0;
        for (int i=0; i < 3; i++)
        {
            if (currentSpellIndex + i >= 5)
            {
                if(i==1) currentSpellIndex = -1;
                else if(i==2) currentSpellIndex = -2;
            }
            int index = currentSpellIndex + i;
            if(i==1)spellManager.setSpell(spellManager.spells[index]);
            //print("currentSpellIndex " + currentSpellIndex + " idnex " + index);
            Sprite sprite;
            SpellIcons.TryGetValue(index, out sprite);
            SpellUIHolders[i].sprite = sprite;
            inventory.SetActive(true);
            SpellUIHoldersInventory[i].sprite = sprite;
            inventory.SetActive(false);
        }
    }
    public void ChangeSpellUILeft()
    {
        // if (currentSpellIndex == -1) currentSpellIndex = 3;
        if (currentSpellIndex == -2) currentSpellIndex = 3;
        if (currentSpellIndex > 0) currentSpellIndex--;
        if (currentSpellIndex == 0) currentSpellIndex = 4;
        for (int i = 0; i < 3; i++)
        {
            int index = currentSpellIndex + i;
            if (index > 4) index -= 5; 
            if (i == 1) spellManager.setSpell(spellManager.spells[index]);
            //print("currentSpellIndex " + currentSpellIndex + " index " + index);
            Sprite sprite;
            SpellIcons.TryGetValue(index, out sprite);
            SpellUIHolders[i].sprite = sprite;
            inventory.SetActive(true);
            SpellUIHoldersInventory[i].sprite = sprite;
            inventory.SetActive(false);
        }
    }

}
