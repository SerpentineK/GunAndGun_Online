using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public NetworkRunner MyRunner { get; private set; }

        // 各種データ格納用変数
        public NetworkedData_Menu MenuData { get; private set; }

        

        public async Task EnterSession(string sessionID, string nameInput, bool isRandom)
        {
            // runnerを生成
            MyRunner = Instantiate(runnerPrefab);

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

            var startTask = MyRunner.StartGame(customGameArgs);
            await startTask;

            // menuDataを生成
            MenuData = MyRunner.Spawn(menuDataPrefab).GetComponent<NetworkedData_Menu>();
            MenuData.transform.parent = this.transform;

            // ユーザーネームを設定
            if (nameInput.Length > 0)
            {
                MenuData.RPC_SetUsername(nameInput);
            }
            else
            {
                int randomNum = Random.Range(1, 10000);
                MenuData.RPC_SetUsername("Player" + string.Format("{0:0000}", randomNum));
            }
        }

    }
}