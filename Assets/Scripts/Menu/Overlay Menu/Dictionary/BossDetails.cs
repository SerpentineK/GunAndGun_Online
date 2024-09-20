using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DictionaryMenu
{
    public class BossDetails : MonoBehaviour
    {
        [SerializeField] private Image imageArea;
        [SerializeField] private RectTransform myRect;
        private float myHeight = 0;

        public void SetToDetails(BossData data)
        {
            imageArea.sprite = data.GetBossDetails();
            myHeight = GunDetails.SetImageSize(imageArea, imageArea.sprite, myRect.rect.width);
            imageArea.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, myHeight);
        }

        public void SetToStages(BossData data)
        {
            imageArea.sprite = data.GetBossStageDetails();
            myHeight = GunDetails.SetImageSize(imageArea, imageArea.sprite, myRect.rect.width);
            imageArea.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, myHeight);
        }

        public void SetToDeck(BossData data)
        {
            imageArea.sprite = data.GetBossDeckDetails();
            myHeight = GunDetails.SetImageSize(imageArea, imageArea.sprite, myRect.rect.width);
            imageArea.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, myHeight);
        }
    }
}
