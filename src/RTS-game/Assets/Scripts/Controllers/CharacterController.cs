using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator playerAnimator;
    public Transform playersTransform;

    private float health, maxHealth, healingStep;
    private bool fighting, alive, needsHealing;

    private MovementController movementController;
    private AnimationController animationController;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100f;
        health = 100f;
        healingStep = 5f;
        fighting = false;
        alive = true;
        needsHealing = false;
        movementController = new MovementController(playersTransform);
        animationController = new AnimationController(playerAnimator, "Idle");
    }

    void Update()
    {
        if (alive)
        {
            if (needsHealing && !fighting)
            {
                Heal(healingStep);
            }

            movementController.UpdateValues();
            bool tmp = movementController.GetHit();
            if (tmp) Debug.Log("hitting");
            animationController.ChooseAnimation(tmp, movementController.GetRunning(), movementController.GetVertical(), movementController.GetHorizontal());
        }
    }

    void LateUpdate()
    {
        movementController.UpdatePhysics();
    }

    public void UpdateFighting(bool fighting)
    {
        this.fighting = fighting;
    }

    public bool CheckIfFighting()
    {
        return this.fighting;
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
        if(health > maxHealth)
        {
            health = maxHealth;
            needsHealing = false;
        }
    }

}
