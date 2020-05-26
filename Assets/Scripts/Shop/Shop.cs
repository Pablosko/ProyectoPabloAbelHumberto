using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    List<Slot> slots = new List<Slot>();
    public Color[] tierColors;
    public int rollPrice;
    public int levelUpPrice;
    public AudioSource audioSource;
    public Text rollPriceText;
    public Text levelUpPriceText;
    public Image expBar;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rollPriceText.text = rollPrice.ToString();
        levelUpPriceText.text = levelUpPrice.ToString();       
        foreach (Transform child in transform)
        {
            slots.Add(child.gameObject.transform.GetChild(1).GetComponent<Slot>());
        }
        //set slots
        foreach (Slot slot in slots)
        {
            int number = Random.Range(0, 100);
            slot.gameObject.transform.parent.gameObject.SetActive(true);
            slot.currentHero = DrawFromPool(number);
            slot.SetSlot();
        }
    }

    public void Roll()
    {
        if (Gamecontroller.instance.player.HaveMoney(rollPrice))
        {
            Utils.PlayRandomAudio(SoundScript.instance.RollEffectsClips,audioSource,false);
            Gamecontroller.instance.player.AddGold(-2);
            foreach (Slot slot in slots)
            {
                int number = Random.Range(0, 100);
                slot.gameObject.transform.parent.gameObject.SetActive(true);
                slot.currentHero = DrawFromPool(number);
                slot.SetSlot();
            }
        }
    }
    public GameObject DrawFromPool(int number)
    {
        int prob = 0;
        for (int i = 0; i < Gamecontroller.instance.tierProbs.Length; i++)
        {
            prob += Gamecontroller.instance.tierProbs[i];
            if (number < prob)
            {
                return Gamecontroller.instance.pool.GetRandomHero(i);
            }
        }
        return null;
    }

   

}
