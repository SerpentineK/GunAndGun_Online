using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunnerTarget : MonoBehaviour
{
    // �e�m�̃f�[�^
    public GunnerData data;

    // �f�[�^���͗p�̗̈�
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

    // SSM�ɑI�����ꂽ�f�[�^��Ԃ��{�^��
    [SerializeField] private GunnerConfirmButton confirmationButton;

    // �e�m�I��s�\�̕\��
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

    // 1�Z�b�g��őΐ푊�肪�I���ς݂̏e�m��I��s�\�ƕ\��������
    public void SetInavailableForTarget()
    {
        if (confirmationButton.gameObject.activeSelf) { confirmationButton.gameObject.SetActive(false); }
        if (!gunnerInavailableDisplay.activeSelf) { gunnerInavailableDisplay.SetActive(true); }
    }
}
