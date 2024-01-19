using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EffectHubParentLocator : EditorWindow
{
    private static readonly string inputPath_Hub = "Assets/Resources/EffectHubData";


    [MenuItem("Tools/Associate Hub With Data")]
    // 呼び出す関数
    private static void LocateParent()
    {
        EffectHubParentLocator window = GetWindow<EffectHubParentLocator>("Associate With Parent");
        window.Show();
    }

    private void OnGUI()
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Press to associate"))
            {
                AssociateDataWithHub();
            }
        }
    }

    public EffectHub[] GetEffectHubFileArray(string inputPath)
    {
        IEnumerable<string> sourceDirectoryPathArray = Directory.EnumerateDirectories(inputPath);
        List<string> sourcePathList = new();
        List<EffectHub> objectList = new();

        foreach (string sourceDirPath in sourceDirectoryPathArray)
        {
            Debug.Log(sourceDirPath);
            if (sourceDirPath != "Assets/Resources/EffectHubData\\_Databases")
            {
                sourcePathList.AddRange(Directory.EnumerateFiles(sourceDirPath, "*.asset"));
            }
        }

        foreach (var item in sourcePathList)
        {
            string replaceStr = "Assets/Resources/";
            string path = item.Replace(replaceStr, "");
            path = path.Remove(path.IndexOf("."));
            path = path.Replace("\\", "/");
            EffectHub target_asHub = Resources.Load(path) as EffectHub;
            objectList.Add(target_asHub);
        }
        return objectList.ToArray();
    }

    public void AssociateDataWithHub()
    {
        var hubArray = GetEffectHubFileArray(inputPath_Hub);
        foreach (var hub in hubArray)
        {
            var data = hub.attachedData;
            CardData data_asCard = data as CardData;
            GunnerData data_asGunner = data as GunnerData;
            GunsData data_asGun = data as GunsData;
            SkillData data_asSkill = data as SkillData;
            if (data_asCard != null)
            {
                data_asCard.effectHub = hub;
            }
            else if (data_asGunner != null)
            {
                data_asGunner.effectHub = hub;
            }
            else if (data_asGun != null)
            {
                data_asGun.effectHub = hub;
            }
            else if (data_asSkill != null)
            {
                data_asSkill.effectHub = hub;
            }
        }
        // 保存
        AssetDatabase.SaveAssets();
        // エディタを最新の状態にする
        AssetDatabase.Refresh();
    }
}
