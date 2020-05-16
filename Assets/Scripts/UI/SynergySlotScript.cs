using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynergySlotScript : MonoBehaviour
{
    Text currentSynergyHeros;
    Text synergyLevels;
    Text descriptionText;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void SetStats(SynergieScript synergie,int amount)
    {       
        descriptionText = transform.Find("SynergieDescription").GetChild(0).GetComponent<Text>();
        synergyLevels = transform.Find("Content").Find("SynergieNum").GetComponent<Text>();
        currentSynergyHeros = transform.Find("Content").Find("ContentOfNumHero").Find("NumOfHeroWithSynergie").GetComponent<Text>();
        currentSynergyHeros.text = "" + amount;
        descriptionText.text = synergie.synergieDescription;
        for (int i = 0; i < synergie.synergieLevels.Length; i++)
        {
            if (i == 0)
                synergyLevels.text = "" + synergie.synergieLevels[i];
            else
                synergyLevels.text += "/"+ synergie.synergieLevels[i] ;
        }
    }
}
