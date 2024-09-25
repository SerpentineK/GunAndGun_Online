using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InitialMenu
{
    public class BossCandidate : MonoBehaviour
    {
        public BossData MyData { get; private set; }

        [Header("Components")]
        [SerializeField] private Image backgroundArea;
        [SerializeField] private Image imageArea;
        [SerializeField] private TMP_Text titleArea;
        [SerializeField] private TMP_Text nameArea;

        [Header("Button Component")]
        public Button myButton;

        [HideInInspector] public InitialMenuState initialS;

        private readonly Color defaultBackground = new(154f / 255f, 154f / 255f, 154f / 255f);

        public void SetCandidateData(BossData data)
        {
            MyData = data;

            backgroundArea.color = defaultBackground;
            imageArea.sprite = data.GetBossGraphics();
            titleArea.SetText(data.GetBossTitle());
            nameArea.SetText(data.GetBossName());

            name = data.GetBossTitle() + "_" + data.GetBossName();
        }

        public void ToggleSelectionState(bool state)
        {
            if (state)
            {
                backgroundArea.color = MyData.GetBossThemeColor();
            }
            else
            {
                backgroundArea.color = defaultBackground;
            }
        }

        public void SelectThisCandidate()
        {
            initialS.SelectBossCandidate(this);
        }
    }
}