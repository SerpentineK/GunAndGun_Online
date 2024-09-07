using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTarget : MonoBehaviour
{
    public SkillData data;

    [SerializeField] private TMP_Text skillNameArea;
    [SerializeField] private TMP_Text costArea;
    [SerializeField] private TMP_Text effectArea;
    [SerializeField] private TMP_Text flavorTextArea;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private SkillConfirmButton confirmButton;

    public void InputSkillData()
    {
        skillNameArea.SetText(data.GetSkillName());
        data.UpdateCostValue();
        costArea.SetText(data.skillCostValue.MyStringValue);
        effectArea.SetText(data.GetSkillEffectText());
        flavorTextArea.SetText(data.GetSkillFlavorText());
        Sprite sprite = data.GetSkillGraphics();
        spriteRenderer.sprite = sprite;
        backgroundRenderer.sprite = sprite;
        confirmButton.data = data;
    }
}
