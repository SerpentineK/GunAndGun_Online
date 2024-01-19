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

    public void InputSkillData()
    {
        skillCostArea?.SetText(string.Format("{0:00}",data.GetSkillCost()));
        skillNameArea?.SetText(data.GetSkillName());
        spriteRenderer.sprite = data.GetSkillGraphics();
    }

    public bool IsEnoughVolt()
    {
        if (data.GetSkillCost()<=voltage.cardCount) { return true; }
        else { return false; }
    }


    public void Start()
    {

    }

    public void Update()
    {
        
    }
}
