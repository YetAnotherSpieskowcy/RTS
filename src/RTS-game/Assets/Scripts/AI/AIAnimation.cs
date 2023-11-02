using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AIAnimation : MonoBehaviour
{
    private Animator anim;
    public float runningSpeed = 1.0f;
    private string currentAnim = "";
    public int attackVariants = 1;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Attack()
    {
        anim.ResetTrigger(currentAnim);
        anim.SetTrigger("Attack" + Random.Range(1, attackVariants));
    }

    public void Move(Vector3 direction)
    {
        anim.ResetTrigger(currentAnim);
        if (direction == Vector3.zero)
        {
            anim.SetTrigger("Idle");
            currentAnim = "Idle";
        }
        else
        {
            if (direction.magnitude > runningSpeed)
            {
                anim.SetTrigger("Run Forward");
                currentAnim = "Run Forward";
            }
            else
            {
                anim.SetTrigger("Walk Forward");
                currentAnim = "Walk Forward";
            }
        }
    }
}
