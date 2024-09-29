using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

// �l�b�g���[�N�֘A�̏������i��}�l�[�W���[�N���X
// ���̃N���X���̂͋��L����Ȃ��̂ŁANetworkBehaviour�ł͂Ȃ�MonoBehaviour����p������
namespace Metaphysics
{
    public class NetworkingManager : MonoBehaviour
    {
        // NetworkRunner�̃v���n�u
        [SerializeField] private NetworkRunner runnerPrefab;

        // �e��f�[�^�̃v���n�u
        [SerializeField] private NetworkObject menuDataPrefab;

        // runner�i�[�p�̕ϐ�
        public NetworkRunner MyRunner { get; private set; }

        // �e��f�[�^�i�[�p�ϐ�
        public NetworkedData_Menu MenuData { get; private set; }

        

        public async Task EnterSession(string sessionID, string nameInput, bool isRandom)
        {
            // runner�𐶐�
            MyRunner = Instantiate(runnerPrefab);

            // NetworkRunner.StartGame()���Ăяo���ASession�ɐڑ�

            StartGameArgs customGameArgs;

            // ���ۂ�SessionName��Fusion���������Ă����GUID�ɔC���āASessionID��SessionProperty�ɓ����
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

            // menuData�𐶐�
            MenuData = MyRunner.Spawn(menuDataPrefab).GetComponent<NetworkedData_Menu>();
            MenuData.transform.parent = this.transform;

            // ���[�U�[�l�[����ݒ�
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