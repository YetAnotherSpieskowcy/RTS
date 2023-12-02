using UnityEngine;

public class AIAnimation : MonoBehaviour
{
    private Animator anim;
    public float runningSpeed = 1.0f;
    private string currentAnim = "";
    public int attackVariants = 1;


    void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    public void Attack()
    {
        if (currentAnim == "Death") return;
        anim.ResetTrigger(currentAnim);
        anim.SetTrigger("Attack" + Random.Range(1, attackVariants));
    }

    public void Shoot()
    {
        if (currentAnim == "Death") return;
        anim.ResetTrigger(currentAnim);
        anim.SetTrigger("Shoot");
    }

    public void Die()
    {
        anim.ResetTrigger(currentAnim);
        anim.SetTrigger("Death");
        currentAnim = "Death";
    }

    public void Work()
    {
        anim.ResetTrigger(currentAnim);
        anim.SetTrigger("Mine");
        currentAnim = "Mine";
    }

    public void StopWork()
    {
        if (currentAnim != "Mine") return;
        anim.ResetTrigger(currentAnim);
        anim.SetTrigger("StopMine");
        currentAnim = "Idle";
    }

    public void Move(Vector3 direction)
    {
        if (currentAnim == "Mine") return;
        if (currentAnim == "Death") return;
        TryGetComponent<MeleeAI>(out MeleeAI meleeAI);
        TryGetComponent<RangeAI>(out RangeAI rangeAI);
        bool attackRunning = meleeAI != null ? meleeAI.GetAttackAnimRunning() : false;
        bool shootRunning = rangeAI != null ? rangeAI.GetShootAnimRunning() : false;
        if (!attackRunning && !shootRunning)
        {
            if (direction == Vector3.zero)
            {
                if (currentAnim != "Idle")
                {
                    anim.ResetTrigger(currentAnim);
                    anim.SetTrigger("Idle");
                    currentAnim = "Idle";
                }
            }
            else
            {
                if (direction.magnitude > runningSpeed)
                {
                    if (currentAnim != "Run Forward")
                    {
                        anim.SetTrigger("Run Forward");
                        currentAnim = "Run Forward";
                    }
                }
                else
                {
                    if (currentAnim != "Walk Forward")
                    {
                        anim.SetTrigger("Walk Forward");
                        currentAnim = "Walk Forward";
                    }
                }
            }
        }
    }
}
