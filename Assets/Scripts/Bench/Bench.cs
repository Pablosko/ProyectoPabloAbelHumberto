using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour
{
    public List<Tile> tiles = new List<Tile>();

    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Tile>().type = TileType.bench;
            tiles.Add(child.gameObject.GetComponent<Tile>());
        }
    }

    void Update()
    {
        
    }
    public Tile GetEmptyTile()
    {
        foreach (Tile tile in tiles)
        {
            if (tile.currentNPC == null)
            {
                return tile;
            }
        }
        return null;
    }
    public Heroe GetFirstHero()
    {
        foreach (Tile tile in tiles)
        {
            if (tile.currentNPC != null)
            {
                return (Heroe)tile.currentNPC;
            }
        }
        return null;
    }
}
