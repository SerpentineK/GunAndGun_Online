using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Card�����^�f�[�^�������N���X�Ƃ��A�����̎�D�ƃZ�b�g�ɂ���J�[�h�݂̂���VisibleCard�ŏ펞�\���������B
// �������ɐV����Card�I�u�W�F�N�g�𐶐����A���g�ɕR�Â���B
// �v���n�u�ƕR�Â���̂͂�����B
public class VisibleCard : MonoBehaviour
{
    // ����VisibleCard���R�Â��Ă���Card�I�u�W�F�N�g
    public Card attachedCard;

    // �f�[�^���͗p�̗̈�ݒ�
    [SerializeField] private TMP_Text nameInputArea;
    [SerializeField] private TMP_Text effectInputArea;
    [SerializeField] private TMP_Text costInputArea;
    [SerializeField] private TMP_Text typeInputArea;
    [SerializeField] private SpriteRenderer gunSpriteRenderer;
    [SerializeField] private SpriteRenderer coloredPanelRenderer;
    [SerializeField] private SpriteRenderer whitePanelRenderer;
    [SerializeField] private Canvas canvas;

    public void InitiateMetaCard() 
    {
        attachedCard = this.gameObject.AddComponent<Card>();
        attachedCard.visibleCard = this;
    }

    // �������ꂽ�J�[�hObject�Ƀv���p�e�B��p���ē��͂��ꂽ�l�𔽉f���郁�\�b�h
    public void InputCardData()
    {
        canvas.worldCamera = Camera.main;
        nameInputArea.SetText(attachedCard.cardName);
        effectInputArea.SetText(attachedCard.effectText);
        costInputArea.SetText("COST\n" + string.Format("{0:00}", attachedCard.cost));
        string typeText = null;
        Color color = new();
        if (attachedCard.cardType == CardData.CardType.Action) 
        { 
            typeText = "�s��";
            color.a = 1f;
            color.r = 0f;
            color.g = 230f;
            color.b = 230f;
        }
        else if (attachedCard.cardType == CardData.CardType.Reaction) 
        { 
            typeText = "�Ή�";
            color.a = 1f;
            color.r = 200f;
            color.g = 0f;
            color.b = 200f;
        }
        else if (attachedCard.cardType == CardData.CardType.Mechanism)
        {
            typeText = "�@�\";
            color.a= 1f;
            color.r = 0f;
            color.g = 200f;
            color.b = 0f;
        }
        else if (attachedCard.cardType == CardData.CardType.SpecialBullet)
        {
            typeText = "�e�e";
            color.a = 1f;
            color.r = 230f;
            color.g = 230f;
            color.b = 0f;
        }
        typeInputArea.SetText(typeText);
        coloredPanelRenderer.color = color;
        coloredPanelRenderer.sortingLayerName = "Cards";
        coloredPanelRenderer.sortingOrder = 2;
        if (attachedCard.cardEffectHub.isOverclock)
        {
            whitePanelRenderer.color = Color.black;
        }
        else
        {
            whitePanelRenderer.color = Color.white;
        }
        gunSpriteRenderer.sprite = attachedCard.gunSprite;
    }

}
