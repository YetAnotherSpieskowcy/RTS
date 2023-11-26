using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterMode
{
    NORMAL_MODE,
    COMBAT_MODE,
    BUILDING_MODE
}

public class CharacterData
{

    private float health, maxHealth, healingStep;
    private bool alive, needsHealing;

    private CharacterMode mode;

    public CharacterData()
    {
        maxHealth = 100f;
        health = 100f;
        healingStep = 5f;
        alive = true;
        needsHealing = false;
        mode = CharacterMode.NORMAL_MODE;
    }

    public void UpdateMode(CharacterMode m)
    {
        mode = m;
        if (m == CharacterMode.COMBAT_MODE)
        {
            // buildMechanismMediator.SetAction(Action.UNAVAILABLE);
        }
        else if (m == CharacterMode.BUILDING_MODE)
        {
            mode = m;
        }
    }

    public CharacterMode GetMode()
    {
        return mode;
    }

    public bool CheckIfAlive()
    {
        return alive;
    }
}
