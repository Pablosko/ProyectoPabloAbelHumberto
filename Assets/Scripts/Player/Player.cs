using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int gold;
    public int level;
    public int exp;
    public int maxExp;
    public int rollPrice;
    public int levelUpPrice;
    public int posibleUnits;
    void Awake()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LevelUp()
    {
        if (HaveMoney(levelUpPrice))
        {
            gold -= levelUpPrice;
            Gamecontroller.instance.hud.UpdateText();
            exp += 4;
            
            if (exp >= maxExp)
            {
                level++;
                exp -= maxExp;
                maxExp = (int)(Mathf.Pow(level, 1.5f) * 1.5f + level * 4f);
            }
        }
        Gamecontroller.instance.hud.UpdateXpBar();
        Gamecontroller.instance.hud.UpdateText();
    }
    public bool HaveMoney(int compare)
    {
        if (gold >= compare)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
