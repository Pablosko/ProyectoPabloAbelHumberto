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
    public Sprite icon;
    public Rarity tier;
    public string name;
    public List<SynergieScript> heroeSynergies;

    public void Awake()
    {
        base.Awake();
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
}
