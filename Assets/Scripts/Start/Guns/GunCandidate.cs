using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunCandidate : MonoBehaviour
{
    public GunsData data;

    public GunTarget target;
    [SerializeField] private Image gunIcon;

    public bool IsCurrentlySelected { get; private set; } = false;


    public BoxCollider2D myCollider;

    public void SetupGun()
    {
        // Colliderの取得
        myCollider = GetComponent<BoxCollider2D>();

        // 自身のセットアップ
        gunIcon.sprite = data.GetGunImage();
        gunIcon.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, data.GetIconWidth());
        gunIcon.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis .Vertical, data.GetIconHeight());
        RectTransform myRect = gameObject.GetComponent<RectTransform>();
        myRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, data.GetIconBounds());
        myCollider.size = new Vector2(data.GetIconBounds(),1000);
        if (!gameObject.activeSelf) { gameObject.SetActive(true); }

        // 紐づいたGunTargetのセットアップ
        target.data = data;
        target.InputGunData();
        if (target.gameObject.activeSelf) { target.gameObject.SetActive(false); }
    }

    public void ActivateTarget()
    {
        if (!IsCurrentlySelected)
        {
            transform.localScale = Vector3.one;
            if (!target.gameObject.activeSelf) { target.gameObject.SetActive(true); }
            IsCurrentlySelected = true;
        }
    }

    public void DeactivateTarget()
    {
        if (IsCurrentlySelected)
        {
            float num = float.Parse("0.75");
            transform.localScale = new Vector3(num, num, num);
            if (target.gameObject.activeSelf) { target.gameObject.SetActive(false); }
            IsCurrentlySelected = false;
        }
    }

    public void SetInavailableForCandidate()
    {
        gunIcon.color = Color.black;
    }
}
