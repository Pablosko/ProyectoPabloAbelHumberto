using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Rarity
{
    normal, commun, rare, epic, legendary
}

public class Heroe : NpcScript
{
    public string clas;
    public string esp;
    public string final;

    public int stars = 1;
    public Sprite icon;
    public Rarity tier;
    public AudioClip lvlUpClip;
    public string name;
    public float level;
    public float totalExp;
    public float currentXp;
    public float nextLevelXp;
    public SynergieScript mainSynergie;
    public List<SynergieScript> heroeSynergies;

    public void Awake()
    {
        base.Awake();
        lvlUpClip = Resources.Load<AudioClip>("Audio/Npc/" + Utils.GetFirstChars(GetType().ToString(), 5) + "/" + gameObject.name + "/" + gameObject.name + "_lvlUp");
        hud = transform.parent.Find("Canvas").GetComponent<HeroeHud>();
    }

    public void Update()
    {
        base.Update();
    }
    public void GetSynergies()
    {
        foreach (SynergieScript i in GetComponents<SynergieScript>())
        {
            heroeSynergies.Add(i);
        }
    }
    public void GetSprite()
    {
        icon = Resources.Load<Sprite>("icons/" + name);
    }
    public override void SelectTarget()
    {
        base.SelectTarget();
        Board board = Gamecontroller.instance.board;
        float distance = 1000;
        EnemyScript newTarget = null; ;
        foreach (EnemyScript enemy in board.enemys)
        {
            float newDistance = enemy.GetDistance(this);
            if (newDistance < distance)
            {
                distance = newDistance;
                newTarget = enemy;
            }
        }
        target = newTarget;
    }
    public void SetPosition()
    {
        currentTile = Gamecontroller.instance.board.GetEmptyTile();
        transform.SetParent(currentTile.transform);
    }
    public void PutIntoBoard()
    {
        SwitchTile(Gamecontroller.instance.board.GetEmptyTile());
    }
    public void RemoveFromBoard()
    {
        SwitchTile(Gamecontroller.instance.bench.GetEmptyTile());
    }
    public void LevelUp()
    {
        Utils.PlayAudio(lvlUpClip, audioSource, false);
    }
}
