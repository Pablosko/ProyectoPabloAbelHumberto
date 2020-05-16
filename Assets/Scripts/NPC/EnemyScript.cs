using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : NpcScript
{
    public List<GameObject> drops = new List<GameObject>();
    public void Awake()
    {
        base.Awake();
    }
    public void Start()
    {
        GenRandomTile();
        SetDrops();
    }
    public void Update()
    {
        base.Update();
    }
    public void GenRandomTile()
    {
        currentTile = Gamecontroller.instance.board.GetRandomEnemyEmptyTile();
            currentTile.currentNPC = this;
            currentTile.currentNPC.transform.parent = currentTile.transform;
            currentTile.currentNPC.transform.localPosition = Vector3.zero;

        Gamecontroller.instance.board.enemys.Add(this);
    }
    public override void SelectTarget()
    {
        base.SelectTarget();
        Board board = Gamecontroller.instance.board;
        float distance = 1000;
        Heroe newTarget = null; ;   
        foreach (Heroe heroe in board.heroes)
        {
            float newDistance = heroe.GetDistance(this);
            if (newDistance <= distance)
            {
                distance = newDistance;
                newTarget = heroe;
            }
        }
        target = newTarget;
    }
    public void Drop()
    {
        int index = Random.Range(0, drops.Count);
        GameObject go = Instantiate(drops[index]);
        go.transform.position = transform.position;
    }
    public void SetDrops()
    {
        drops.Add(Resources.Load<GameObject>("Drops/Test"));
    }
}
