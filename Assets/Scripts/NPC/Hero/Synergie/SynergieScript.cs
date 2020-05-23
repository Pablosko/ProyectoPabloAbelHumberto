using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SynergieType
{
        Water,
        Fire,
        Wind,
        Stone
}
[System.Serializable]
public class SynergieScript : MonoBehaviour
{

    public Heroe heroe;
    public List<string> tier2types = new List<string>();
    public List<List<string>> Finaltiertypes = new List<List<string>>();
    public SynergieType type;
    public int espInt = 0;
    public string synergieName = "";
    public string synergieDescription = "";
    public List<Heroe> synergieHeroes = new List<Heroe>();
    public int[] synergieLevels;

    public virtual void SynergieEffect(Heroe heroe)
    {
        Debug.Log("Efecto de synergia activo");
    }
    public void Start()
    {
        synergieName = type.ToString();
    }
}
