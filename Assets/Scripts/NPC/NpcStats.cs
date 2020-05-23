using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStats : MonoBehaviour
{
    List<Stat> StatsList = new List<Stat>();
    public Stat physicalDamage;
    public Stat hp;
    public Stat speed;
    public Stat magicDamge;
    public Stat maxHp;
    public Stat mana;
    public Stat criticalDamage;
    public Stat criticalRate;
    public Stat physicalArmor;
    public Stat magicalArmor;
    public Stat dodge;
    public Stat range;
    public Stat attackSpeed;

    void Start()
    {
        StatsList.Add(physicalDamage);
        StatsList.Add(hp);
        StatsList.Add(speed);
        StatsList.Add(magicDamge);
        StatsList.Add(maxHp);
        StatsList.Add(mana);
        StatsList.Add(criticalDamage);
        StatsList.Add(criticalRate);
        StatsList.Add(physicalArmor);
        StatsList.Add(magicalArmor);
        StatsList.Add(dodge);
        StatsList.Add(range);
        StatsList.Add(attackSpeed);
        UpdateStats();

        hp.value = maxHp.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateStats()
    {
        foreach (Stat stat in StatsList)
        {
            stat.value = stat.basevalue + stat.GetExtraValues();
        }
    }
}
