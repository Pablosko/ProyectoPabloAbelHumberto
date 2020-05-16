using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcHud : MonoBehaviour
{
    public Image filled;
    public Transform bar;
    public Transform barGo;
    // Start is called before the first frame update
    void Start()
    {
        barGo = transform.parent.GetChild(0).GetChild(0);
        bar = transform.GetChild(0);
        filled = bar.transform.Find("Filled").GetComponent<Image>(); 
    }

    // Update is called once per frame
    void Update()
    {
         bar.position = Camera.main.WorldToScreenPoint(barGo.transform.position);
    }
    public void UpdateBar(float hp,float maxHp)
    {
        filled.fillAmount = 1 - hp / maxHp;
    }
}
