using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPool : MonoBehaviour
{
    public int[] quantity = new int[5];
    public List<List<GameObject>> heroes = new List<List<GameObject>>();
    void Start()
    {
        GeneratePool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GeneratePool()
    {
        for (int i = 0; i < quantity.Length; i++)
        {
            heroes.Add(GetTierHeroList(i+1, quantity[i]));
        }
    }
    public List<GameObject> GetTierHeroList(int tier,int size)
    {
        List<GameObject> tierList = new List<GameObject>();
        string path = "Heros/Tier" + tier;
        GameObject[] singelHeros = Resources.LoadAll<GameObject>(path);
        foreach (GameObject hero in singelHeros)
        {
            for (int i = 0; i < size; i++)
            {
                tierList.Add(hero);
                hero.transform.GetChild(0).GetComponent<Heroe>().GetSprite();
                //print(hero.GetComponent<Heroe>().name);
            }
        }
        return tierList;
    }
    public GameObject GetRandomHero(int tier)
    {
        int index = Random.Range(0, heroes[tier].Count);
        return heroes[tier][index];
    }
}
