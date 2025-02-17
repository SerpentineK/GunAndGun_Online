using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SettingsMenu
{
    public class SettingsMenuManager : MonoBehaviour
    {
        // LocalSettings格納用変数
        private LocalSettings mySettings;

        // 各種設定画面に紐づいているクラス
        [SerializeField] private SoundSettings mySoundSettings;
        [SerializeField] private ControlsSettings myControlsSettings;
        [SerializeField] private GraphicsSettings myGraphicsSettings;
        [SerializeField] private MiscellaneousSettings myMiscellaneousSettings;

        private bool initialized = false;

        public void InitializeSettingsMenus()
        {
            if (!initialized)
            {
                // MetaphysicsからLocalSettingsを取ってくる
                Scene metaScene = SceneManager.GetSceneByName("Metaphysics");

                foreach (var rootObj in metaScene.GetRootGameObjects())
                {
                    if (rootObj.TryGetComponent<LocalSettings>(out var settings))
                    {
                        mySettings = settings;
                    }
                }

                // menusに各メニューを登録し、それぞれにmySettingsを入れて初期設定を反映する
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