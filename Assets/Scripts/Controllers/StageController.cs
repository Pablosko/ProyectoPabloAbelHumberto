using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State
{
    Start,
    Fight,
    End
}
public class StageController : MonoBehaviour
{
    public int currentStage;
    public float startTime;
    public float endDelayTime;
    public float currentTime;
    public State state;
    Board board;
    public delegate void StateFunction();

   void Start()
    {
        board = Gamecontroller.instance.board;
    }

    void Update()
    {
        if (state == State.Start)
        {
            SetTimer(startTime, Time.deltaTime, StartStage);
        }
        else if (state == State.End)
        {
            SetTimer(endDelayTime ,Time.deltaTime,EndStage);
        }
    }
    public void SetTimer(float timer,float time,StateFunction func)
    {
        if (currentTime < timer)
        {
            currentTime += time;
        }
        if (currentTime >= timer)
        {
            currentTime = 0;
            func();
        }
        Gamecontroller.instance.hud.UpdateStageTimer(currentTime, timer, 1);
    }
    public void StartStage()
    {
        state = State.Fight;
        int numOfHeroes = Gamecontroller.instance.board.GetNumberOfHeroes();
        for (int i = numOfHeroes; i <= Gamecontroller.instance.player.posibleUnits; i++)
        {
            Heroe heroe = Gamecontroller.instance.bench.GetFirstHero();
           if (heroe != null)
           heroe.PutIntoBoard();
        }
        board.SetBoardNpcs();

        foreach (NpcScript npc in board.npcs)
        {
            npc.SelectTarget();
        }
    }
    public void EndStage()
    {
        state = State.Start;
        currentStage++;
        Gamecontroller.instance.hud.UpdateStageText(currentStage);
        Gamecontroller.instance.board.DestroyAllEnemysInBoard();
        Gamecontroller.instance.GenEnemies();
        Gamecontroller.instance.board.returnHeroesToStartingPos();
    }
}
