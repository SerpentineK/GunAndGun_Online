using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DictionaryMenu
{
    public class GunPoolSwitcher : MonoBehaviour
    {
        [SerializeField] private Image logoArea;
        private GunShowcase correspondingShowcase = null;

        public void SetSwitcher(CardpoolDatabase database, GunShowcase showcase)
        {
            logoArea.sprite = database.Logo;
            correspondingShowcase = showcase;
        }

        public void ShowcaseContentTransition()
        {
            GunDictionary.Instance.SwitchShowcase(correspondingShowcase);
        }
    }
}