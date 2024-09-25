using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ネットワーク関連の処理を司るマネージャークラス
// このクラス自体は共有されないので、NetworkBehaviourではなくMonoBehaviourから継承する
namespace Metaphysics
{
    public class NetworkingManager : MonoBehaviour
    {
        // NetworkRunnerのプレハブ
        [SerializeField] private GameObject runnerPrefab;

        // 各種データのプレハブ
        [SerializeField] private NetworkObject menuDataPrefab;

        // runner格納用の変数
        private NetworkRunner runner;

        // 各種データ格納用変数
        private NetworkedData_Menu menuData;

        public void EnterSession(string sessionID, string nameInput)
        {
            // runnerを生成
            runner = Instantiate(runnerPrefab).GetComponent<NetworkRunner>();

            // NetworkRunner.StartGame()を呼び出し、Sessionに接続
            // SessionNameが空白の場合はFusionでランダムな部屋に繋いでくれるらしい
            // (空いてる部屋がない場合はランダムなIDでの部屋生成までやってくれるっぽい)
            runner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Shared,
                SessionName = sessionID,
                MatchmakingMode = Fusion.Photon.Realtime.MatchmakingMode.FillRoom
            });

            // menuDataを生成
            NetworkObject tempObject = runner.Spawn(menuDataPrefab);
            menuData = tempObject.GetComponent<NetworkedData_Menu>();

            // ユーザーネームを設定
            if (nameInput.Length > 0)
            {
                menuData.Username = nameInput;
            }
            else
            {
                int randomNum = Random.Range(1, 10000);
                menuData.Username = "Player" + string.Format("{0:0000}", randomNum);
            }
        }
    }
}