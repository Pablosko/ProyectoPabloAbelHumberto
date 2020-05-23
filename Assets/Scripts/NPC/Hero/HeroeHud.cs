using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroeHud : NpcHud
{
    private Text heroeStatsText;
    private Image heroeImage;
    private Image basicSkill;
    private Image[] items = new Image[3];
    public Heroe heroe;
    public GameObject upgradeGo;
    public GameObject UnlockableSkillsContainer;
    public GameObject heroeInfo;

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        heroe = transform.parent.GetChild(0).GetComponent<Heroe>();
        heroeInfo = transform.Find("HeroeInfoHud").gameObject;
        upgradeGo = heroeInfo.transform.Find("Updrade").gameObject;
        UnlockableSkillsContainer = heroeInfo.transform.Find("UnlockableSkillsContainer").gameObject;
        GetGameobjectSkillData();
        if(heroe.stars == 1)
            UpdateSkillsStats();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public void GetGameobjectSkillData()
    {
        foreach (Transform child in transform.Find("HeroeInfoHud").Find("UnlockableSkillsContainer"))
        {
            List<GameObject> temp = new List<GameObject>();
            foreach (Transform childOfChild in child)
            {
                temp.Add(childOfChild.gameObject);
            }

            tiersSkills.Add(temp);
        }
    }
    public void UpdateSkillsStats()
    {
        UnlockableSkillsContainer.SetActive(true);
        for (int i = 0; i < heroe.stars; i ++)
        {
            tiersSkills[i][0].transform.parent.gameObject.SetActive(true);
            int j = 0;
            foreach (GameObject go in tiersSkills[i])
            {
                tiersSkills[i][0].transform.parent.gameObject.SetActive(true);
                string path = "SkillSlots/"+ heroe.clas+"/Tier" + (i + 1).ToString()  + heroe.esp + heroe.final  + "/Skill" + (j + 1).ToString();
                if (i == 0)
                {
                    path = "SkillSlots/" + heroe.clas + "/Tier" + (i + 1).ToString() + "/Skill" + (j + 1).ToString();
                }
                TreeSkill parentSript = Resources.Load<GameObject>(path).GetComponent<TreeSkill>();
                System.Type type = parentSript.GetType();
                Sprite newImage = Resources.Load<Image>(path).sprite;
                tiersSkills[i][j].GetComponent<Image>().sprite = newImage;
                TreeSkill script = (TreeSkill)tiersSkills[i][j].AddComponent(type);
                script = parentSript;
                j++;
            }
        }
    }
    public override void ActivateStatsHud(bool state)
    {
        heroeInfo.SetActive(state);
    }
    public void UpgradeMenu()
    {
        upgradeGo.SetActive(true);
        UnlockableSkillsContainer.SetActive(false);
        upgradeGo.transform.localPosition = tiersSkills[heroe.stars - 1][0].transform.localPosition;
    }
    public void UpgradeTier(int esp)
    {
        if (heroe.mainSynergie.espInt > 0)
        {
            esp += 2;
        }
        if (esp > 1)
        {
            heroe.clas = heroe.mainSynergie.Finaltiertypes[heroe.mainSynergie.espInt][esp];
        }
        else
        {
            heroe.esp = heroe.mainSynergie.tier2types[esp];
            heroe.mainSynergie.espInt = esp;
        }
        upgradeGo.SetActive(false);
        UpdateSkillsStats();
    }
}
