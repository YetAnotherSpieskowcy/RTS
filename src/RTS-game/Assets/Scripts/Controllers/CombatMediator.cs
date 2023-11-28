using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatModeState
{
    ACTIVE,
    INACTIVE,
    ENDING,
    ENDED
}

public class CombatMediator
{
    private CombatModeState state;

    public CombatMediator()
    {
        state = CombatModeState.INACTIVE;
    }

    public void SetState(CombatModeState s)
    {
        state = s;
    }

    public CombatModeState GetState()
    {
        return state;
    }
}
