using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class StoneSynergie : SynergieScript
{
    public override void SynergieEffect(Heroe heroe)
    {
        base.SynergieEffect(heroe);
        heroe.hp += 10;
    }
}
