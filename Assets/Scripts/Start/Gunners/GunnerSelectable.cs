using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunnerSelectable : MonoBehaviour
{
    // targetに記入するためのデータ
    public GunnerData data;
    
    // 縦長のアイコンオブジェクトと、それが選択された際に有効化する拡大表示
    [SerializeField] private GunnerCandidate candidate;
    [SerializeField] private GunnerTarget target;

    public bool isCurrentlySelected { get; private set; } = false;

    private RectTransform myRect;
    private BoxCollider2D myCollider;


    public void SetupGunner()
    {
        // candidateのセットアップ
        candidate.data = data;
        candidate.InputGunnerData();
        if (!candidate.gameObject.activeSelf) { candidate.gameObject.SetActive(true); }

        // targetのセットアップ
        target.data = data;
        target.InputGunnerData();
        if (target.gameObject.activeSelf) { target.gameObject.SetActive(false); }

        // 自身のプロパティのセットアップ
        myRect = GetComponent<RectTransform>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    public void DeactivateTarget() 
    {
        if (isCurrentlySelected)
        {
            if (target.gameObject.activeSelf) { target.gameObject.SetActive(false); }
            if (!candidate.gameObject.activeSelf) { candidate.gameObject.SetActive(true); }
            isCurrentlySelected = false;
            myRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 480);
            myRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1600);
            myCollider.size = new Vector2(480, 1600);
            
        }
    }

    public void ActivateTarget()
    {
        if (!isCurrentlySelected)
        {
            if (!target.gameObject.activeSelf) { target.gameObject.SetActive(true); }
            if (candidate.gameObject.activeSelf) { candidate.gameObject.SetActive(false); }
            isCurrentlySelected = true;
            myRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 2100);
            myRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 2037);
            myCollider.size = new Vector2(2100, 2037);
        }
    }

    public void SetInavailable()
    {
        candidate.SetInavailableForCandidate();
        target.SetInavailableForTarget();
    }
}
