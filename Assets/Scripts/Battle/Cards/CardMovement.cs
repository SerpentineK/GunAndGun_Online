using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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


    public void OnBeginDrag(PointerEventData eventData)
    {
        this.originalPosition = transform.position;
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        this.status = CardStatus.MOVING;
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
            if (myCard.cardType == CardData.CardType.Action | myCard.cardType == CardData.CardType.Mechanism)
            {
                EffectManager.instance.UseCard(myCard);
                status = CardStatus.NULL;
            }
            else if (myCard.cardType == CardData.CardType.Reaction)
            {
                EffectManager.instance.SetReaction(myCard);
                status = CardStatus.NULL;
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
    }
}
