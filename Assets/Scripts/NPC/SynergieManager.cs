using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynergieManager : MonoBehaviour
{
    public Dictionary<string, int> synergies = new Dictionary<string, int>();
    public Dictionary<string, SynergySlotScript> synergiesHud = new Dictionary<string, SynergySlotScript>();
    public GameObject SynergiePrefab;
    public Transform content;
    void Start()
    {
    }

    void FixedUpdate()
    {
        
    }
    public void AddHeroSynergies(Heroe heroe)
    {
        foreach (SynergieScript synergie in heroe.GetComponents<SynergieScript>())
        {
            if (!ContainsSynergie(synergie))
            {
                SynergySlotScript slot = Instantiate(SynergiePrefab, content).GetComponent<SynergySlotScript>();
                synergies.Add(synergie.type.ToString(), 1);
                synergiesHud.Add(synergie.type.ToString(),slot);

                slot.SetStats(synergie,synergies[synergie.type.ToString()]);
            }
            else
            {
                if (synergies[synergie.type.ToString()] == 0)
                {
                    SynergySlotScript slot = Instantiate(SynergiePrefab, content).GetComponent<SynergySlotScript>();
                    synergies[synergie.type.ToString()]++;
                    synergiesHud[synergie.type.ToString()] = slot;
                    slot.SetStats(synergie, synergies[synergie.type.ToString()]);
                }
                else
                {
                    synergies[synergie.type.ToString()]++;
                    synergiesHud[synergie.type.ToString()].SetStats(synergie, synergies[synergie.type.ToString()]);
                }
            }
        }
    }
    public bool ContainsSynergie(SynergieScript newSynergie)
    {
        foreach (KeyValuePair<string,int> synergie in synergies)
        {
            if (synergie.Key == newSynergie.type.ToString())
            {
                return true;
            }
        }
        return false;
    }
    public void RemoveHeroSynergies(Heroe heroe)
    {
        foreach (SynergieScript synergie in heroe.GetComponents<SynergieScript>())
        {
            if (ContainsSynergie(synergie))
            {
                synergies[synergie.type.ToString()]--;
                synergiesHud[synergie.type.ToString()].SetStats(synergie, synergies[synergie.type.ToString()]);
                if (synergies[synergie.type.ToString()] <= 0)
                {
                    Destroy(synergiesHud[synergie.type.ToString()].gameObject);
                }
            }
        }
    }
}