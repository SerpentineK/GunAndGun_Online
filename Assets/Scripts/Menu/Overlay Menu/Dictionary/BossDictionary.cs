using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OverlayMenu;

namespace DictionaryMenu
{
    public class BossDictionary : MonoBehaviour, IDictionaryMenu
    {
        public void Initialize()
        {

        }

        public void EnterDictMenu()
        {
            ToggleGameObject(gameObject, true);
        }
        public void ExitDictMenu()
        {
            ToggleGameObject(gameObject, true);
        }
    }
}