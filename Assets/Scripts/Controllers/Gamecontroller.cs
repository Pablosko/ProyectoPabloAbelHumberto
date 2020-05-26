﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Gamecontroller : MonoBehaviour
{
    public static Gamecontroller instance;
    [SerializeField ]
    public SoundScript audioController;
    public GameObject pauseMenu;
    public GameObject shopContainer;
    public int[] tierProbs = new int[5];
    public HeroPool pool;
    public Shop shop;
    public float hexSize;
    public Board board;
    public int enemysToSpawn;
    public Player player;
    public HudController hud;
    public StageController stageController;
    List<GameObject> enemys = new List<GameObject>();
    public Bench bench;
    public GameObject ItemTigger;
    public SynergieManager synergieManager;
    public AudioSource audioSource;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioController = SoundScript.instance;
        enemys.AddRange(Resources.LoadAll<GameObject>("Prefabs/enemys"));
        GenEnemies();
        
        SoundScript.instance.PlayMusic(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePauseMenu();
        }
        if (Time.timeScale != 0)
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
                        Utils.PlayAudio(SoundScript.instance.SellHeroEffectClip, audioSource, false);
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
    }
    public void GenEnemies()
    {
        for (int i = 0; i < enemysToSpawn; i++)
        {
            GameObject go = Instantiate(enemys[Random.Range(0,enemys.Count)]);
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
    public void ClosePauseMenu()
    {
        Time.timeScale = Utils.BoolToInt(pauseMenu.gameObject.activeSelf);
        pauseMenu.SetActive(!pauseMenu.gameObject.activeSelf);
        shopContainer.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
