using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterMode
{
    NORMAL_MODE,
    COMBAT_MODE,
    BUILDING_MODE,
    GATHER_MODE,
    ORDER_MODE
}

public class CharacterData
{
    private CharacterMode mode;

    public CharacterData()
    {
        mode = CharacterMode.NORMAL_MODE;
    }

    public void UpdateMode(CharacterMode m)
    {
        mode = m;
    }

    public CharacterMode GetMode()
    {
        return mode;
    }
}
