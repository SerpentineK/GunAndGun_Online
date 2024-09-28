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
        [SerializeField] private NetworkRunner runnerPrefab;

        // 各種データのプレハブ
        [SerializeField] private NetworkObject menuDataPrefab;

        // runner格納用の変数
        private NetworkRunner runner;

        // 各種データ格納用変数
        private NetworkedData_Menu menuData;

        public async void EnterSession(string sessionID, string nameInput, bool isRandom)
        {
            // runnerを生成
            runner = Instantiate(runnerPrefab);

            // NetworkRunner.StartGame()を呼び出し、Sessionに接続

            StartGameArgs customGameArgs;

            // 実際のSessionNameはFusionが生成してくれるGUIDに任せて、SessionIDはSessionPropertyに入れる
            if (isRandom)
            {
                customGameArgs = new StartGameArgs 
                { 
                    GameMode = GameMode.Shared,
                    MatchmakingMode = Fusion.Photon.Realtime.MatchmakingMode.RandomMatching
                };
            }
            else
            {
                var customProps = new Dictionary<string, SessionProperty>
                {
                    ["Session ID"] = sessionID
                };

                customGameArgs = new StartGameArgs
                {
                    GameMode = GameMode.Shared,
                    SessionProperties = customProps,
                    MatchmakingMode = Fusion.Photon.Realtime.MatchmakingMode.FillRoom
                };
            }

            var startTask = runner.StartGame(customGameArgs);
            await startTask;

            // menuDataを生成
            menuData = runner.Spawn(menuDataPrefab).gameObject.GetComponent<NetworkedData_Menu>();

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