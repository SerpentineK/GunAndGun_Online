using Fusion;
using Fusion.Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using static UtilityTools;

// ネットワーク関連の処理を司るマネージャークラス
// このクラス自体は共有されないので、NetworkBehaviourではなくMonoBehaviourから継承する
namespace Metaphysics
{
    public class NetworkingManager : MonoBehaviour
    {
        // NetworkRunnerのプレハブ
        [Header("Runner Prefab")]
        [SerializeField] private NetworkRunner runnerPrefab;

        // 各種データのプレハブ
        [Header("Data Prefabs")]
        [SerializeField] private NetworkObject menuDataPrefab;

        // runner格納用の変数
        public NetworkRunner MyRunner { get; private set; }

        // 各種データ格納用変数
        public NetworkedData_Menu LocalMenuData { get; set; }
        public NetworkedData_Menu RemoteMenuData { get; set; }

        // SessionInfoのリスト(callbackManagerから情報を受け取るときに使う)
        public List<SessionInfo> sessionInfoList = new();

        // 接続試行の結果
        public enum ConnectionResult
        {
            NONE,
            CONNECTED,      // 接続に成功した場合
            FAILED,         // 接続に失敗した場合
            NOT_FOUND,      // ランダム対戦で「部屋がなければメニューに戻る」を選び部屋がなかった場合
        }

        // ランダムに決定されるIDと名前の長さ
        private readonly int randomIdLength = 6;
        private readonly int randomNameLength = 4;


        // [特定の対戦部屋へ接続]
        // 戻り値は次のstateに移行してよいかの判定に使う
        public async Task<ConnectionResult> ConnectToSession(string sessionID, string nameInput, bool isPublic)
        {
            // runnerを生成
            MyRunner = Instantiate(runnerPrefab);

            // SessionIDが未記入の場合、長さ10のランダム数列を作って代入
            if (sessionID.Length == 0)
            {
                sessionID = CreateRandomNumberString(randomIdLength);
            }

            // StartGameArgsを作成
            StartGameArgs customGameArgs = new()
            {
                GameMode = GameMode.Shared,
                SessionName = sessionID,
            };

            var result = await MyRunner.StartGame(customGameArgs);

            if (result.Ok)
            {
                // menuDataを生成
                MyRunner.Spawn(menuDataPrefab).GetComponent<NetworkedData_Menu>();

                // ユーザーネームを設定
                if (nameInput.Length > 0)
                {
                    LocalMenuData.RPC_SetUsername(nameInput);
                }
                else
                {
                    LocalMenuData.RPC_SetUsername("Player" + CreateRandomNumberString(randomNameLength));
                }


                // この対戦部屋に既に設定が敷かれていない（＝自分が最初の参加者）なら公開に関する設定を適用
                if (!MyRunner.SessionInfo.Properties.ContainsKey("random"))
                {
                    // ランダム対戦による入室を許可するかの設定
                    Dictionary<string, SessionProperty> customProps = new()
                    {
                        { "random", isPublic }
                    };

                    // 適用
                    MyRunner.SessionInfo.UpdateCustomProperties(customProps);
                }

                return ConnectionResult.CONNECTED;
            }
            else
            {
                Debug.Log($"Failed to start game: {result.ShutdownReason}");
                return ConnectionResult.FAILED;
            }
        }

        // [ランダム対戦]
        // 戻り値のboolは次のstateに移行してよいかの判定に使う
        public async Task<ConnectionResult> ConnectToRandomSession(string nameInput, bool createSession)
        {
            // runnerを生成
            MyRunner = Instantiate(runnerPrefab);

            // ランダム対戦による入室を許可する部屋を検索するためのフィルター
            Dictionary<string, SessionProperty> customProps = new()
            {
                { "random", true }
            };

            // StartGameArgsを作成（この段階ではjoinしか許可しない）
            StartGameArgs customGameArgs = new()
            {
                GameMode = GameMode.Shared,
                SessionProperties = customProps,
                EnableClientSessionCreation = false
            };

            var firstResult = await MyRunner.StartGame(customGameArgs);

            // 正常に接続できた場合（つまり既存の部屋にjoinできた場合）
            if (firstResult.Ok)
            {
                // 新規対戦部屋の作成がfalseに設定されていて自分しか部屋にいない場合
                // つまり誤って部屋を作ってしまった場合、対戦部屋を抜ける
                if (!createSession && MyRunner.SessionInfo.PlayerCount == 1)
                {
                    await MyRunner.Shutdown();
                    return ConnectionResult.NOT_FOUND;
                }

                // menuDataを生成
                MyRunner.Spawn(menuDataPrefab).GetComponent<NetworkedData_Menu>();

                // ユーザーネームを設定
                if (nameInput.Length > 0)
                {
                    LocalMenuData.RPC_SetUsername(nameInput);
                }
                else
                {
                    LocalMenuData.RPC_SetUsername("Player" + CreateRandomNumberString(randomNameLength));
                }

                return ConnectionResult.CONNECTED;
            }
            // GameNotFoundで接続に失敗した場合（つまりjoinする部屋が見つからなかった場合）
            else if (firstResult.ShutdownReason == ShutdownReason.GameNotFound)
            {
                if (!createSession)
                {
                    await MyRunner.Shutdown();
                    return ConnectionResult.NOT_FOUND;
                }
                // 一度MyRunnerを消去
                await MyRunner.Shutdown();

                // 再度NetworkRunnerを生成
                MyRunner = Instantiate(runnerPrefab);

                // ConnectToSessionメソッドを用いてランダム対戦可能な対戦部屋を作成
                var secondResult = await ConnectToSession(CreateRandomNumberString(randomIdLength), nameInput, true);
                return secondResult;
            }
            else
            {
                Debug.Log($"Failed to start game: {firstResult.ShutdownReason}");
                return ConnectionResult.FAILED;
            }

        }

        
    }
}