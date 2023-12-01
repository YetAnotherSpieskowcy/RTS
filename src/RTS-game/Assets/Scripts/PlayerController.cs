using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    public Transform playersTransform;

    private MovementController movementController;
    private AnimationController animationController;
    private BuildMechanismMediator buildMechanismMediator;
    private CombatController combatController;
    private CombatMediator combatMediator;
    private CharacterData characterData;
    private Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        movementController = new MovementController(playersTransform);
        animationController = new AnimationController(playerAnimator, "Idle");
        buildMechanismMediator = GetComponent<BuildMechanismController>().GetBuildMechanismMediator();
        combatController = new CombatController();
        combatMediator = GetComponent<UnitDispatcher>().combatMediator;
        characterData = new CharacterData();
        unit = GetComponent<Unit>();
    }

    void Update()
    {
        if (unit.IsAlive())
        {
            UpdateMode();
            CharacterMode mode = characterData.GetMode();
            movementController.UpdateValues();
            bool hitting = false;

            if (mode == CharacterMode.GATHER_MODE)
            {
                animationController.Gather();
                return;
            }
            else if (mode == CharacterMode.COMBAT_MODE)
            {
                animationController.Attack();
                return;
            }
            else if (mode == CharacterMode.GATHER_MODE)
            {
                animationController.Gather();
                return;
            }

            animationController.ChooseAnimation(movementController.GetRunning(), movementController.GetVertical(), movementController.GetHorizontal());
        }
    }

    void LateUpdate()
    {
        if (unit.IsAlive())
        {
            movementController.UpdatePhysics(characterData.GetMode() == CharacterMode.COMBAT_MODE, combatMediator.GetState() != CombatModeState.INACTIVE);
        }
    }

    public void Die()
    {
        animationController.Die();
    }

    public void Hit()
    {
        animationController.Hit();
    }

    private void UpdateMode()
    {
        CharacterMode mode = characterData.GetMode();
        if (mode == CharacterMode.COMBAT_MODE || mode == CharacterMode.GATHER_MODE)
        {
            return;
        }
        else if (movementController.GetHit() && mode == CharacterMode.NORMAL_MODE)
        {
            SetMode(CharacterMode.COMBAT_MODE);

        }
        else if (combatMediator.GetState() == CombatModeState.ACTIVE && mode != CharacterMode.ORDER_MODE)
        {
            buildMechanismMediator.SetAction(Action.UNAVAILABLE);
            SetMode(CharacterMode.ORDER_MODE);
        }
        else if (combatMediator.GetState() == CombatModeState.ENDING)
        {
            buildMechanismMediator.SetAction(Action.AVAILABLE);
            combatMediator.SetState(CombatModeState.ENDED);
        }
        else if (combatMediator.GetState() == CombatModeState.INACTIVE)
        {
            Action action = buildMechanismMediator.GetAction();
            if (action != Action.AVAILABLE && action != Action.UNAVAILABLE && mode != CharacterMode.BUILDING_MODE)
            {
                SetMode(CharacterMode.BUILDING_MODE);
            }
            else if (!animationController.CheckIfAttackRunning() && buildMechanismMediator.GetAction() == Action.AVAILABLE && mode != CharacterMode.NORMAL_MODE)
            {
                SetMode(CharacterMode.NORMAL_MODE);
            }
        }
        else if (combatMediator.GetState() == CombatModeState.ENDED)
        {
            combatMediator.SetState(CombatModeState.INACTIVE);
        }
    }

    public void SetMode(CharacterMode mode)
    {
        characterData.UpdateMode(mode);
    }
}
