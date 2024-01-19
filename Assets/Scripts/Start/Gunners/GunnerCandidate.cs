using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GunnerCandidate : MonoBehaviour
{
    public GunnerData data;

    [SerializeField] private SpriteRenderer iconSpriteRenderer;
    [SerializeField] private SpriteRenderer greyBackgroundRenderer;
    [SerializeField] private TMP_Text iconLabel;

    public void InputGunnerData()
    {
        iconSpriteRenderer.sprite = data.GetGunnerImage();
        iconSpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        iconSpriteRenderer.transform.localPosition = data.GetIconVector();
        iconSpriteRenderer.transform.localScale = data.GetIconScale();
        iconLabel.color = data.GetIconLabelColor();
        iconLabel.SetText(data.GetGunnerName());
    }

    // 1�Z�b�g��őΐ푊�肪�I���ς݂̏e�m��I��s�\�ƕ\��������
    public void SetInavailableForCandidate()
    {
        var greyOverlayColor = Color.grey;
        iconSpriteRenderer.color = greyOverlayColor;
        greyBackgroundRenderer.color = greyOverlayColor;
    }
}
