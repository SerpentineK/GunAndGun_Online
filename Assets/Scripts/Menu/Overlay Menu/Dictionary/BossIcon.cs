using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DictionaryMenu
{
    public class BossIcon : MonoBehaviour
    {
        public BossData MyData { get; private set; }

        // ボタンが複数ある（ボス詳細、ステージ詳細、ボスデッキ詳細）
        public Button DetailsButton { get; private set; }
        public Button StageButton { get; private set; }
        public Button DeckButton { get; private set; }

        [Header("Buttons")]
        [SerializeField] private Button detailsButton;
        [SerializeField] private Button stageButton;
        [SerializeField] private Button deckButton;

        [Header("Components")]
        [SerializeField] private Image imageComponent;
        [SerializeField] private Image shadowComponent;
        [SerializeField] private TMP_Text titleComponent;
        [SerializeField] private TMP_Text nameComponent;

        public void SetIconData(BossData data)
        {
            MyData = data;

            DetailsButton = detailsButton;
            StageButton = stageButton;
            DeckButton = deckButton;

            imageComponent.sprite = data.GetBossGraphics();
            shadowComponent.sprite = data.GetBossGraphics();
            titleComponent.SetText(data.GetBossTitle());
            nameComponent.SetText(data.GetBossName());

            name = data.GetBossTitle() + data.GetBossName();
        }

        public void SendBossDetails()
        {
            BossDictionary.DetailsDisplay.SetToDetails(MyData);
        }
        public void SendBossStages()
        {
            BossDictionary.DetailsDisplay.SetToStages(MyData);
        }
        public void SendBossDeck()
        {
            BossDictionary.DetailsDisplay.SetToDeck(MyData);
        }
    }
}