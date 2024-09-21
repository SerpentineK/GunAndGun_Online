using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static OverlayMenu;


namespace DictionaryMenu
{
    public class DictionaryManager : MonoBehaviour
    {
        [Header("Databases")]
        [SerializeField] private CardpoolDatabase[] _cardpools;

        public static CardpoolDatabase[] Cardpools { get; private set; }

        [Header("Dictionaries")]
        [SerializeField] private GunnerDictionary gunnerDictionary;
        [SerializeField] private GunDictionary gunDictionary;
        [SerializeField] private BossDictionary bossDictionary;

        [Header("Initial Category Button")]
        [SerializeField] private Button topCategoryButton;

        private IDictionaryMenu currentDictionary = null;
        private GameObject currentHighlight = null;
        private TMP_Text currentLabel = null;

        private bool initialized = false;

        public void Awake()
        {
            Cardpools = _cardpools;
        }

        public void InitializeDictionaries()
        {
            if (!initialized)
            {
                TeaseGameObject(gunnerDictionary.gameObject);
                TeaseGameObject(gunDictionary.gameObject);
                TeaseGameObject(bossDictionary.gameObject);
                gunnerDictionary.Initialize();
                gunDictionary.Initialize();
                bossDictionary.Initialize();
                topCategoryButton.onClick.Invoke();
                initialized = true;
            }
        }

        public void ChangeDictionary(IDictionaryMenu nextDictionary)
        {
            if (currentDictionary == null)
            {
                nextDictionary.EnterDictMenu();
                currentDictionary = nextDictionary;
            }
            else if (nextDictionary == null) 
            {
                return;
            }
            else if (currentDictionary != nextDictionary)
            {
                currentDictionary.ExitDictMenu();
                nextDictionary.EnterDictMenu();
                currentDictionary = nextDictionary;
            }
        }

        public void ChangeToGunner()
        {
            ChangeDictionary(gunnerDictionary);
        }

        public void ChangeToGun()
        {
            ChangeDictionary(gunDictionary);
        }

        public void ChangeToBoss()
        {
            ChangeDictionary(bossDictionary);
        }

        public void ChangeHighlight(GameObject nextHighlight)
        {
            if (currentHighlight == null)
            {
                ToggleGameObject(nextHighlight, true);
                currentHighlight = nextHighlight;
            }
            else if (nextHighlight == null)
            {
                return;
            }
            else if (currentHighlight != nextHighlight)
            {
                ToggleGameObject(currentHighlight, false);
                ToggleGameObject(nextHighlight, true);
                currentHighlight = nextHighlight;
            }
        }

        public void ChangeUnderline(TMP_Text nextLabel)
        {
            if (currentLabel == null)
            {
                nextLabel.fontStyle = FontStyles.Underline;
                currentLabel = nextLabel;
            }
            else if (nextLabel == null)
            {
                return;
            }
            else if (currentLabel != nextLabel)
            {
                currentLabel.fontStyle = FontStyles.Normal;
                nextLabel.fontStyle = FontStyles.Underline;
                currentLabel = nextLabel;
            }
        }
    }
}