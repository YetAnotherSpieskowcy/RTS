using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitDispatcher : MonoBehaviour
{
    private  CommandController commandController;
    bool selectionEnabled = false;
    FormationDispatcher fdispatcher;
    List<Unit> selectedUnits = new();
    List<Unit> friendlyUnitCache = new();
    void Awake()
    {
        fdispatcher = GetComponentInChildren<FormationDispatcher>();
    }
    void CollectFriendlyUnits()
    {

        foreach (Unit unit in GameObject.FindObjectsOfType(typeof(Unit)))
        {
            // TODO Limit range of player commands
            if (unit.IsFriendly && unit.IsAlive())
            {
                friendlyUnitCache.Add(unit);
            }
        }
    }
    void Update()
    {
        if (fdispatcher.IsLocked())
        {
            return;
        }
        if (Input.GetKeyDown(InputSettings.UnitSelectionMenu) && !selectionEnabled && selectedUnits.Count == 0)
        {
            CollectFriendlyUnits();
            selectionEnabled = true;
            Debug.Log("1. Everyone\n2. Melee\n3. Ranged\n0. Cancel");
            commandController.ActivateGroupChoice();
        }
        if (selectionEnabled)
        {
            if (Input.GetKeyDown(InputSettings.UnitSelectionMenuItem1))
            {
                Debug.Log("All");
                selectionEnabled = false;
                selectedUnits = friendlyUnitCache;
            }
            if (Input.GetKeyDown(InputSettings.UnitSelectionMenuItem2))
            {
                Debug.Log("Melee");
                selectionEnabled = false;
                selectedUnits = friendlyUnitCache.Where(it => it.group == Unit.Group.Melee).ToList();
            }
            if (Input.GetKeyDown(InputSettings.UnitSelectionMenuItem3))
            {
                Debug.Log("Ranged");
                selectedUnits = friendlyUnitCache.Where(it => it.group == Unit.Group.Ranged).ToList();
                selectionEnabled = false;
            }
            if (!selectionEnabled)
            {
                commandController.ActivateCommandChoice();
                Debug.Log("1. Follow\n2. Halt\n3. Attack\n4. Go here\n5. Retreat\n0. Cancel");
            }
            if (Input.GetKeyDown(InputSettings.UnitSelectionMenuCancel))
            {
                Debug.Log("Cancel");
                selectionEnabled = false;
                commandController.SetAllUnvisible();
            }
        }
        else if (selectedUnits.Count > 0)
        {
            if (Input.GetKeyDown(InputSettings.UnitSelectionMenuItem1))
            {
                Debug.Log("Follow");
                selectedUnits.ForEach(it =>
                {
                    EnemyAI ai = it.GetComponent<EnemyAI>();
                    if (ai != null)
                    {
                        ai.Target(transform);
                        ai.StoppingDistance = 3.0f + Random.Range(0.0f, 2.0f);
                    }
                });
                selectedUnits.Clear();
                commandController.SetAllUnvisible();
            }

            if (Input.GetKeyDown(InputSettings.UnitSelectionMenuItem2))
            {
                Debug.Log("Halt");
                selectedUnits.ForEach(it =>
                {
                    EnemyAI ai = it.GetComponent<EnemyAI>();
                    if (ai != null)
                    {
                        ai.Target(null);
                    }
                });
                selectedUnits.Clear();
                commandController.SetAllUnvisible();
            }
            if (Input.GetKeyDown(InputSettings.UnitSelectionMenuItem3))
            {
                Debug.Log("Attack");
                BattleContext.Context.ReInit();
                List<Unit> possibleTargets = BattleContext.Context.GetTargetsOfAligment(Unit.Team.Enemy, it => true).ToList();
                List<Unit> carry = new();
                foreach (Unit unit in selectedUnits)
                {
                    EnemyAI ai = unit.GetComponent<EnemyAI>();
                    if (ai != null)
                    {
                        if (ai.target == transform || ai.target == null)
                        {
                            Unit target = (from n in possibleTargets
                                           orderby Vector3.Distance(unit.transform.position, n.transform.position)
                                           select n).First();
                            possibleTargets.Remove(target);
                            carry.Add(target);
                            ai.Target(target.transform);
                            if (possibleTargets.Count == 0)
                            {
                                possibleTargets.AddRange(carry);
                                carry.Clear();
                            }
                        }
                    }
                }
                selectedUnits.Clear();
                commandController.SetAllUnvisible();
            }
            if (Input.GetKeyDown(InputSettings.UnitSelectionMenuItem4))
            {
                Debug.Log("Go here");
                fdispatcher.StartDispatch(new List<Unit>(selectedUnits));
                selectedUnits.Clear();
                commandController.SetAllUnvisible();
            }
        }
        if (Input.GetKeyDown(InputSettings.UnitSelectionMenuItem5))
        {
            Debug.Log("Retreat");
            Debug.Log("Not implemented");
            selectedUnits.Clear();
            commandController.SetAllUnvisible();
        }
        if (Input.GetKeyDown(InputSettings.UnitSelectionMenuCancel))
        {
            Debug.Log("Cancel");
            selectedUnits.Clear();
            commandController.SetAllUnvisible();
        }


    }
    void Start()
    {
        this.commandController = GameObject.Find("UI").GetComponent<CommandController>();
    }
}
