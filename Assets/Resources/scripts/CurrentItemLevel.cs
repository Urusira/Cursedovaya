using System;
using UnityEngine;

public class CurrentItemLevel : MonoBehaviour
{
    public int level = 1;
    public float levelCharsMultiplier = 1.2f;

    public void LevelUp()
    {
        if (level < 10)
        {
            level++;
            levelCharsMultiplier += level * 0.06f;
        }
    }
}