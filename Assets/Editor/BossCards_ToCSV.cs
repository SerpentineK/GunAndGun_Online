using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class BossCards_ToCSV : EditorWindow
{
    private readonly string inputPath = "Assets/Resources/BossCardData";
    private readonly string outputPath = "Assets/Resources/CSVs";

    private bool token = false;

    private int popupIndex;
    private string[] bossDirectoryArray;
    private string[] bossNameArray;
    private int[] bossIntArray;
    private StreamWriter sw;

    [MenuItem("Tools/Create Boss Card CSV")]
    // åƒÇ—èoÇ∑ä÷êî
    private static void CreateCsv()
    {
        BossCards_ToCSV window = GetWindow<BossCards_ToCSV>("Create Boss Card CSV");
        window.Show();
    }

    private void OnGUI()
    {
        if (!token)
        {
            bossDirectoryArray = Directory.GetDirectories(inputPath);
            bossNameArray = new string[bossDirectoryArray.Length];
            bossIntArray = new int[bossDirectoryArray.Length];
            for (int i = 0; i < bossDirectoryArray.Length; i++)
            {
                bossNameArray[i] = SimplifyDirectory(bossDirectoryArray[i]);
                bossIntArray[i] = i;
            }
            token = true;
        }
        using (new EditorGUILayout.VerticalScope())
        {
            EditorGUILayout.LabelField("Select the origin directory");
            popupIndex = EditorGUILayout.IntPopup(
                selectedValue: popupIndex,
                displayedOptions: bossNameArray,
                optionValues: bossIntArray
                );
            if (GUILayout.Button("Press to create csv file"))
            {
                string[][] myResult = GetFolderContents(bossNameArray[popupIndex]);
                sw = new StreamWriter(outputPath + "/" + bossNameArray[popupIndex] + "_BossCardData.csv", false, Encoding.GetEncoding("utf-8"));
                sw.WriteLine("ID,name,quantity,effect01,effect02,effect03");
                foreach (string[] card in myResult)
                {
                    sw.WriteLine(string.Join(",", card));
                }
                sw.Close();
                Debug.Log("Finished!");
            }
        }
    }

    private string SimplifyDirectory(string item)
    {
        string targetStr = item;
        string replaceStr = "Assets/Resources/";
        targetStr = targetStr.Replace(replaceStr, "");
        targetStr = targetStr.Substring(targetStr.IndexOf("\\") + 1);
        targetStr = targetStr.Replace("\\", "/");
        return targetStr;
    }

    private string[][] GetFolderContents(string folderName)
    {
        string path = "BossCardData/" + folderName + "/BossDeckCards";
        BossCardData[] dataArray = Resources.LoadAll<BossCardData>(path).ToArray();
        string[][] result = new string[dataArray.Length][];
        for (int i = 0; i < dataArray.Length; i++)
        {
            result[i] = new string[6];
        }
        int count = 0;
        foreach (BossCardData data in dataArray)
        {
            result[count][0] = data.GetCardId();
            result[count][1] = data.GetCardName();
            result[count][2] = string.Format("{0:00}",data.GetNumOfCards());
            result[count][3] = data.GetCardEffectTexts()[0].Replace("\n", "\\n");
            result[count][4] = data.GetCardEffectTexts()[1].Replace("\n", "\\n");
            result[count][5] = data.GetCardEffectTexts()[2].Replace("\n", "\\n");
            count++;
        }
        return result;
    }
}
