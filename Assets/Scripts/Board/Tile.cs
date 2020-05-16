using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TileType
{
    board,
    bench
}
public enum TileZoneType
{
    Ally,
    Enemy
}
public class Tile : MonoBehaviour
{
    public TileType type;
    public TileZoneType zoneType;
    public NpcScript currentNPC;
    public NpcScript nextNpc;
    public GameObject borde;
    public int x;
    public int y;
    public List<Tile> adyacentHex = new List<Tile>();
    void Start()
    {
        borde = transform.Find("Borde").gameObject;
    }
    public void ActiveBorde(bool state)
    {
        borde.SetActive(state);
    }
    public void AnimateBorde(bool state)
    {
        if(state == false)
        borde.GetComponent<Animator>().Rebind();
        borde.GetComponent<Animator>().enabled = state;
    }
}

