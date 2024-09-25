using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SettingsMenu
{
    public class SettingsMenuManager : MonoBehaviour
    {
        // LocalSettings�i�[�p�ϐ�
        private LocalSettings mySettings;

        // �e��ݒ��ʂɕR�Â��Ă���N���X
        [SerializeField] private SoundSettings mySoundSettings;
        [SerializeField] private ControlsSettings myControlsSettings;
        [SerializeField] private GraphicsSettings myGraphicsSettings;
        [SerializeField] private MiscellaneousSettings myMiscellaneousSettings;

        private bool initialized = false;

        public void InitializeSettingsMenus()
        {
            if (!initialized)
            {
                // Metaphysics����LocalSettings������Ă���
                Scene metaScene = SceneManager.GetSceneByName("Metaphysics");

                foreach (var rootObj in metaScene.GetRootGameObjects())
                {
                    if (rootObj.TryGetComponent<LocalSettings>(out var settings))
                    {
                        mySettings = settings;
                    }
                }

                // menus�Ɋe���j���[��o�^���A���ꂼ���mySettings�����ď����ݒ�𔽉f����
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