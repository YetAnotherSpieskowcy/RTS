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

/*    if (needsHealing && mode != CharacterMode.COMBAT_MODE)      // if player isn't in a fight then regenerate
            {
                Heal(healingStep);
}*/

public CharacterMode GetMode()
    {
        return mode;
    }

    public void SetAlive(bool alive)
    {
        this.alive = alive;
    }

    public bool CheckIfAlive()
    {
        return alive;
    }

    public void Wound(float healthPoints)
    {
        health -= healthPoints;
        if (health <= 0)
        {
            health = 0;
            SetAlive(false);
            needsHealing = false;
        }
    }

    public void Heal(float healthPoints)
    {
        health += healthPoints;
        if (health > maxHealth)
        {
            health = maxHealth;
            needsHealing = false;
        }
    }
}
