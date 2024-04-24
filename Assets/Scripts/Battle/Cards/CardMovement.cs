using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CardMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 originalPosition;
    private HorizontalLayoutGroup layoutGroup;
    private enum CardStatus
    {
        NULL,
        IN_HAND,
        MOVING
    }
    private CardStatus status;

    [SerializeField] private GameObject HikagiriLimitExplanation_Action;
    [SerializeField] private Canvas actionLimitCanvas;
    [SerializeField] private GameObject HikagiriLimitExplanation_Bullet;
    [SerializeField] private Canvas bulletLimitCanvas;

    [SerializeField] private Canvas mainCanvas;

    public void Awake()
    {
        actionLimitCanvas.worldCamera = Camera.main;
        bulletLimitCanvas.worldCamera = Camera.main;
        actionLimitCanvas.sortingLayerName = "Cards";
        actionLimitCanvas.sortingOrder = 1;
        bulletLimitCanvas.sortingLayerName = "Cards";
        bulletLimitCanvas.sortingOrder = 1;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        this.originalPosition = transform.position;
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        this.status = CardStatus.MOVING;
        this.gameObject.GetComponent<SortingGroup>().sortingLayerName = "Interactable";
        mainCanvas.sortingLayerName = "Interactable";
        bulletLimitCanvas.sortingLayerName = "Interactable";
        actionLimitCanvas.sortingLayerName = "Interactable";
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        transform.position = currentPosition;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        if (transform.position.y > 0) 
        {
            Card myCard = this.GetComponent<Card>();
            if (myCard.cardType != CardData.CardType.SpecialBullet)
            {
                this.status = CardStatus.NULL;
            }
            else
            {
                transform.position = originalPosition;
                this.status = CardStatus.IN_HAND;
            }
        }
        else
        {
            transform.position = originalPosition;
            this.status = CardStatus.IN_HAND;
        }
        this.gameObject.GetComponent<SortingGroup>().sortingLayerName = "Cards";
        mainCanvas.sortingLayerName = "Cards";
        bulletLimitCanvas.sortingLayerName = "Cards";
        actionLimitCanvas.sortingLayerName = "Cards";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RectTransform trans = this.gameObject.GetComponent<RectTransform>();
        trans.localScale = new Vector3(2.5f, 2.5f, 1.0f);
        BoxCollider2D collider = this.gameObject.GetComponent<BoxCollider2D>();
        collider.size = trans.sizeDelta;
        if (this.status != CardStatus.MOVING) 
        {
            layoutGroup = this.gameObject.GetComponentInParent<HorizontalLayoutGroup>();
            layoutGroup.enabled = false;
            layoutGroup.enabled = true;
        }
        Card myCard = this.GetComponent<Card>();
        if (HikagiriLimitExplanation_Action.activeSelf) { HikagiriLimitExplanation_Action.SetActive(false); }
        if (HikagiriLimitExplanation_Bullet.activeSelf) { HikagiriLimitExplanation_Bullet.SetActive(false); }
        if (myCard.cardEffectHub.isLimitedByHikagiri)
        {
            if (myCard.cardType == CardData.CardType.Action)
            {
                HikagiriLimitExplanation_Action.SetActive(true);
            }
            else if(myCard.cardType == CardData.CardType.SpecialBullet)
            {
                HikagiriLimitExplanation_Bullet.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RectTransform trans = this.gameObject.GetComponent<RectTransform>();
        trans.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        BoxCollider2D collider = this.gameObject.GetComponent<BoxCollider2D>();
        collider.size = trans.sizeDelta;
        if (this.status != CardStatus.MOVING)
        {
            layoutGroup = this.gameObject.GetComponentInParent<HorizontalLayoutGroup>();
            layoutGroup.enabled = false;
            layoutGroup.enabled = true;
        }
        if (HikagiriLimitExplanation_Action.activeSelf) { HikagiriLimitExplanation_Action.SetActive(false); }
        if (HikagiriLimitExplanation_Bullet.activeSelf) { HikagiriLimitExplanation_Bullet.SetActive(false); }
    }
}
