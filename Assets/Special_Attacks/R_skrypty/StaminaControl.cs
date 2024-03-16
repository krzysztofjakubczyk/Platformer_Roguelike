using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaminaControl : MonoBehaviour
{
    [SerializeField] float maxStamina = 100;
    [SerializeField] float currentStamina = 0;
    [SerializeField] float recoverTime = 0.3f;
    
    [SerializeField] bool staminaRecover = true;

    bool GettingStaminaIsRunning;
    

    private void Start()
    {
        StartCoroutine(GettingStamina());
    }

    IEnumerator GettingStamina()
    {
        GettingStaminaIsRunning = true;

        while(staminaRecover && currentStamina < maxStamina)
        {
            currentStamina += 1;
            yield return new WaitForSeconds(recoverTime);
        }

        GettingStaminaIsRunning = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SubStamina(20);
        }
    }
    public bool SubStamina(float amount)
    {
        if(currentStamina < amount)
            return false;

        currentStamina -= amount;

        if(!GettingStaminaIsRunning)
            StartCoroutine(GettingStamina());

        return true;
    }

    public bool AddStamina(float amount)
    {
        if (currentStamina >= maxStamina)
            return false;

        currentStamina += amount;
        return true;
    }

    public void AddMaxStamina(float amount)
    {
        maxStamina += amount;

        if (!GettingStaminaIsRunning)
            StartCoroutine(GettingStamina());
    }

    public void SubMaxStamina(float amount)
    {
        maxStamina -= amount;
    }


}
