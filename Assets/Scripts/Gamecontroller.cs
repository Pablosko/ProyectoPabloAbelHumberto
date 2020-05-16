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
    }
    public void GenEnemies()
    {
        for (int i = 0; i < enemysToSpawn; i++)
        {
            board.enemys.Add(Instantiate(enemy).GetComponent<EnemyScript>());
        }
    }
   
}
