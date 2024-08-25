using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunSelector : MonoBehaviour
{
    // 現在のゲームモード
    public GameManager.GAME_MODE gameMode;

    // 個々の機銃のCandidate
    public GunCandidate[] gunCandidates;

    // 使用する機銃のデータベースリスト
    public List<GunsDataBase> databases;

    // 使用する可能性のある機銃のデータリスト
    public List<GunsData> possibleDataList;

    // 使用すると決まった機銃のデータリスト
    public List<GunsData> finalDataList;

    // 現在ActiveなTargetと紐づいたCandidate
    public GunCandidate activeCandidate;

    // 機銃決定後、その情報を保管する変数
    public GunsData selectedGunData;

    // 選択不可能な機銃のリスト（自分や相手が選択済みの機銃）
    public List<GunsData> inavailableGunsDataList;

    // 選んだ銃士を表示する領域
    [SerializeField] private TMP_Text infoArea_gunner;

    public HorizontalLayoutGroup myLayoutGroup;
    public ContentSizeFitter mySizeFitter;

    private readonly GunsData.GunType[] gunTypes = { GunsData.GunType.LIGHT, GunsData.GunType.HEAVY, GunsData.GunType.SPECIAL};

    public void OrganizeGuns()
    {
        possibleDataList = new List<GunsData>();
        finalDataList = new List<GunsData>();

        // 一度全てのCandidateを非アクティブ化
        foreach (var item in gunCandidates)
        {
            if (item.gameObject.activeSelf) { item.gameObject.SetActive(false); }
        }

        // データベース群の一つ一つのリストをpossibleDataListに追加
        foreach (var item in databases)
        {
            possibleDataList.AddRange(item.GetGunsDataList());
        }

        // GamemodeがNORMALのとき(つまり、カードプールがBLOCKに限定されており使用される機銃がデータリストの全てであるとき)
        if (gameMode == GameManager.GAME_MODE.NORMAL)
        {
            finalDataList = possibleDataList;
            ActivateGunCandidates(finalDataList.Count);
        }
        // UNLIMITEDゲームモードのとき(つまり、ランダムに機銃8挺を選び出してそれを選択肢として提示するとき)
        else
        {
            foreach(var item in gunTypes)
            {
                var temporaryList = possibleDataList.FindAll(x => x.GetGunType() == item);
                for (int i = 0; i < 2; i++)
                {
                    int randomId = Random.Range(0, temporaryList.Count);
                    finalDataList.Add(temporaryList[randomId]);
                    temporaryList.RemoveAt(randomId);
                }
            }
            ActivateGunCandidates(finalDataList.Count);
        }
        GunCandidate firstGunCandidate = gunCandidates[0];
        firstGunCandidate.ActivateTarget();
        activeCandidate = firstGunCandidate;
    }

    // GunCandidateをセットアップする関数
    // (GunTargetはSetupGun関数で個々のGunCandidateに紐づいたものがセットアップされるためここでは触れない)
    public void ActivateGunCandidates(int num)
    {
        for(int i = 0; i < num; i++)
        {
            var candidate = gunCandidates[i];
            candidate.data = finalDataList[i];
            candidate.SetupGun();
            if (inavailableGunsDataList.Contains(candidate.data)) 
            {
                candidate.SetInavailableForCandidate();
                candidate.target.SetInavailableForTarget();
            }
        }
    }

}
