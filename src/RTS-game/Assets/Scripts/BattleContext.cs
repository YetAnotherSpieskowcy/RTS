using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleContext
{
    List<Unit> units = new();
    private static BattleContext _context;
    public static BattleContext Context
    {
        get
        {
            if (_context == null)
            {
                _context = new();
            }
            return _context;
        }
    }
    public void ReInit()
    {
        foreach (Unit unit in GameObject.FindObjectsOfType(typeof(Unit)))
        {
            units.Add(unit);
        }

    }
    public Unit[] GetTargetsOfAligment(Unit.Team team, Predicate<Unit> predicate)
    {
        return units.Where(it => it.team == team).Where(it => predicate(it)).ToArray();
    }
}
