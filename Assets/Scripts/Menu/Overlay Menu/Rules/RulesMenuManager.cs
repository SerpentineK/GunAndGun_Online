using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static OverlayMenu;

namespace RulesMenu
{
    public class RulesMenuManager : MonoBehaviour
    {
        public static RulesMenuManager instance;

        [SerializeField] private GameObject[] _indexArray;

        [SerializeField] private GameObject[] _subIndexArray;

        private static GameObject[] indexArray;
        private static GameObject[] subIndexArray;

        [SerializeField] private Image contentRenderer;

        public static Image ContentRenderer { get; private set; }

        [SerializeField] private Button topIndexButton;
        [SerializeField] private Button topContentButton;

        private void Awake()
        {
            instance = this;

            indexArray = _indexArray;
            subIndexArray = _subIndexArray;

            ContentRenderer = contentRenderer;

            topIndexButton.onClick.Invoke();
            topContentButton.onClick.Invoke();
        }



        public static void SetSubIndex(GameObject nextSub)
        {
            foreach (var subIndex in subIndexArray)
            {
                ToggleGameObject(subIndex, false);
            }

            if (nextSub)
            {
                ToggleGameObject(nextSub, true);
            }

        }

        public static void HighlightSubIndex(ContentButton nextButton)
        {
            foreach (GameObject subIndex in subIndexArray)
            {
                ContentButton[] myButtonArray = subIndex.GetComponentsInChildren<ContentButton>();
                foreach (ContentButton button in myButtonArray)
                {
                    if (nextButton != button)
                    {
                        ToggleGameObject(button.Highlight, false);
                        button.Label.fontStyle = FontStyles.Normal;
                    }
                    else
                    {
                        ToggleGameObject(button.Highlight, true);
                        button.Label.fontStyle = FontStyles.Underline;
                    }
                }
            }
        }
        public static void HighlightIndex(IndexButton nextButton)
        {
            foreach (GameObject index in indexArray)
            {
                IndexButton button = index.GetComponent<IndexButton>();
                if (nextButton != button)
                {
                    ToggleGameObject(button.Highlight, false);
                    button.Label.fontStyle = FontStyles.Normal;
                }
                else
                {
                    ToggleGameObject(button.Highlight, true);
                    button.Label.fontStyle = FontStyles.Underline;
                }
            }
        }

        public static void SetContent(Sprite description)
        {
            ContentRenderer.sprite = description;
            ContentRenderer.preserveAspect = true;

            float myHeight = ContentRenderer.preferredHeight;
            float myWidth = ContentRenderer.preferredWidth;

            float ratio = 1000 / myWidth;
            RectTransform myRect = ContentRenderer.GetComponent<RectTransform>();
            myRect.sizeDelta = new Vector2(1000, myHeight * ratio);
            myRect.localPosition = new Vector2(0, 0);
        }


    }
}