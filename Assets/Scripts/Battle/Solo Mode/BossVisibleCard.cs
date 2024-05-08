using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossVisibleCard : MonoBehaviour
{
    // ����VisibleCard���R�Â��Ă���Card�I�u�W�F�N�g
    public BossCard attachedCard;

    // �f�[�^���͗p�̗̈�ݒ�
    [SerializeField] private TMP_Text nameInputArea;
    [SerializeField] private TMP_Text nameENGInputArea;
    [SerializeField] private TMP_Text[] effectInputAreas;
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private Canvas canvas;

    // �w�i�̉摜
    [SerializeField] private Sprite backgroundSprite;

    public void InputCardData()
    {
        canvas.worldCamera = Camera.main;
        nameInputArea.SetText(attachedCard.cardName);
        nameENGInputArea.SetText(attachedCard.cardNameENG);
        for (int i = 0; i < 3; i++)
        {
            effectInputAreas[i].SetText(attachedCard.effectTexts[i]);
        }
        backgroundRenderer.sprite = backgroundSprite;
    }
}
