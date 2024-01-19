using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    // 技能のデータ
    public SkillData data;
    
    // データ入力用の領域
    [SerializeField] private TMP_Text skillCostArea;
    [SerializeField] private TMP_Text skillNameArea;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // コストを確認するためのボルテージ参照
    [SerializeField] private Field voltage;

    // ボルテージから捨て札へカードを移動させるためのFieldManager参照
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
