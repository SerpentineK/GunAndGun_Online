using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SettingsMenu
{
    public class SettingsMenuManager : MonoBehaviour
    {
        // LocalSettingsŠi”[—p•Ï”
        private LocalSettings mySettings;

        // Šeíİ’è‰æ–Ê‚É•R‚Ã‚¢‚Ä‚¢‚éƒNƒ‰ƒX
        [SerializeField] private SoundSettings mySoundSettings;
        [SerializeField] private ControlsSettings myControlsSettings;
        [SerializeField] private GraphicsSettings myGraphicsSettings;
        [SerializeField] private MiscellaneousSettings myMiscellaneousSettings;

        private bool initialized = false;

        public void InitializeSettingsMenus()
        {
            if (!initialized)
            {
                // Metaphysics‚©‚çLocalSettings‚ğæ‚Á‚Ä‚­‚é
                Scene metaScene = SceneManager.GetSceneByName("Metaphysics");

                foreach (var rootObj in metaScene.GetRootGameObjects())
                {
                    if (rootObj.TryGetComponent<LocalSettings>(out var settings))
                    {
                        mySettings = settings;
                    }
                }

                // menus‚ÉŠeƒƒjƒ…[‚ğ“o˜^‚µA‚»‚ê‚¼‚ê‚ÉmySettings‚ğ“ü‚ê‚Ä‰Šúİ’è‚ğ”½‰f‚·‚é
                ISettingsMenu[] menus = new ISettingsMenu[] 
                {
                    mySoundSettings,
                    myControlsSettings,
                    myGraphicsSettings,
                    myMiscellaneousSettings
                };

                foreach (var menu in menus)
                {
                    menu.MyLocalSettings = mySettings;
                    menu.ReflectLocal();
                }

                initialized = true;
            }
        }
    }
}