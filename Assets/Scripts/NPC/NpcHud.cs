using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcHud : MonoBehaviour
{
    public Image filled;
    public Transform bar;
    public List<List<GameObject>> tiersSkills = new List<List<GameObject>>();
    public Transform barGo;
    // Start is called before the first frame update
    public void Start()
    {
        barGo = transform.parent.GetChild(0).Find("Mesh").Find("BarGo");
        bar = transform.Find("Bar");
        filled = bar.transform.Find("Filled").GetComponent<Image>(); 
    }

    // Update is called once per frame
    public void Update()
    {
        bar.position = Camera.main.WorldToScreenPoint(barGo.transform.position);
    }
    public void UpdateBar(float hp,float maxHp)
    {
        filled.fillAmount = 1 - hp / maxHp;
    }
    public virtual void ActivateStatsHud(bool state)
    {

    }
}
