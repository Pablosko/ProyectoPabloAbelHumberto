using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerSynergie : SynergieScript
{
    void Start()
    {
        heroe = GetComponent<Heroe>();
        heroe.clas = "Ranger";
        heroe.mainSynergie = this;
        tier2types.Add("/Light");
        tier2types.Add("/Shadow");
        List<string> test = new List<string>();
        test.Add("/Prietist");
        test.Add("/Purificator");
        Finaltiertypes.Add(test);
        List<string> test2 = new List<string>();
        test2.Add("/Apocaliptic");
        test2.Add("/Demoniac");
        Finaltiertypes.Add(test2);
    }

    void Update()
    {
        
    }
    public void SetTier(int esp,int final)
    {
        heroe.esp = tier2types[esp];
        heroe.final = Finaltiertypes[esp][final];
    }
}
