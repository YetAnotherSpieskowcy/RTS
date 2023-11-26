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
    private CharacterData characterData;
    private Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        movementController = new MovementController(playersTransform);
        animationController = new AnimationController(playerAnimator, "Idle");
        buildMechanismMediator = GetComponent<BuildMechanismController>().GetBuildMechanismMediator(); //GameObject.Find("Player").
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

            if (mode == CharacterMode.BUILDING_MODE)
            {
                //Debug.Log("building");
                //characterData.UpdateMode(CharacterMode.BUILDING_MODE);
            }
            else
            {
                //Debug.Log("normal");
                characterData.UpdateMode(CharacterMode.NORMAL_MODE);
                hitting = movementController.GetHit();
            }

            animationController.ChooseAnimation(hitting, movementController.GetRunning(), movementController.GetVertical(), movementController.GetHorizontal());
        }
    }

    void LateUpdate()
    {
        if (unit.IsAlive())
        {
            movementController.UpdatePhysics(animationController.AnimationRunning());
        }
    }

    public void MarkAnimationEnded()
    {
        animationController.MarkAnimationEnded();
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
        Action action = buildMechanismMediator.GetAction();
        if (action != Action.AVAILABLE && action != Action.UNAVAILABLE && mode != CharacterMode.COMBAT_MODE && mode != CharacterMode.BUILDING_MODE)
        {
            //Debug.Log("building");
            characterData.UpdateMode(CharacterMode.BUILDING_MODE);
        }
        else if (buildMechanismMediator.GetAction() == Action.AVAILABLE && mode != CharacterMode.NORMAL_MODE)
        {
            //Debug.Log("normal");
            characterData.UpdateMode(CharacterMode.NORMAL_MODE);
        }
    }
}
