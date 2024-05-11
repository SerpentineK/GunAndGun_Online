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
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private Canvas canvas;

    // �J�[�h�̑I�����\�ɂ���R���|�[�l���g
    [SerializeField] private SelectableObject selectable;

    // �w�i�̉摜
    [SerializeField] private Sprite actionSprite;
    [SerializeField] private Sprite reactionSprite;
    [SerializeField] private Sprite mechanismSprite;
    [SerializeField] private Sprite specialBulletSprite;
    [SerializeField] private Sprite actionOverclockSprite;
    [SerializeField] private Sprite reactionOverclockSprite;
    [SerializeField] private Sprite mechanismOverclockSprite;
    [SerializeField] private Sprite specialBulletOverclockSprite;

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
        if (attachedCard.cardType == CardData.CardType.Action) 
        { 
            typeText = "�s��";
            if (attachedCard.isOverclock)
            {
                backgroundRenderer.sprite = actionOverclockSprite;
            }
            else
            {
                backgroundRenderer.sprite = actionSprite;
            }
        }
        else if (attachedCard.cardType == CardData.CardType.Reaction) 
        { 
            typeText = "�Ή�";
            if (attachedCard.isOverclock)
            {
                backgroundRenderer.sprite = reactionOverclockSprite;
            }
            else
            {
                backgroundRenderer.sprite = reactionSprite;
            }
        }
        else if (attachedCard.cardType == CardData.CardType.Mechanism)
        {
            typeText = "�@�\";
            if (attachedCard.isOverclock)
            {
                backgroundRenderer.sprite = mechanismOverclockSprite;
            }
            else
            {
                backgroundRenderer.sprite = mechanismSprite;
            }
        }
        else if (attachedCard.cardType == CardData.CardType.SpecialBullet)
        {
            typeText = "�e�e";
            if (attachedCard.isOverclock)
            {
                backgroundRenderer.sprite = specialBulletOverclockSprite;
            }
            else
            {
                backgroundRenderer.sprite = specialBulletSprite;
            }
        }
        typeInputArea.SetText(typeText);
        gunSpriteRenderer.sprite = attachedCard.gunSprite;
        selectable.mask.sprite = backgroundRenderer.sprite;
    }

}
