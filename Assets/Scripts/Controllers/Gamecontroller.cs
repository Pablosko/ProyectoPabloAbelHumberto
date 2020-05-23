using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamecontroller : MonoBehaviour
{
    public static Gamecontroller instance;
    public int[] tierProbs = new int[5];
    public HeroPool pool;
    public Shop shop;
    public float hexSize;
    public Board board;
    public int enemysToSpawn;
    public Player player;
    public HudController hud;
    public StageController stageController;
    GameObject enemy;
    public Bench bench;
    public GameObject ItemTigger;
    public SynergieManager synergieManager;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        enemy = Resources.Load<GameObject>("Prefabs/enemys/Test");
        GenEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("roll");
            shop.Roll();
        }
        if (Input.GetKeyDown(KeyCode.D) && (stageController.state == State.Start))
        {
            print("mete");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray, 100);
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.gameObject.layer == 10)
                {
                    Heroe heroe = hit.transform.parent.GetComponent<Heroe>();
                    if (heroe.currentTile.type == TileType.bench)
                    {
                        heroe.PutIntoBoard();
                    }
                    else
                    {
                        heroe.RemoveFromBoard();
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray, 100);
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.parent.GetComponent<Heroe>() != null)
                {
                    Destroy(hit.transform.parent.parent.gameObject);
                    board.RemoveFromBoard(hit.transform.parent.GetComponent<Heroe>());
                    player.AddGold(hit.transform.parent.GetComponent<Heroe>().stars * (int)hit.transform.parent.GetComponent<Heroe>().tier);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            bool activado = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray, 100);
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.parent.GetComponent<Heroe>() != null)
                {
                    activado = true;
                    hit.transform.parent.GetComponent<Heroe>().hud.ActivateStatsHud(!hit.transform.parent.GetComponent<Heroe>().hud.GetComponent<HeroeHud>().heroeInfo.activeSelf);
                }
            }
            if (!activado)
            {
                foreach (Heroe heroe in player.heroes)
                {
                    heroe.hud.ActivateStatsHud(false);
                }
            }
        }
    }
    public void GenEnemies()
    {
        for (int i = 0; i < enemysToSpawn; i++)
        {
            GameObject go = Instantiate(enemy);
            board.enemys.Add(go.transform.GetChild(0).GetComponent<EnemyScript>());
        }
    }
    public void AdminComands()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKey(KeyCode.G))
            {
                player.AddGold(99999);
            }
            if (Input.GetKey(KeyCode.I))
            {
                for (int i = 0; i < board.heroes.Count; i++)
                {
                    board.heroes[i].isInvecible = !board.heroes[i].isInvecible;
                }
            }
            if (Input.GetKey(KeyCode.F))
            {
                for (int i = 0; i < board.enemys.Count; i++)
                {
                    board.enemys[i].GetDmg(99999);
                }
            }
        }
    }
}
