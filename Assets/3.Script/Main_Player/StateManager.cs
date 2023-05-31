using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    
    public State currentState;
    State tempState;

    [Header("ป๓ลย")]
    public State idle;
    public State move;
    public State[] dashstates;
    public State atk;
    public State dodge;

    private void Start()
    {
        currentState = idle;
    }
    public void ChangeState(State newState)
    {
        tempState = currentState;
        if (currentState != null)
            currentState.Exit(newState);
        currentState = newState;
        currentState.Enter(tempState);
    }

    private void Update()
    {
        if (currentState != null)
            currentState.StateUpdate();
    }
    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.StateFixedUpdate();
    }
}
