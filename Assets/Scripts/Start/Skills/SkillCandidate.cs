using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillCandidate : MonoBehaviour
{
    // この選択候補のSkillData
    public SkillData data;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text skillNameArea;
    [SerializeField] private SkillTarget target;

    public bool IsCurrentlySelected { get; private set; } = false;


    public void SetupSkill()
    {
        // 自身のセットアップ
        spriteRenderer.sprite = data.GetSkillGraphics();
        skillNameArea.SetText(data.GetSkillName());
        if (!gameObject.activeSelf) { gameObject.SetActive(true); }

        // 紐づいているtargetのセットアップ
        target.data = data;
        target.InputSkillData();
        if (target.gameObject.activeSelf) { target.gameObject.SetActive(false); }
    }

    public void ActivateTarget()
    {
        if (!IsCurrentlySelected)
        {
            if (!target.gameObject.activeSelf) { target.gameObject.SetActive(true); }
            IsCurrentlySelected = true;
        }
    }

    public void DeactivateTarget()
    {
        if (IsCurrentlySelected)
        {
            if (target.gameObject.activeSelf) { target.gameObject.SetActive(false); }
            IsCurrentlySelected = false;
        }
    }
}
