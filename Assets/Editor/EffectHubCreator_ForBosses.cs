using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class EffectHubCreator_ForBosses : EditorWindow
{
    // ボスフォルダが入っているディレクトリのパス
    private readonly string bossMasterDataPath = "Assets/Resources/BossCardData";

    // EffectHubアセットの出力パス
    private readonly string outputPath = "Assets/Resources/EffectHubData/BossCards";

    // アセットの拡張子
    private readonly string assetExtension = ".asset";

    private enum DataClassification
    {
        None,
        BossDeckCard,
        BossStage
    }

    // メニューにアイテムを追加
    [MenuItem("Tools/Create EffectHubs For Bosses")]
    // 呼び出す関数
    private static void CreateHub_ForBosses()
    {
        EffectHubCreator_ForBosses window = GetWindow<EffectHubCreator_ForBosses>("Create EffectHub For Bosses");
        window.Show();
    }
    
    // パスからデータを読み込み、ScriptableObjectとして返す関数
    // DataClassificationを用いてキャスト先を変更している
    private ScriptableObject ReadDataFromPath(string path, DataClassification classification) 
    {
        string replaceStr = "Assets/Resources/";
        string revisedPath = path.Replace(replaceStr, "");
        revisedPath = revisedPath.Remove(revisedPath.IndexOf("."));
        revisedPath = revisedPath.Replace("\\", "/");
        ScriptableObject data = null;
        switch (classification)
        {
            case DataClassification.None:
                break;
            case DataClassification.BossDeckCard:
                data = Resources.Load<BossCardData>(revisedPath);
                break;
            case DataClassification.BossStage:
                data = Resources.Load<BossStageData>(revisedPath);
                break;
            default:
                break;
        }
        return data;
    }

    // EffectHubの元となるデータを取ってきて、変数に格納する関数
    private Dictionary<DataClassification, ScriptableObject[]> GetDataForBoss(string bossTitle)
    {
        // 「情報元となるオブジェクト」と「各々のオブジェクトのパス」、それぞれのリスト
        List<BossCardData> bossDeckCardList = new();
        List<BossStageData> bossStageList = new();
        List<string> bossDeckCardPathList = new();
        List<string> bossStagePathList = new();

        // ボスデッキカードとボスステージのフォルダパスを別々の変数に格納してからそれぞれの直下に存在する.assetファイルのパスをListに格納する
        string bossDirectoryPath = Path.Combine(bossMasterDataPath, bossTitle);

        string bossDeckCardDirectoryPath = Path.Combine(bossDirectoryPath, "DeckCards");
        string bossStageDirectoryPath = Path.Combine(bossDirectoryPath, "Stages");

        bossDeckCardPathList.AddRange(Directory.EnumerateFiles(bossDeckCardDirectoryPath, "*.asset"));
        bossStagePathList.AddRange(Directory.EnumerateFiles(bossStageDirectoryPath, "*.asset"));

        // bossDeckCardPathListに含まれるパス全てについてReadDataFromPath()を実行し、その結果をbossDeckCardListに追加する
        foreach (string sourcePath in bossDeckCardPathList)
        {
            BossCardData cardData = ReadDataFromPath(sourcePath, DataClassification.BossDeckCard) as BossCardData;
            bossDeckCardList.Add(cardData);
        }

        // bossStagePathListについても同様にする
        foreach (string sourcePath in bossStagePathList)
        {
            BossStageData cardData = ReadDataFromPath(sourcePath, DataClassification.BossStage) as BossStageData;
            bossStageList.Add(cardData);
        }

        // 辞書型配列を作成し返り値にする
        Dictionary<DataClassification, ScriptableObject[]> dict = new()
        {
            { DataClassification.BossDeckCard, bossDeckCardList.ToArray() },
            { DataClassification.BossStage, bossStageList.ToArray() }
        };
        return dict;
    }

    private void CreateBossDeckCardHubs(BossCardData cardData, string directory)
    {
        string cardName = cardData.GetCardNameENG();
        string[] effectTexts = cardData.GetCardEffectTexts();

        EffectHub newHub;

        for (int i = 0; i < effectTexts.Length; i++)
        {
            newHub = CreateInstance<EffectHub>();
            int level = i + 1;
            string hubName = cardName + "_LV" + level.ToString();
            newHub.name = hubName;
            string effectText = effectTexts[i];
            newHub.effectContentText = effectText;

            string finalPath = Path.Combine(directory, cardName, hubName) + assetExtension;

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(finalPath))
            {
                // アセット作成
                AssetDatabase.CreateAsset(newHub, finalPath);
                // 更新通知
                EditorUtility.SetDirty(newHub);
                // 保存
                AssetDatabase.SaveAssets();
                // エディタを最新の状態にする
                AssetDatabase.Refresh();
            }
        }
    }

    private void CreateBossStageHub(BossStageData stageData, string directory)
    {
        string stageName = stageData.name;
        string effectText = stageData.GetStageAbility();

        EffectHub newHub = CreateInstance<EffectHub>();
        newHub.name = stageName;
        newHub.effectContentText = effectText;

        string finalPath = Path.Combine(directory, stageName) + assetExtension;

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (!File.Exists(finalPath))
        {
            // アセット作成
            AssetDatabase.CreateAsset(newHub, finalPath);
            // 更新通知
            EditorUtility.SetDirty(newHub);
            // 保存
            AssetDatabase.SaveAssets();
            // エディタを最新の状態にする
            AssetDatabase.Refresh();
        }
    }

    private void ExportHubsFromData(string bossTitle)
    {
        var dataDict = GetDataForBoss(bossTitle);
        BossCardData[] deckCardsArray = dataDict[DataClassification.BossDeckCard] as BossCardData[];
        BossStageData[] stageArray = dataDict[DataClassification.BossStage] as BossStageData[];

        string bossDirectory = Path.Combine(outputPath, bossTitle);

        string deckCardDirectory = Path.Combine(bossDirectory, "DeckCards");
        string stageDirectory = Path.Combine(bossDirectory, "Stages");

        foreach (var deckCard in deckCardsArray)
        {
            CreateBossDeckCardHubs(deckCard, deckCardDirectory);
        }

        foreach (var stage in stageArray)
        {
            CreateBossStageHub(stage, stageDirectory);
        }
    }

    private void OnGUI()
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Press to create Boss EffectHubs"))
            {
                // ExportHubsFromData("SuperIdol_Ran");
            }
        }
    }
}
