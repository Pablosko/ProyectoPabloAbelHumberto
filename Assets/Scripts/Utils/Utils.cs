using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ExtraStat
{
    public float value;
}
[System.Serializable]
public class Stat
{
    [System.NonSerialized]
    public float value;

    public float basevalue;
    [System.NonSerialized]
    public List<ExtraStat> extraValues = new List<ExtraStat>();

    public float GetExtraValues()
    {
        float total = 0;
        foreach (ExtraStat stat in extraValues)
        {
            total += stat.value;
        }
        return total;
    }
}
public class Utils: MonoBehaviour
{
    public static List<Heroe> GetSameNameHeroes(List<Heroe> heroes,string name)
    {
        List<Heroe> sameNameHeroes = new List<Heroe>();
        foreach (Heroe heroe in heroes)
        {
            if (heroe.name == name)
            {
                sameNameHeroes.Add(heroe);
            }
        }
        return sameNameHeroes;
    }
    public static List<List<Heroe>> SortByStarHeros(List<Heroe> heroes)
    {
        List<List<Heroe>> sortedHeroes = new List<List<Heroe>>();

        for (int i = 1; i < 4; i++)
        {
            List<Heroe> sameStar = new List<Heroe>(); 
            foreach (Heroe heroe in heroes)
            {
                if (heroe.stars == i)
                {
                    sameStar.Add(heroe);
                }
            }
            sortedHeroes.Add(sameStar);
        }
        return sortedHeroes;
    }
    public static List<Heroe> Get3HighestLvlHeroes(List<Heroe> heroes)
    {
        Heroe nextHero = heroes[0];
        List<Heroe> topHeroes = new List<Heroe>();
        for (int i = 0; i < 3; i++)
        {
            foreach (Heroe heroe in heroes)
            {
                if (heroe.totalExp >= nextHero.totalExp)
                {
                    nextHero = heroe;
                }
            }
            topHeroes.Add(nextHero);
            heroes.Remove(nextHero);
        }
        return topHeroes;
    }
    public static void MixHeroes(List<Heroe> heroes)
    {
        heroes[2].stars++;
        heroes[2].currentXp += heroes[1].totalExp + heroes[2].currentXp;
        heroes[2].transform.localScale = Vector3.one * 0.75f * heroes[2].stars;
        Gamecontroller.instance.player.heroes.Remove(heroes[1]);
        Gamecontroller.instance.player.heroes.Remove(heroes[0]);
        Gamecontroller.instance.board.RemoveFromBoard(heroes[0]);
        Gamecontroller.instance.board.RemoveFromBoard(heroes[1]);

        heroes[2].hud.GetComponent<HeroeHud>().UpgradeMenu();

        Destroy(heroes[1].transform.parent.gameObject);
        Destroy(heroes[0].transform.parent.gameObject);
    }
    public static int BoolToInt(bool bol)
    {
        if (bol)
            return 1;
        else
            return 0;
    }
}
