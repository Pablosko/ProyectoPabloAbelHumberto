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

    [SerializeField]
    public SynergieType type;
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
