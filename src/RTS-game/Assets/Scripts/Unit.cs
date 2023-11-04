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
    [SerializeField] private int health = 100;
    public void Hit(int damage)
    {
        health -= damage;
        Debug.Log("Bonk!");
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
