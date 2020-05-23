using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Heroe_Dario : Heroe
{

    private void Awake()
    {
        base.Awake();
        GetSynergies();

    }
    void Start()
    {
    }

    void Update()
    {
        base.Update();
    }
}
