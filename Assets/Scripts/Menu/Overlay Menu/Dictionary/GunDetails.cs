using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DictionaryMenu
{
    public class GunDetails : MonoBehaviour
    {
        [SerializeField] private Image gunDetailsArea;
        [SerializeField] private Image deckDetailsArea;

        public void SetDetails(GunsData data)
        {
            RectTransform myRect = gameObject.GetComponent<RectTransform>();

            float myWidth = myRect.rect.width;
            float myHeight = 0f;
            
            myHeight += SetImageSize(gunDetailsArea, data.GunDetails, myWidth);
            myHeight += SetImageSize(deckDetailsArea, data.GetDeckDatabase().DeckDetails, myWidth);

            gameObject.GetComponent<ScrollRect>().content.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, myHeight);
        }

        public static float SetImageSize(Image targetImage, Sprite mySprite, float myWidth)
        {
            targetImage.sprite = mySprite;
            targetImage.preserveAspect = true;

            Vector2 preferredSize = new(targetImage.preferredWidth, targetImage.preferredHeight);
            RectTransform childRect = targetImage.GetComponent<RectTransform>();

            float childHeight = CalculateHeight(preferredSize, myWidth);

            childRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, myWidth);
            childRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, childHeight);

            return childHeight;
        }

        public static float CalculateHeight(Vector2 preferred, float parentWidth)
        {
            float result = preferred.y * (parentWidth / preferred.x);
            return result;
        }
    }
}