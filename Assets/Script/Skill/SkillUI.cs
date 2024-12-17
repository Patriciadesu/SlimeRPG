using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private Transform playerSkillsContent;
    [SerializeField] private GameObject skillInfo;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private Transform upgradeState;

    private void Start()
    {
        LoadSkillList();
        SkillManager.Instance.onSkillChanged += LoadSkillList;
    }

    private void LoadSkillList()
    {
        var skillHaves = SkillManager.Instance.skills.Where(s => s.Have).ToArray();
        skillHaves = skillHaves.Select(s => Skill.GetMaxLevel(s.GetType(), true)).ToArray();
        skillHaves = new HashSet<Skill>(skillHaves).ToArray();

        foreach (Transform playerSkillUI in playerSkillsContent)
            Destroy(playerSkillUI.gameObject);

        foreach (var skillHave in skillHaves)
        {
            var skillProfileUI = Instantiate(Resources.Load<Transform>("Skill UI/Skill Profile"));

            skillProfileUI.Find("Skill Name").GetComponent<TextMeshProUGUI>().text = skillHave.Name;

            skillProfileUI.GetComponent<Button>().onClick.RemoveAllListeners();
            skillProfileUI.GetComponent<Button>().onClick.AddListener(() =>
                SelectSkill(skillHave, Skill.GetAllLevels(skillHave.GetType(), false))
            );

            skillProfileUI.SetParent(playerSkillsContent);
        }

        if (skillHaves.Length > 0)
        {
            skillInfo.SetActive(true);
            SelectSkill(skillHaves[0], Skill.GetAllLevels(skillHaves[0].GetType(), false));
        }
        else
            skillInfo.SetActive(false);
    }

    private void SelectSkill(Skill skill, Skill[] allSkills)
    {
        nameText.text = $"Name : {skill.Name} Lv.{skill.Level}";
        typeText.text = $"Type : {skill.GetType().Name}";
        if (skill is ActiveSkill activeSkill)
        {
            rarityText.text = $"Grade : {activeSkill.rarity}";
            rarityText.gameObject.SetActive(true);
        }
        else rarityText.gameObject.SetActive(false);

        foreach (Transform upgradeUI in upgradeState)
            Destroy(upgradeUI.gameObject);


        foreach (var skillLevel in allSkills)
        {
            var upgradingPathUI = Instantiate(Resources.Load<Transform>("Skill UI/Upgrading Path"));

            upgradingPathUI.Find("State Sprite").GetComponent<Image>().sprite = skillLevel.SkillSprite;

            upgradingPathUI.Find("State Sprite/Lock Dim").gameObject.SetActive(!skillLevel.Have);

            upgradingPathUI.Find("Level").GetComponent<TextMeshProUGUI>().text = $"Lv.{skillLevel.Level}";

            upgradingPathUI.GetComponent<Button>().onClick.RemoveAllListeners();
            upgradingPathUI.GetComponent<Button>().onClick.AddListener(() => SelectSkill(skillLevel, allSkills));

            upgradingPathUI.SetParent(upgradeState);
        }
    }
}
