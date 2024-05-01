using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Anim_State : MonoBehaviour
{
    public enum playerStates{running, idle, jumping, attack, hurt, dead, recover};
    public playerStates state;
    public playerStates currentState;
    public playerStates previousState;


    void Start()
    {
        state = playerStates.idle;
        currentState = playerStates.idle;
        previousState = playerStates.idle;
    }

    void Update()
    {
        
    }

    void ChangeState(playerStates newState)
    {
        


    }




}
