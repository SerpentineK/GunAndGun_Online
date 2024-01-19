using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunnerTarget : MonoBehaviour
{
    // 銃士のデータ
    public GunnerData data;

    // データ入力用の領域
    [SerializeField] private Image gunnerImageArea;
    [SerializeField] private TMP_Text gunnerNameArea;
    [SerializeField] private TMP_Text agilityArea;
    [SerializeField] private TMP_Text handArea;
    [SerializeField] private TMP_Text passiveAbilityArea;
    [SerializeField] private TMP_Text flavorTextArea;
    [SerializeField] private Image[] skillImageAreas = new Image[3];
    [SerializeField] private TMP_Text[] skillNameAreas = new TMP_Text[3];

    // SSM (StartStateManager)
    [SerializeField] private StartSceneManager SSM;

    // SSMに選択されたデータを返すボタン
    [SerializeField] private GunnerConfirmButton confirmationButton;

    // 銃士選択不可能の表示
    [SerializeField] private GameObject gunnerInavailableDisplay;

    public void InputGunnerData()
    {
        gunnerImageArea.sprite = data.GetGunnerImage();
        gunnerNameArea.SetText(data.GetGunnerName());
        agilityArea.SetText(string.Format("{0:00}", data.GetGunnerAgility()));
        string displayedGunnerHand;
        if (data.GetGunnerHand() != 0)
        {
            displayedGunnerHand = string.Format("{0:00}", data.GetGunnerHand());
        }
        else
        {
            displayedGunnerHand = "X";
        }
        handArea.SetText(displayedGunnerHand);
        passiveAbilityArea.SetText(data.GetGunnerAbility());
        flavorTextArea.SetText(data.GetGunnerFlavorText());
        SkillData[] skills = data.GetSkillArray();
        for (int i = 0; i < skills.Length; i++)
        {
            skillImageAreas[i].sprite = skills[i].GetSkillGraphics();
            skillNameAreas[i].SetText(skills[i].GetSkillName());
        }
        confirmationButton.confirmedData = data;
    }

    // 1セット戦で対戦相手が選択済みの銃士を選択不可能と表示したい
    public void SetInavailableForTarget()
    {
        if (confirmationButton.gameObject.activeSelf) { confirmationButton.gameObject.SetActive(false); }
        if (!gunnerInavailableDisplay.activeSelf) { gunnerInavailableDisplay.SetActive(true); }
    }
}
