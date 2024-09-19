using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class DeckCards_ToCSV : EditorWindow
{
    // デッキカードのデータへのパス
    private readonly string inputPath = "Assets/Resources/CardData";
    private readonly string outputPath = "Assets/Resources/CSVs";

    private string[] cardDirectoryArray;

    private bool token = false;

    private int popupIndex;
    private int[] cardIntArray;
    string[] simplifiedArray;
    private StreamWriter sw;

    [MenuItem("Tools/Create Deck Card CSV")]
    // 呼び出す関数
    private static void CreateCsv()
    {
        DeckCards_ToCSV window = GetWindow<DeckCards_ToCSV>("Create Deck Card CSV");
        window.Show();
    }

    private void OnGUI()
    {
        if (!token)
        {
            cardDirectoryArray = Directory.GetDirectories(inputPath);
            simplifiedArray = new string[cardDirectoryArray.Length];
            cardIntArray = new int[cardDirectoryArray.Length];
            for (int i = 0; i < cardDirectoryArray.Length; i++) 
            { 
                simplifiedArray[i] = SimplifyDirectory(cardDirectoryArray[i]); 
                cardIntArray[i] = i; 
            }
            token = true;
        }
        using (new EditorGUILayout.VerticalScope())
        {
            EditorGUILayout.LabelField("Select the origin directory");
            popupIndex = EditorGUILayout.IntPopup(
                selectedValue: popupIndex,
                displayedOptions: simplifiedArray,
                optionValues: cardIntArray
                );
            if (GUILayout.Button("Press to create csv file"))
            {
                string[][] myResult = GetFolderContents(cardDirectoryArray[popupIndex].Replace("Assets/Resources/", "").Replace("\\","/"));
                sw = new StreamWriter(outputPath +"/"+ simplifiedArray[popupIndex] + "_CardData.csv", false, Encoding.GetEncoding("utf-8"));
                sw.WriteLine("ID,name,type,quantity,cost,effect");
                foreach (string[] card in myResult)
                {
                    sw.WriteLine(string.Join(",", card));
                }
                sw.Close();
                Debug.Log("Finished!");
            }
            if(GUILayout.Button("Press to mass produce files"))
            {
                for (int i = 0; i < cardDirectoryArray.Length; i++)
                {
                    string[][] myResult = GetFolderContents(cardDirectoryArray[i].Replace("Assets/Resources/", "").Replace("\\", "/"));
                    sw = new StreamWriter(outputPath + "/" + simplifiedArray[i] + "_CardData.csv", false, Encoding.GetEncoding("utf-8"));
                    sw.WriteLine("ID,name,type,quantity,cost,effect");
                    foreach (string[] card in myResult)
                    {
                        sw.WriteLine(string.Join(",", card));
                    }
                    sw.Close();
                }
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

    private string[][] GetFolderContents(string path)
    {
        CardData[] dataArray = Resources.LoadAll<CardData>(path).ToArray();
        string[][] result = new string[dataArray.Length][];
        for(int i = 0; i < dataArray.Length; i++)
        {
            result[i] = new string[6];
        }
        int count = 0;
        foreach (CardData data in dataArray)
        {
            result[count][0] = data.GetCardId();
            result[count][1] = data.GetCardName();
            result[count][2] = data.GetCardType().ToString();
            result[count][3] = data.GetNumberOfCards().ToString();
            result[count][4] = data.GetCardCost().ToString();
            result[count][5] = data.GetCardEffectText().Replace("\n", "\\n");
            count++;
        }
        return result;
    }
}
