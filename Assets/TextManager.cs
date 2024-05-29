using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    /*
    [SerializeField]
    GameObject inventory;
    [SerializeField]
    List<TMP_Text> StatsTexts;

    Dictionary<string, float> StatsValues;

    [SerializeField]
    GameObject player;

    PlayerStats playerStats;

    [SerializeField] 
    private TMP_Text goldText;
    [SerializeField]
    private TMP_Text hpText;
    [SerializeField]
    private TMP_Text staminaText;
    [SerializeField]
    private TMP_Text adText;
    [SerializeField]
    private TMP_Text asText;
    [SerializeField]
    private TMP_Text movementText;
    [SerializeField]
    private TMP_Text jpText; //jump Power
    [SerializeField]
    private TMP_Text dpText; //dash Power

    // Start is called before the first frame update
    void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
        
        StatsValues = new Dictionary<string, float>()
        {
            {"HealthValue", playerStats.hp},
            {"StaminaValue", playerStats.stamina},
            {"AttackDamageValue", playerStats.attackDamage},
            {"AttackSpeedValue", playerStats.attackSpeed},
            {"MovementValue", playerStats.movementSpeed},
            {"JumpPowerValue", playerStats.jumpPower},
            {"DashPowerValue", playerStats.dashPower},
            {"GoldValue", playerStats.dashPower}
        };
        playerStats.updateGUI += updateGUI;
        //updateGUI();
    }
    private void updateGUI()
    {
        foreach (var stat in StatsTexts)
        {
            float value = 0;
            StatsValues.TryGetValue(stat.name, out value);
            stat.text = value.ToString();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SetActive(!inventory.activeSelf);
            updateGUI();
        }
    }
    */
}
