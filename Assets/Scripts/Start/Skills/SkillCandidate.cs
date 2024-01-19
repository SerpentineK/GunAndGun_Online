using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillCandidate : MonoBehaviour
{
    // ���̑I������SkillData
    public SkillData data;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text skillNameArea;
    [SerializeField] private SkillTarget target;

    public bool IsCurrentlySelected { get; private set; } = false;


    public void SetupSkill()
    {
        // ���g�̃Z�b�g�A�b�v
        spriteRenderer.sprite = data.GetSkillGraphics();
        skillNameArea.SetText(data.GetSkillName());
        if (!gameObject.activeSelf) { gameObject.SetActive(true); }

        // �R�Â��Ă���target�̃Z�b�g�A�b�v
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
