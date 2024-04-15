using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StatConroller : MonoBehaviour      
{
    public delegate void AmountChange();
    public static event AmountChange BothBarsCheck;

    [SerializeField] float maxAmount = 100;
    [SerializeField] float currentAmount;
    [SerializeField] float recoverTime = 0;
    [SerializeField] bool canRecover = false;


    bool RecoverIsRunning;


    public virtual void Start()
    {
        StartCoroutine(RecoverNew());
    }

    IEnumerator RecoverNew()
    {
        RecoverIsRunning = true;

        while (canRecover && currentAmount < maxAmount)
        {
            currentAmount += 1;

            BothBarsCheck?.Invoke();

            yield return new WaitForSeconds(recoverTime);
        }

        RecoverIsRunning = false;
    }


    public virtual bool SubStamina(float amount)
    {
        if (currentAmount < amount)
            return false;

        currentAmount -= amount;

        if (!RecoverIsRunning)
            StartCoroutine(RecoverNew());

        return true;
    }

    public virtual bool AddAmount(float amount)
    {
        if (currentAmount >= maxAmount)
            return false;


        currentAmount += amount;
        BothBarsCheck?.Invoke();

        return true;
    }

    public virtual void AddMaxAmount(float amount)
    {
        maxAmount += amount;

        if (!RecoverIsRunning)
            StartCoroutine(RecoverNew());
    }

    public virtual void SubMaxAmount(float amount)
    {
        maxAmount -= amount;

        BothBarsCheck?.Invoke();
    }

    public float GetMaxAmount() { return maxAmount; }
    public float GetCurrentAmount() { return currentAmount; }

}
