using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DictionaryMenu
{
    public class GunDetails : MonoBehaviour
    {
        [SerializeField] private Image imageArea;

        public void SetDetails(GunsData data)
        {
            imageArea.sprite = data.GunDetails;
            imageArea.preserveAspect = true;
        }
    }
}