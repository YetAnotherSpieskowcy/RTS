using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int strength = 1;
    public float strengthExpirience = 0.0f;
    public int leadership = 1;
    public float leadershipExpirience = 0.0f;
    public int charisma = 1;
    public float charismaExpirience = 0.0f;

    public Modal modal;

    public void Update()
    {
        if (strengthExpirience >= strength)
        {
            strength++;
            strengthExpirience = 0;
            modal.Show("Your strength has improved");
        }
        if (leadershipExpirience >= leadership)
        {
            leadership++;
            leadershipExpirience = 0;
            modal.Show("Your leadership has improved");
        }
        if (charismaExpirience >= charisma)
        {
            charisma++;
            charismaExpirience = 0;
            modal.Show("Your charisma has improved");
        }
    }
}
