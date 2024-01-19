using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GunnerSelector : MonoBehaviour
{
    // 現在のゲームモード
    public GameManager.GAME_MODE gameMode;

    // 個々の銃士のSelectable
    public GunnerSelectable[] gunnerSelectables;

    // 使用する銃士のデータベースリスト
    public List<GunnerDataBase> databases;

    // 使用する可能性のある銃士のデータリスト
    public List<GunnerData> possibleDataList;

    // 使用すると決まった銃士のデータリスト
    public List<GunnerData> finalDataList;

    // 現在ActiveなTargetを擁するSelectable
    public GunnerSelectable activeSelectable;

    // 銃士決定後、その情報を保管する変数
    public GunnerData selectedGunnerData;

    // 相手の選択した銃士のデータ
    public GunnerData opponentSelectionData;

    // 選んだ銃士を表示する領域
    [SerializeField] private TMP_Text infoArea_gunner;

    public HorizontalLayoutGroup myLayoutGroup;
    public ContentSizeFitter mySizeFitter;

    public void OrganizeGunners()
    {
        possibleDataList = new List<GunnerData>();
        finalDataList = new List<GunnerData>();

        // 一度全てのSelectableを非アクティブ化する
        foreach (var item in gunnerSelectables)
        {
            if (item.gameObject.activeSelf) { item.gameObject.SetActive(false); }
        }

        // データベース群の一つ一つのリストをpossibleDataListに追加
        foreach (var item in databases)
        {
            possibleDataList.AddRange(item.GetGunnerDataList());
        }

        // NORMALゲームモードのとき(つまり、カードプールがBLOCKに限定されており使用される銃士がデータリストの全てであるとき)
        if (gameMode == GameManager.GAME_MODE.NORMAL)
        {
            finalDataList = possibleDataList;
            ActivateGunnerSelectables(finalDataList.Count);
        }
        // UNLIMITEDゲームモードのとき(つまり、ランダムに銃士4人を選び出してそれを選択肢として提示するとき)
        else
        {
            for(int i = 0;i < 4; i++)
            {
                int randomId=Random.Range(0, possibleDataList.Count);
                finalDataList.Add(possibleDataList[randomId]);
                possibleDataList.RemoveAt(randomId);
            }
            ActivateGunnerSelectables(finalDataList.Count);
        }
        myLayoutGroup.CalculateLayoutInputHorizontal();
        myLayoutGroup.SetLayoutHorizontal();
        Canvas.ForceUpdateCanvases();
        mySizeFitter.enabled = false;
        mySizeFitter.enabled = true;
    }

    public void ActivateGunnerSelectables(int num)
    {
        for (int i = 0; i < num; i++)
        {
            var selectable = gunnerSelectables[i];
            selectable.gameObject.SetActive(true);
            selectable.data = finalDataList[i];
            selectable.SetupGunner();
            if(selectable.data == opponentSelectionData)
            {
                selectable.SetInavailable();
            }
        }
    }
}
