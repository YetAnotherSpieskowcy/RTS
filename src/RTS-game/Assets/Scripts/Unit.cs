using UnityEngine;


public class Unit : MonoBehaviour
{
    // TODO Remake into more generic system
    public enum Group
    {
        Melee = 1,
        Ranged = 2
    }
    public enum Team
    {
        Friendly,
        Enemy,
        Neutral
    }
    public Group group;
    public Team team;
    public bool IsFriendly
    {
        get
        {
            return team == Team.Friendly;
        }
    }
    public void SetFriendly()
    {
        this.team = Team.Friendly;
    }
    private bool isDead = false;
    public bool IsAlive()
    {
        return !isDead;
    }
    [SerializeField] private int health = 100;
    public int Health { get => health; }

    public void Hit(int damage)
    {
        health -= damage;
        Debug.Log("Bonk!");
        if (health <= 0)
        {
            EnemyAI ai = GetComponent<EnemyAI>();
            if (ai != null)
            {
                isDead = true;
                ai.Die();
            }
        }
    }
    public void Notify(Transform transform)
    {
        EnemyAI ai = GetComponent<EnemyAI>();
        if (ai != null && !ai.HasTarget())
        {
            ai.Target(transform);
        }

    }
}
