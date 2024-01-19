using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EffectHubCreator : EditorWindow
{
    // 参照する対象(カード、銃士、機銃、技能)の入力パス 技能は銃士パス内に存在する
    private static readonly string inputPath_Card = "Assets/Resources/CardData";
    private static readonly string inputPath_Gunner = "Assets/Resources/GunnerData";
    private static readonly string inputPath_Gun = "Assets/Resources/GunsData";
    private static readonly string inputPath_Skill = "Assets/Resources/SkillData";

    // EffectHubアセットの出力パス
    private static readonly string outputPath = "Assets/Resources/EffectHubData";

    // アセットの拡張子
    public static readonly string assetExtension = ".asset";

    public enum DataClassification 
    {
        None,
        Card,
        Gunner,
        Gun,
        Skill
    }

    // メニューにアイテムを追加
    [MenuItem("Tools/Create EffectHub")]
    // 呼び出す関数
    private static void CreateHub()
    {
        EffectHubCreator window = GetWindow<EffectHubCreator>("Create EffectHub");
        window.Show();
    }

    // 作成されたHubと紐づけるためのDataを取ってくる
    public ScriptableObject[] GetPartialDataFileArray(string inputPath, DataClassification classification)
    {
        IEnumerable<string> sourceDirectoryPathArray = Directory.EnumerateDirectories(inputPath);
        List<string> sourcePathList = new();
        List<ScriptableObject> objectList = new();

        foreach (string sourceDirPath in sourceDirectoryPathArray)
        {
            sourcePathList.AddRange(Directory.EnumerateFiles(sourceDirPath, "*.asset"));
        }

        foreach (var item in sourcePathList)
        {
            string replaceStr = "Assets/Resources/";
            string path = item.Replace(replaceStr, "");
            path = path.Remove(path.IndexOf("."));
            path = path.Replace("\\", "/");
            Debug.Log(path);
            if(classification == DataClassification.Card)
            {
                CardData target_asCard = Resources.Load(path) as CardData;
                objectList.Add(target_asCard);
            }
            else if (classification == DataClassification.Gunner)
            {
                GunnerData target_asGunner = Resources.Load(path) as GunnerData;
                objectList.Add(target_asGunner);
            }
            else if (classification == DataClassification.Gun) 
            {
                GunsData target_asGun = Resources.Load(path) as GunsData;
                objectList.Add(target_asGun);
            }
            else if (classification == DataClassification.Skill)
            {
                SkillData target_asSkill = Resources.Load(path) as SkillData;
                objectList.Add(target_asSkill);
            }
        }
        return objectList.ToArray();
    }


    private void OnGUI()
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Press to recreate EffectHubs"))
            {
                // PutUnusedFilesToBin();
                // ExportCardsFromData();
                // ExportGunnersFromData();
                // ExportGunsFromData();
                // ExportSkillsFromData();
            }
        }
    }

    public void ExportCardsFromData()
    {
        ScriptableObject[] dataFileArray = GetPartialDataFileArray(inputPath_Card, DataClassification.Card);

        string directory;
        string hubName;
        string effectText;
        CardData.CardType cardType;

        for (int i = 0;i < dataFileArray.Length; i++)
        {
            CardData data = dataFileArray[i] as CardData;
            GunsData attached = data.GetAttachedGunData();

            directory = "Assets/Resources/EffectHubData/" + attached.name + "Cards";
            hubName = data.name + "_EffectHub";
            effectText = data.GetCardEffectText();
            cardType = data.GetCardType();

            EffectHub hubObject = CreateInstance<EffectHub>();
            hubObject.effectContentText = effectText;
            hubObject.cardType = cardType;
            hubObject.name = hubName;
            hubObject.attachedData = data;
            string finalPath = directory + "/" + hubName + assetExtension;

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(finalPath))
            {
                // アセット作成
                AssetDatabase.CreateAsset(hubObject, finalPath);
                // 更新通知
                EditorUtility.SetDirty(hubObject);
                // 保存
                AssetDatabase.SaveAssets();
                // エディタを最新の状態にする
                AssetDatabase.Refresh();
            }
        }

    }

    public void ExportGunnersFromData()
    {
        ScriptableObject[] dataFileArray = GetPartialDataFileArray(inputPath_Gunner, DataClassification.Gunner);

        string directory = "Assets/Resources/EffectHubData/Gunners";
        string hubName;
        string effectText;

        for (int i = 0; i < dataFileArray.Length; i++)
        {
            GunnerData data = dataFileArray[i] as GunnerData;

            hubName = data.name+"_EffectHub";
            effectText = data.GetGunnerAbility();

            EffectHub hubObject = CreateInstance<EffectHub>();
            hubObject.effectContentText = effectText;
            hubObject.name = hubName;
            hubObject.cardType = CardData.CardType.Other;
            hubObject.attachedData = data;
            string finalPath = directory + "/" + hubName + assetExtension;
            
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(finalPath))
            {
                // アセット作成
                AssetDatabase.CreateAsset(hubObject, finalPath);
                // 更新通知
                EditorUtility.SetDirty(hubObject);
                // 保存
                AssetDatabase.SaveAssets();
                // エディタを最新の状態にする
                AssetDatabase.Refresh();
            }
        }
    }

    public void ExportGunsFromData()
    {
        ScriptableObject[] dataFileArray = GetPartialDataFileArray(inputPath_Gun, DataClassification.Gun);

        string directory = "Assets/Resources/EffectHubData/Guns";
        string hubName;
        string effectText;

        for (int i = 0; i < dataFileArray.Length; i++)
        {
            GunsData data = dataFileArray[i] as GunsData;

            hubName = data.name + "_EffectHub";
            effectText = data.GetGunAbility();

            EffectHub hubObject = CreateInstance<EffectHub>();
            hubObject.effectContentText = effectText;
            hubObject.name = hubName;
            hubObject.cardType = CardData.CardType.Other;
            hubObject.attachedData = data;
            string finalPath = directory + "/" + hubName + assetExtension;

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(finalPath))
            {
                // アセット作成
                AssetDatabase.CreateAsset(hubObject, finalPath);
                // 更新通知
                EditorUtility.SetDirty(hubObject);
                // 保存
                AssetDatabase.SaveAssets();
                // エディタを最新の状態にする
                AssetDatabase.Refresh();
            }
        }
    }

    public void ExportSkillsFromData()
    {
        ScriptableObject[] dataFileArray = GetPartialDataFileArray(inputPath_Skill, DataClassification.Skill);

        string directory = "Assets/Resources/EffectHubData/Skills";
        string hubName;
        string effectText;

        for (int i = 0; i < dataFileArray.Length; i++)
        {
            SkillData data = dataFileArray[i] as SkillData;

            hubName = data.name + "_EffectHub";
            effectText = data.GetSkillEffectText();

            EffectHub hubObject = CreateInstance<EffectHub>();
            hubObject.effectContentText = effectText;
            hubObject.name = hubName;
            hubObject.cardType = CardData.CardType.Other;
            hubObject.attachedData = data;
            string finalPath = directory + "/" + hubName + assetExtension;

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(finalPath))
            {
                // アセット作成
                AssetDatabase.CreateAsset(hubObject, finalPath);
                // 更新通知
                EditorUtility.SetDirty(hubObject);
                // 保存
                AssetDatabase.SaveAssets();
                // エディタを最新の状態にする
                AssetDatabase.Refresh();
            }
        }
    }

    public void PutUnusedFilesToBin()
    {
        IEnumerable<string> hubPathEnum = Directory.EnumerateFiles(outputPath, "*EffectHub*", SearchOption.AllDirectories);

        string binDirectory = "Assets/Resources/EffectHubData/_bin";
        if (!Directory.Exists(binDirectory))
        {
            Directory.CreateDirectory(binDirectory);
        }
        IEnumerable<string> binPathEnum = Directory.EnumerateFiles(binDirectory, "*EffectHub*", SearchOption.AllDirectories);

        IEnumerable<string> unusedPathEnum = hubPathEnum.Except(binPathEnum);
        IEnumerable<string> binDirectoryEnum = Directory.EnumerateDirectories(binDirectory,"*",SearchOption.TopDirectoryOnly);

        string removeStr = "Assets/Resources/";
        string finalPath;
        string finalDirectory;
        string hubPath_Simplified;
        string hubPath_forResources;
        EffectHub effectHub;


        foreach (var binItemPath in binPathEnum)
        {
            File.Delete(binItemPath);
        }

        foreach(string directory in binDirectoryEnum)
        {
            Directory.Delete(directory);
        }

        foreach (string hubPath in unusedPathEnum)
        {
            hubPath_Simplified = hubPath.Replace("\\","/");
            hubPath_forResources = hubPath_Simplified.Remove(hubPath_Simplified.IndexOf(".asset")).Replace(removeStr, "");
            effectHub = Resources.Load<EffectHub>(hubPath_forResources);
            if (!effectHub.finishedInput)
            {
                string parentDirName = hubPath_Simplified.Replace(outputPath + "/", "");
                string childFileName = hubPath_Simplified.Substring(hubPath_Simplified.LastIndexOf("/") + 1);
                parentDirName = parentDirName.Remove(parentDirName.LastIndexOf("/"));
                finalDirectory = binDirectory + "/" + parentDirName;
                if (!Directory.Exists(finalDirectory)) 
                {
                    Directory.CreateDirectory(finalDirectory);
                }
                finalPath = finalDirectory + "/" + childFileName;
                File.Move(hubPath, finalPath);
            }
        }
    }

}
