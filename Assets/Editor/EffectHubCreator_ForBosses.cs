using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class EffectHubCreator_ForBosses : EditorWindow
{
    // �{�X�t�H���_�������Ă���f�B���N�g���̃p�X
    private readonly string bossMasterDataPath = "Assets/Resources/BossCardData";

    // EffectHub�A�Z�b�g�̏o�̓p�X
    private readonly string outputPath = "Assets/Resources/EffectHubData/BossCards";

    // �A�Z�b�g�̊g���q
    private readonly string assetExtension = ".asset";

    private enum DataClassification
    {
        None,
        BossDeckCard,
        BossStage
    }

    // ���j���[�ɃA�C�e����ǉ�
    [MenuItem("Tools/Create EffectHubs For Bosses")]
    // �Ăяo���֐�
    private static void CreateHub_ForBosses()
    {
        EffectHubCreator_ForBosses window = GetWindow<EffectHubCreator_ForBosses>("Create EffectHub For Bosses");
        window.Show();
    }
    
    // �p�X����f�[�^��ǂݍ��݁AScriptableObject�Ƃ��ĕԂ��֐�
    // DataClassification��p���ăL���X�g���ύX���Ă���
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

    // EffectHub�̌��ƂȂ�f�[�^������Ă��āA�ϐ��Ɋi�[����֐�
    private Dictionary<DataClassification, ScriptableObject[]> GetDataForBoss(string bossTitle)
    {
        // �u��񌳂ƂȂ�I�u�W�F�N�g�v�Ɓu�e�X�̃I�u�W�F�N�g�̃p�X�v�A���ꂼ��̃��X�g
        List<BossCardData> bossDeckCardList = new();
        List<BossStageData> bossStageList = new();
        List<string> bossDeckCardPathList = new();
        List<string> bossStagePathList = new();

        // �{�X�f�b�L�J�[�h�ƃ{�X�X�e�[�W�̃t�H���_�p�X��ʁX�̕ϐ��Ɋi�[���Ă��炻�ꂼ��̒����ɑ��݂���.asset�t�@�C���̃p�X��List�Ɋi�[����
        string bossDirectoryPath = Path.Combine(bossMasterDataPath, bossTitle);

        string bossDeckCardDirectoryPath = Path.Combine(bossDirectoryPath, "DeckCards");
        string bossStageDirectoryPath = Path.Combine(bossDirectoryPath, "Stages");

        bossDeckCardPathList.AddRange(Directory.EnumerateFiles(bossDeckCardDirectoryPath, "*.asset"));
        bossStagePathList.AddRange(Directory.EnumerateFiles(bossStageDirectoryPath, "*.asset"));

        // bossDeckCardPathList�Ɋ܂܂��p�X�S�Ăɂ���ReadDataFromPath()�����s���A���̌��ʂ�bossDeckCardList�ɒǉ�����
        foreach (string sourcePath in bossDeckCardPathList)
        {
            BossCardData cardData = ReadDataFromPath(sourcePath, DataClassification.BossDeckCard) as BossCardData;
            bossDeckCardList.Add(cardData);
        }

        // bossStagePathList�ɂ��Ă����l�ɂ���
        foreach (string sourcePath in bossStagePathList)
        {
            BossStageData cardData = ReadDataFromPath(sourcePath, DataClassification.BossStage) as BossStageData;
            bossStageList.Add(cardData);
        }

        // �����^�z����쐬���Ԃ�l�ɂ���
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
                // �A�Z�b�g�쐬
                AssetDatabase.CreateAsset(newHub, finalPath);
                // �X�V�ʒm
                EditorUtility.SetDirty(newHub);
                // �ۑ�
                AssetDatabase.SaveAssets();
                // �G�f�B�^���ŐV�̏�Ԃɂ���
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
            // �A�Z�b�g�쐬
            AssetDatabase.CreateAsset(newHub, finalPath);
            // �X�V�ʒm
            EditorUtility.SetDirty(newHub);
            // �ۑ�
            AssetDatabase.SaveAssets();
            // �G�f�B�^���ŐV�̏�Ԃɂ���
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
