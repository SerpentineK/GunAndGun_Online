using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SettingsMenu
{
    public interface ISettingsMenu
    {
        // LocalSettings型のプロパティ
        public LocalSettings MyLocalSettings { get; set; }

        // LocalSettingsのデフォルト値を反映する関数
        void ReflectLocal();
    }
}