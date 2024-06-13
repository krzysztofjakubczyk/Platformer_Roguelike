using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StatConroller : MonoBehaviour      
{
    public delegate void AmountChange();
    public static event AmountChange BothBarsCheck;

    [SerializeField] protected float maxAmount = 100;
    [SerializeField] protected float currentAmount;
    [SerializeField] protected float recoverTime = 0;
    [SerializeField] protected bool canRecover = false;

    bool RecoverIsRunning;


    public IEnumerator RecoverNew()
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

   

    public virtual void SubAmount(float amount)
    {
        if (currentAmount < amount)
        {
            currentAmount = 0;
            if (GetComponent<moveSnake>() != null)
                GetComponent<moveSnake>().OnDeath();
        }
        else
            currentAmount -= amount;

        if (!RecoverIsRunning)
            StartCoroutine(RecoverNew());

        return;
    }

    public virtual bool AddAmount(float amount)
    {
        if (currentAmount >= maxAmount)
            return false;


        currentAmount += amount;
        BothBarsCheck?.Invoke();

        return true;
    }

    public virtual float AddMaxAmount(float amount)
    {
        maxAmount += amount;

        if (!RecoverIsRunning)
            StartCoroutine(RecoverNew());
        return maxAmount;
    }

    public virtual void SubMaxAmount(float amount)
    {
        maxAmount -= amount;

        BothBarsCheck?.Invoke();
    }

    public virtual void SetRecoverTime(float time)
    {
        recoverTime = time;
    }
    public float GetMaxAmount() { return maxAmount; }
    public float GetCurrentAmount() { return currentAmount; }

}
