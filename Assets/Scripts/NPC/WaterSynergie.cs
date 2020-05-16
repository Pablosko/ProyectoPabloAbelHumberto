using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class WaterSynergie : SynergieScript
{   
    public override void SynergieEffect(Heroe heroe)
    {
        type = SynergieType.Water;
        base.SynergieEffect(heroe);
        heroe.mana += 10;
    }
    public void Start()
    {
        type = SynergieType.Water;
        base.Start();
    }

}
