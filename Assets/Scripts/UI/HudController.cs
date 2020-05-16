using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public Text goldText;
    public Image expBar;
    public Text rollPriceText;
    public Text levelUpPriceText;
    public Text stageText;
    public Text timerText;
    public Image timeFillImage;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
        UpdateXpBar();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void UpdateText()
    {
        goldText.text = ""+ Gamecontroller.instance.player.gold;
    }
    public void UpdateXpBar()
    {
         expBar.fillAmount = Gamecontroller.instance.player.exp / Gamecontroller.instance.player.maxExp;
    }
    public void UpdateStageTimer(float time,float maxTime,int round)
    {
        if(!timeFillImage.transform.parent.gameObject.activeSelf)
            timeFillImage.transform.parent.gameObject.SetActive(false);

        timeFillImage.fillAmount = time/maxTime;
        time = (int)(time * Mathf.Pow(10,round));
        float restTime = maxTime - (time / Mathf.Pow(10, round));
        if (restTime < 0.2f)
        {
            timeFillImage.transform.parent.gameObject.SetActive(false);
            timerText.text = "";
        }
        else
        timerText.text = restTime.ToString();
    }
    public void UpdateStageText(int stage)
    {
        stageText.text = "Stage " + stage.ToString();
    }
}
