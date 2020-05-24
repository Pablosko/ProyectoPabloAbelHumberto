using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tempAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
        DontDestroyOnLoad(tempAudio);
    }

//game.columVelocity = game.tempColumVelocity;
//game.metros = 0;
//game.currentTime = 0.0f;
//player.isAlive = true;
//player.isMeta = false;
//player.imagePlayer.sprite.name = player.nameGeo[Random.Range(0, 4)];
//player.SetGeometry();

public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
