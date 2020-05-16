using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinChangePosition : MonoBehaviour
{
    Vector2 fistPositionCoin;
    Vector2 secondPositionCoin;

    // Start is called before the first frame update
    void Start()
    {
        fistPositionCoin = gameObject.GetComponent<RectTransform>().anchoredPosition;
        secondPositionCoin = new Vector2(-585.2f, -272.7f);
    }
    public void openShop()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = secondPositionCoin;
    }
    public void CloseShop()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = fistPositionCoin;
    }
}
