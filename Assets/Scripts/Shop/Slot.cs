using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject currentHero;
    public Text name;
    public Image image;
    public Text cost;
    Bench bench;
    // Start is called before the first frame update
    void Start()
    {
        name = transform.Find("HeroText").GetComponent<Text>();
        cost = transform.Find("Cost").GetComponent<Text>();
        image = GetComponent<Image>();
        bench = GameObject.Find("banquillo").GetComponent<Bench>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSlot()
    {
        name.text = currentHero.transform.GetChild(0).GetComponent<Heroe>().name;
        cost.text = "" + currentHero.transform.GetChild(0).GetComponent<Heroe>().tier;
        image.sprite = currentHero.transform.GetChild(0).GetComponent<Heroe>().icon;
    }

    public void BuyHeroe()
    {
        if (Gamecontroller.instance.player.HaveMoney((int)currentHero.transform.GetChild(0).GetComponent<Heroe>().tier + 1))
        {
            Tile tempTile = bench.GetEmptyTile();
            Gamecontroller.instance.player.gold -= (int)currentHero.transform.GetChild(0).GetComponent<Heroe>().tier + 1;
            GameObject hero = Instantiate(currentHero, tempTile.transform);
            tempTile.currentNPC = hero.transform.GetChild(0).GetComponent<Heroe>();
            tempTile.currentNPC.currentTile = tempTile;
            hero.transform.localPosition = Vector3.zero;
            hero.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
