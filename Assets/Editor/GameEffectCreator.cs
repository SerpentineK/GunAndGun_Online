using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System;
using Unity.VisualScripting;
using UnityEngine.WSA;




#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameEffectCreator : EditorWindow
{
    // 参照するスクリプトの入力パス
    private static string inputPath = "Assets/Scripts/Battle/Effects/EffectClasses";

    // 参照する対象(カード、銃士、機銃)の入力パス
    private static string inputPath_Card = "Assets/Resources/CardData";
    private static string inputPath_Gunner = "Assets/Resources/GunnerData";
    private static string inputPath_Gun = "Assets/Resources/GunsData";
    private static string inputPath_Skill = "Assets/Resources/SkillData";

    // Effectアセットの出力パス
    private static string outputPath = "Assets/Resources/EffectData";

    // アセットの拡張子
    public static readonly string assetExtension = ".asset";

    [MenuItem("Tools/Create Effect")]
    private static void Init()
    {
        GameEffectCreator window = GetWindow<GameEffectCreator> ("Create Effect");
        window.minSize = new Vector2(320, 320);
        window.Show();
    }

    private string[] effectDirArray;
    private string[] effectPathArray;
    private List<string> effectNameList;

    // Effectの名前一覧の配列を返す関数
    public string[] SetNameArray()
    {
        effectNameList = new List<string>();
        effectDirArray = Directory.GetDirectories(inputPath);
        foreach (string directory in effectDirArray)
        {
            effectPathArray = Directory.GetFiles(directory);
            foreach (var item in effectPathArray)
            {
                string targetStr = item;
                string removeStr = "Assets/Scripts/Battle/Effects/EffectClasses\\";
                targetStr = targetStr.Replace(removeStr, "");
                targetStr = targetStr.Remove(targetStr.IndexOf(".cs"));
                targetStr = targetStr.Replace("\\", "/");
                effectNameList.Add(targetStr);
            }
        }
        return effectNameList.ToArray();
    }

    // EffectHub用の関数01
    public string[] SetPartialNameArray_ForHub(string inputPath)
    {
        string[] sourceDirectoryPathArray = Directory.GetDirectories(inputPath);
        List<string> sourcePathList = new List<string>();
        List<string> sourceNameList = new List<string>();

        foreach (string sourceDirPath in sourceDirectoryPathArray)
        {
            sourcePathList.AddRange(Directory.GetFiles(sourceDirPath));
        }

        foreach (var item in sourcePathList)
        {
            string targetStr = item;
            string replaceStr = "Assets/Resources/";
            targetStr = targetStr.Replace(replaceStr, "");
            targetStr = targetStr.Remove(targetStr.IndexOf("."));
            targetStr = targetStr.Substring(targetStr.IndexOf("\\") + 1);
            targetStr = targetStr.Replace("\\", "/");
            sourceNameList.Add(targetStr);
        }
        return sourceNameList.ToArray();
    }

    // EffectHub用の関数02
    public string[] SetFullNameArray_ForHub()
    {
        string[][] array =
        {
            SetPartialNameArray_ForHub(inputPath_Card),
            SetPartialNameArray_ForHub(inputPath_Gunner),
            SetPartialNameArray_ForHub(inputPath_Gun),
            SetPartialNameArray_ForHub(inputPath_Skill)
        };
        int len = array[0].Length + array[1].Length + array[2].Length + array[3].Length;
        string[] fullArray = new string[len];
        int childCounter = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int index = 0; index < array[i].Length; index++)
            {
                string child = array[i][index];
                fullArray[childCounter] = child;
                childCounter++;
            }
        }
        return fullArray;
    }

    private int popupIndex_ForClass;
    private int popupIndex_ForHub;
    private string[] effectNameArray;
    private string[] hubNameArray;
    private int[] effectIntArray;
    private int[] hubIntArray;
    private string fileName = "";
    private bool token = false;


    private void OnGUI()
    {
        if (!token)
        {
            effectNameArray = SetNameArray();
            effectIntArray = new int[effectNameArray.Length];
            for(int i = 0; i < effectNameArray.Length; i++)
            {
                effectIntArray[i] = i;
            }
            hubNameArray = SetFullNameArray_ForHub();
            hubIntArray = new int[hubNameArray.Length];
            for (int i = 0; i < hubNameArray.Length; i++)
            {
                hubIntArray[i] = i;
            }
            token = true;
        }
        using (new GUILayout.VerticalScope())
        {
            EditorGUILayout.LabelField("Select the Hub this Effect is for");
            popupIndex_ForHub = EditorGUILayout.IntPopup(
                selectedValue: popupIndex_ForHub,
                displayedOptions: hubNameArray,
                optionValues: hubIntArray
                );
            EditorGUILayout.LabelField("Select Class of Instance");
            popupIndex_ForClass = EditorGUILayout.IntPopup(
                selectedValue: popupIndex_ForClass,
                displayedOptions: effectNameArray,
                optionValues:effectIntArray
                );
            EditorGUILayout.LabelField("File Name");
            fileName = EditorGUILayout.TextArea(text: fileName);
        }
        if (GUILayout.Button("Create Effect"))
        {
            Export(popupIndex_ForHub, popupIndex_ForClass, fileName);
        }
    }

    private void Export(int hubIndex, int classIndex, string name)
    {
        string effectName = effectNameArray[classIndex];
        effectName = effectName.Substring(effectName.IndexOf("/") + 1);
        string hubName = hubNameArray[hubIndex];
        string effectParent = hubName.Remove(hubName.IndexOf("/"));
        hubName = hubName.Substring(hubName.IndexOf("/") + 1);
        var effectObject = CreateInstance(effectName);
        string finalPath = outputPath + "/" + effectParent + "/" + hubName + "/" + name + assetExtension;
        if (!Directory.Exists(outputPath + "/" + effectParent))
        {
            Directory.CreateDirectory(outputPath + "/" + effectParent);
        }
        if (!Directory.Exists(outputPath + "/" + effectParent + "/" + hubName))
        {
            Directory.CreateDirectory(outputPath + "/" + effectParent + "/" + hubName);
        }
        if (!File.Exists(finalPath)) 
        {
            // アセット作成
            AssetDatabase.CreateAsset(effectObject, finalPath);
            // 更新通知
            EditorUtility.SetDirty(effectObject);
            // 保存
            AssetDatabase.SaveAssets();
            // エディタを最新の状態にする
            AssetDatabase.Refresh();
        }
    }
}
