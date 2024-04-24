using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.Progress;





#if UNITY_EDITOR
using UnityEditor;
#endif

public class TestCardCreator : EditorWindow
{
    // �Q�Ƃ���X�N���v�g�̓��̓p�X
    private static string inputPath_Effects = "Assets/Scripts/Battle/Effects/EffectClasses";

    // Card�A�Z�b�g�̏o�̓p�X
    private static string outputPath = "Assets/Resources/EffectData/_TestCards";

    // �A�Z�b�g�̊g���q
    public static readonly string assetExtension = ".asset";

    // [MenuItem("Tools/Create Test Effects")]
    private static void Init()
    {
        TestCardCreator window = GetWindow<TestCardCreator>("Create Test Effects");
        window.minSize = new Vector2(320, 320);
        window.Show();
    }

    private string[] effectDirArray;
    private string[] effectPathArray;
    private List<string> effectNameList;

    // Effect�̖��O�ꗗ�̔z���Ԃ��֐�
    public string[] SetNameArray()
    {
        effectNameList = new List<string>();
        effectDirArray = Directory.GetDirectories(inputPath_Effects);
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
            token = true;
        }
        if (GUILayout.Button("Create Effect Hubs"))
        {
            foreach (var item in effectNameArray)
            {
                Export(item);
            }
        }
    }

    private void Export(string effectName)
    {
        effectName = effectName.Substring(effectName.IndexOf("/") + 1);
        fileName = "00_" + effectName + "_TestEffect";
        var effectObject = CreateInstance(effectName);
        string finalPath = outputPath + "/" + fileName + assetExtension;
        if (!File.Exists(finalPath))
        {
            // �A�Z�b�g�쐬
            AssetDatabase.CreateAsset(effectObject, finalPath);
            // �X�V�ʒm
            EditorUtility.SetDirty(effectObject);
            // �ۑ�
            AssetDatabase.SaveAssets();
            // �G�f�B�^���ŐV�̏�Ԃɂ���
            AssetDatabase.Refresh();
        }
        string hubPath = "EffectHubData/_TestCards/" + effectName + "_TestHub" ;
        EffectHub targetHub = Resources.Load(hubPath) as EffectHub;
        Effect targetEffect = Resources.Load(outputPath + "/" + fileName) as Effect;
        targetHub.effects.Add(targetEffect);
        Resources.UnloadAsset(targetHub);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
