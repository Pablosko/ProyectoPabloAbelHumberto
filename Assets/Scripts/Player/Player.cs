using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Heroe> heroes = new List<Heroe>();
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
    void Update()
    {
    }
    public void LevelUp()
    {
        if (HaveMoney(levelUpPrice))
        {
            AddGold(-levelUpPrice);
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
    public void checkForHeroeUpgrade()
    {
        List<string> names = new List<string>();
        foreach (Heroe heroe in heroes)
        {
            if (!names.Contains(heroe.name))
            {
                List<Heroe> sameName = Utils.GetSameNameHeroes(heroes,heroe.name);
                names.Add(heroe.name);
                if (sameName.Count >= 3)
                {
                    List<List<Heroe>> sortedHeroes = Utils.SortByStarHeros(sameName);
                    //mirar tier 1-2-3
                    foreach (List<Heroe> list in sortedHeroes)
                    {
                        if (list.Count >= 3)
                        {
                            if (list[0].stars != 3)
                            {
                                List<Heroe> top3 = Utils.Get3HighestLvlHeroes(list);
                                Utils.MixHeroes(top3);
                                checkForHeroeUpgrade();
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
    public void AddGold(int extra)
    {
        gold += extra;
        Gamecontroller.instance.hud.UpdateText();
    }
}
