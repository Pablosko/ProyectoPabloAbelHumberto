using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum GameState
{
    Gameplaying,
    Menu
}
public class Gamecontroller : MonoBehaviour
{
    public static Gamecontroller instance;
    [SerializeField]
    GameState gamestate;
    public GameObject gameGo;
    public GameObject menuGo;
    public GameObject menuGoNoCanvas;
    public GameObject canvas;
    public MainMenu menuScript;
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
    public SoundScript audio;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        enemys.AddRange(Resources.LoadAll<GameObject>("Prefabs/enemys"));
        audioSource = GetComponent<AudioSource>();
        audio.PlayMusic(true);
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
                        player.AddGold(2);
                        player.DestroyHero(hit.transform.parent.GetComponent<Heroe>());
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
        Time.timeScale = Utils.BoolToInt(menuGoNoCanvas.gameObject.activeSelf);
        menuGoNoCanvas.SetActive(!menuGoNoCanvas.gameObject.activeSelf);
        shopContainer.gameObject.SetActive(!menuGoNoCanvas.gameObject.activeSelf);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void MainButton()
    {

        if (gamestate == GameState.Gameplaying)
        {
            Gamecontroller.instance.ClosePauseMenu();
        }
        else if (gamestate == GameState.Menu)
        {
            gamestate = GameState.Gameplaying;
            menuGo.SetActive(false);
            gameGo.SetActive(true);
            menuGoNoCanvas.transform.SetParent(canvas.transform);
            menuGoNoCanvas.GetComponent<RectTransform>().localPosition = Vector3.zero;
            menuGoNoCanvas.GetComponent<RectTransform>().localScale = Vector3.one;
            menuGoNoCanvas.gameObject.SetActive(false);
            menuScript.mainButton.transform.GetChild(0).GetComponent<Text>().text = "Resume";
            menuScript.exit.transform.GetChild(0).GetComponent<Text>().text = "BackToMenu";
            GenEnemies();
        }
    }
    public void QuitButton()
    {
        if (gamestate == GameState.Gameplaying)
        {
            menuGo.SetActive(true);
            gameGo.SetActive(false);
            menuGoNoCanvas.transform.SetParent(menuScript.gameObject.transform);
            menuGoNoCanvas.GetComponent<RectTransform>().localPosition = Vector3.zero;
            gamestate = GameState.Menu;

        }
        else if (gamestate == GameState.Menu)
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }
    }
}
