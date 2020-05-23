using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class WaterSynergie : SynergieScript
{   
    public override void SynergieEffect(Heroe heroe)
    {
        type = SynergieType.Water;
        base.SynergieEffect(heroe);
    }
    public void Start()
    {
        type = SynergieType.Water;
        base.Start();

    }

}
