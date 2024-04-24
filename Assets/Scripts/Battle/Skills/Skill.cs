using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    // �Z�\�̃f�[�^
    public SkillData data;
    
    // �f�[�^���͗p�̗̈�
    [SerializeField] private TMP_Text skillCostArea;
    [SerializeField] private TMP_Text skillNameArea;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // �R�X�g���m�F���邽�߂̃{���e�[�W�Q��
    [SerializeField] private Field voltage;

    // �{���e�[�W����̂ĎD�փJ�[�h���ړ������邽�߂�FieldManager�Q��
    [SerializeField] private FieldManager fieldManager;

    // �R�X�g
    public int cost;

    // ����
    public EffectHub effectHub;

    public void InputSkillData()
    {
        cost = data.GetSkillCost();
        skillCostArea.SetText(string.Format("{0:00}",cost));
        skillNameArea.SetText(data.GetSkillName());
        spriteRenderer.sprite = data.GetSkillGraphics();
        effectHub = data.effectHub;
    }

    public bool IsEnoughVolt()
    {
        if (cost<=voltage.cardCount) { return true; }
        else { return false; }
    }
}
