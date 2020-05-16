using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class WindSynergie : SynergieScript
{
    public override void SynergieEffect(Heroe heroe)
    {
        base.SynergieEffect(heroe);
        heroe.dodge += 10;
    }
    public void Start()
    {
        type = SynergieType.Wind;
        base.Start();
    }
}
