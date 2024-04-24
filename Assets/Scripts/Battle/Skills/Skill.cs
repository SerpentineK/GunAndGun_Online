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

    // コスト
    public int cost;

    // 効果
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
