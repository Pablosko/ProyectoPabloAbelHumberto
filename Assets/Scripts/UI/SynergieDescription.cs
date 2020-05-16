using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SynergieDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject synergieDescription;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        synergieDescription.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        synergieDescription.SetActive(false);
    }
}
