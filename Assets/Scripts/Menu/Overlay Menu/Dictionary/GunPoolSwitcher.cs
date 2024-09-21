using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DictionaryMenu
{
    public class GunPoolSwitcher : MonoBehaviour
    {
        [SerializeField] private Image logoArea;
        [SerializeField] private GameObject selectionIndicator;

        private GunShowcase correspondingShowcase = null;

        private void Awake()
        {
            OverlayMenu.ToggleGameObject(selectionIndicator, false);
        }

        public void SetSwitcher(CardpoolDatabase database, GunShowcase showcase)
        {
            logoArea.sprite = database.Logo;
            correspondingShowcase = showcase;
        }

        public void OnMySelection()
        {
            GunDictionary.Instance.SignifySwitcherSelection(this);
            GunDictionary.Instance.SwitchShowcase(correspondingShowcase);
        }

        public void ToggleSelectionDisplay(bool state)
        {
            OverlayMenu.ToggleGameObject(selectionIndicator, state);
        }
    }
}