using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SettingsMenu
{
    public interface ISettingsMenu
    {
        // LocalSettings�^�̃v���p�e�B
        public LocalSettings MyLocalSettings { get; set; }

        // LocalSettings�̃f�t�H���g�l�𔽉f����֐�
        void ReflectLocal();
    }
}