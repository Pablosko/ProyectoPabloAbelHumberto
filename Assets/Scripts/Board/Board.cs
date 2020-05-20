using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Board : MonoBehaviour
{
    public Vector3 hexagonPosition;
    public List<Heroe> heroes = new List<Heroe>();
    public List<EnemyScript> enemys = new List<EnemyScript>();
    public List<NpcScript> npcs = new List<NpcScript>();
    public List<Tile> tiles = new List<Tile>();

    [SerializeField]
    GameObject hexagonContent;
    [SerializeField]
    GameObject hexagonPf;
    [SerializeField]
    GameObject linkPf;
    [Space(3)]

    [Header("Size Of Board")]
    [SerializeField]
    public int numOfBoxesX;
    [SerializeField]
    public int numOfBoxesZ;
    [Space(3)]

    [Header("Start Position")]
    [SerializeField]
    Vector3 startPosition;
    [SerializeField]
    float spaceBetweenBoxes;

    List<int> offsets = new List<int>();

    // Start is called before the first frame update
    void Awake()
    {
        offsets.Add(1 + numOfBoxesZ);
        offsets.Add(numOfBoxesZ);
        offsets.Add(-1 + numOfBoxesZ);
        offsets.Add(1);
        offsets.Add(-1);
        offsets.Add(1 - numOfBoxesZ);
        offsets.Add(-numOfBoxesZ);
        offsets.Add(-1 - numOfBoxesZ);
        spawnHexagon();
        Generatelinks();
        SetTileType();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void spawnHexagon()
    {
        GameObject tempHexagon;
        
        for (int i = 0; i < numOfBoxesX; i++)
        {
            Vector3 tempPos = startPosition;

            tempPos += new Vector3((spaceBetweenBoxes * 0.85f) * (i + 1), 0, 0);
            if ((i + 1) % 2 != 0)
            {
                tempPos -= new Vector3(0, 0, spaceBetweenBoxes / 2);
            }
            for (int j = 0; j < numOfBoxesZ; j++)
            {
                tempHexagon = Instantiate(hexagonPf, tempPos, hexagonPf.transform.rotation);
                hexagonPosition = tempHexagon.transform.position;
                Tile tile = tempHexagon.GetComponent<Tile>();
                tile.x = j;
                tile.y = i;
                tempHexagon.transform.parent = hexagonContent.transform;
                tiles.Add(tile);
                tile.type = TileType.board;
                tempPos -= new Vector3(0, 0, spaceBetweenBoxes);
            }
        }
    }
    void Generatelinks()
    {
        for (int i = 0; i < numOfBoxesX; i++)
        {
            for (int j = 0; j < numOfBoxesZ; j++)
            {
                foreach (int index in offsets)
                {
                    if (containsHexagon(index + (i * numOfBoxesX) + j) && containsHexagon((i * numOfBoxesX) + j))
                    {
                        if (Vector3.Distance(tiles[(i * numOfBoxesX) + j].transform.position, tiles[(i * numOfBoxesX) + j + index].transform.position) <= 2f)
                        {
                            tiles[(i * numOfBoxesX) + j].adyacentHex.Add(tiles[(i * numOfBoxesX) + j + index]);
                        }
                    }
                }
            }
        }
    }
    bool containsHexagon(int i)
    {
        return (i >= 0 && i < tiles.Count);
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
    public void ActiveCasillasBorde(bool state)
    {
        foreach (Tile tile in Gamecontroller.instance.board.tiles)
        {
            tile.ActiveBorde(state);
        }
    }
    public void SetBoardNpcs()
    {
        npcs = new List<NpcScript>();
        heroes = new List<Heroe>();
        enemys = new List<EnemyScript>();

        foreach (Tile tile in tiles)
        {
            if (tile.currentNPC != null)
            {
                if (tile.currentNPC)
                {
                    npcs.Add(tile.currentNPC);
                }
                if (tile.currentNPC.GetComponent<EnemyScript>() != null)
                {
                    enemys.Add(tile.currentNPC.GetComponent<EnemyScript>());
                }
                else if (tile.currentNPC.GetComponent<Heroe>() != null)
                {
                    tile.currentNPC.GetComponent<Heroe>().startingTile = tile.currentNPC.GetComponent<Heroe>().currentTile;
                    heroes.Add(tile.currentNPC.GetComponent<Heroe>());
                }
            }
        }
    }
    public void RemoveFromBoard(NpcScript npc)
    {
        if (npcs.Contains(npc))
        {
            npcs.Remove(npc);
        }
        if (enemys.Contains(npc.GetComponent<EnemyScript>()))
        {
            enemys.Remove(npc.GetComponent<EnemyScript>());
        }
        else if (heroes.Contains(npc.GetComponent<Heroe>()))
        {
            heroes.Remove(npc.GetComponent<Heroe>());
            Gamecontroller.instance.synergieManager.RemoveHeroSynergies(npc.GetComponent<Heroe>());
        }
    }
    public void DestroyAllEnemysInBoard()
    {
        foreach (EnemyScript enemy in enemys)
        {
            Destroy(enemy);
        }
    }
    public void CheckEndStage()
    {
        if (enemys.Count == 0)
        {
            SetHeroesToIdle();
            Gamecontroller.instance.stageController.state = State.End;
        }
        else if (heroes.Count == 0)
        {
            SetHeroesToIdle();
            //repetir ronda
        }
    }
    public void returnHeroesToStartingPos()
    {
        foreach (Heroe heroe in heroes)
        {
            heroe.hp = heroe.maxHp;
            heroe.currentTile.currentNPC = null;
            heroe.currentTile = heroe.startingTile;
            heroe.currentTile.currentNPC = heroe;
            heroe.SwitchTile(heroe.currentTile);
        }
    }
    public void returnHeroeToBanquillo(Heroe hero)
    {
        hero.currentTile.currentNPC = null;
        hero.currentTile = Gamecontroller.instance.bench.GetEmptyTile();
        hero.currentTile.currentNPC = hero;
        hero.transform.SetParent(hero.currentTile.transform);
        hero.transform.localPosition = Vector3.zero;
    }
    public void SetTileType()
    {
        foreach(Tile tile in tiles)
        {
            if (Physics.CheckSphere(tile.transform.position, tile.GetComponent<SphereCollider>().radius, 9 << 11))
            {
                tile.zoneType = TileZoneType.Ally;
            }
            else if (Physics.CheckSphere(tile.transform.position, tile.GetComponent<SphereCollider>().radius, 9 << 12))
            {
                tile.zoneType = TileZoneType.Enemy;
                tile.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.black);
            }
        }
    }
    public Tile GetRandomEnemyEmptyTile()
    {
        List<Tile> posibleTiles = new List<Tile>();
        foreach (Tile tile in tiles)
        {
            if ((tile.zoneType == TileZoneType.Enemy) &&
                (tile.currentNPC == null) &&
                (tile.nextNpc == null))
            {
                posibleTiles.Add(tile);
            }
        }
        return posibleTiles[Random.Range(0, posibleTiles.Count)];
    }
    public void AddHeroToBoard(Heroe heroe)
    {
        heroes.Add(heroe);
        Gamecontroller.instance.synergieManager.AddHeroSynergies(heroe);
    }
    public int GetNumberOfHeroes()
    {
        return heroes.Count;
    }
    public void checkTargets()
    {
        foreach (NpcScript npc in npcs)
        {
            if (npc.target != null)
            {
                if (npc.target.hp <= 0)
                {
                    npc.SelectTarget();
                }

            } else
                npc.SelectTarget();
        }
    }
    public void SetHeroesToIdle()
    {
        foreach (Heroe heroe in heroes)
        {
            heroe.anim.SetTrigger("idle");
            heroe.anim.SetBool("run",false);
        }
    }

}
