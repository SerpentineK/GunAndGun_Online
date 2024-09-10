using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DictionaryMenu
{
    public class DictionaryManager : MonoBehaviour
    {
        [Header("databases")]
        [SerializeField] private GunnerDataBase[] gunnerDatabaseArray;
        [SerializeField] private GunsDataBase[] gunsDatabaseArray;

        public static GunnerDataBase[] GunnerDataBases { get; private set; }
        public static GunsDataBase[] GunsDataBases { get; private set; }

        [Header("Dictionaries")]
        [SerializeField] private GunnerDictionary gunnerDictionary;

        public void Awake()
        {
            GunnerDataBases = gunnerDatabaseArray;
            GunsDataBases = gunsDatabaseArray;
            GunnerDictionary.instance = gunnerDictionary;
            gunnerDictionary.ActivateIcons();
        }

    }
}