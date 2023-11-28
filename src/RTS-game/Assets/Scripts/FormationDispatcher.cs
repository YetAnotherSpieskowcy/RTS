using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FormationDispatcher : MonoBehaviour
{
    [SerializeField] Transform marker;
    List<Unit> selectedUnits = null;
    private CombatMediator combatMediator;
    public void StartDispatch(List<Unit> selectedUnits, CombatMediator mediator)
    {
        this.selectedUnits = selectedUnits;
        this.combatMediator = mediator;
    }
    void Update()
    {
        if (selectedUnits != null)
        {
            RaycastHit hit;
            LayerMask mask;
            mask = LayerMask.GetMask("Player") ^ int.MaxValue;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, float.PositiveInfinity, mask))
            {
                if (hit.transform.tag == "Terrain")
                {
                    marker.position = hit.point;

                    if (Input.GetMouseButtonDown(0))
                    {
                        var pos = hit.point;
                        var formation = Formations.Radial(3, .5f);
                        var v = formation.Take(selectedUnits.Count).ToList();
                        foreach (var unit in selectedUnits)
                        {
                            EnemyAI ai = unit.GetComponent<EnemyAI>();
                            if (ai != null)
                            {
                                ai.Target(pos + v.First());
                                v.RemoveRange(0, 1);
                            }
                        }
                        selectedUnits = null;
                        combatMediator.SetState(CombatModeState.ENDING);
                    }
                }
            }
        }
    }
    public bool IsLocked()
    {
        return selectedUnits != null;
    }
}
