using Fusion;
using Fusion.Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using static UtilityTools;

// �l�b�g���[�N�֘A�̏������i��}�l�[�W���[�N���X
// ���̃N���X���̂͋��L����Ȃ��̂ŁANetworkBehaviour�ł͂Ȃ�MonoBehaviour����p������
namespace Metaphysics
{
    public class NetworkingManager : MonoBehaviour
    {
        // NetworkRunner�̃v���n�u
        [Header("Runner Prefab")]
        [SerializeField] private NetworkRunner runnerPrefab;

        // �e��f�[�^�̃v���n�u
        [Header("Data Prefabs")]
        [SerializeField] private NetworkObject menuDataPrefab;

        // runner�i�[�p�̕ϐ�
        public NetworkRunner MyRunner { get; private set; }

        // �e��f�[�^�i�[�p�ϐ�
        public NetworkedData_Menu LocalMenuData { get; set; }
        public NetworkedData_Menu RemoteMenuData { get; set; }

        // SessionInfo�̃��X�g(callbackManager��������󂯎��Ƃ��Ɏg��)
        public List<SessionInfo> sessionInfoList = new();

        // �ڑ����s�̌���
        public enum ConnectionResult
        {
            NONE,
            CONNECTED,      // �ڑ��ɐ��������ꍇ
            FAILED,         // �ڑ��Ɏ��s�����ꍇ
            NOT_FOUND,      // �����_���ΐ�Łu�������Ȃ���΃��j���[�ɖ߂�v��I�ѕ������Ȃ������ꍇ
        }

        // �����_���Ɍ��肳���ID�Ɩ��O�̒���
        private readonly int randomIdLength = 6;
        private readonly int randomNameLength = 4;


        // [����̑ΐ핔���֐ڑ�]
        // �߂�l�͎���state�Ɉڍs���Ă悢���̔���Ɏg��
        public async Task<ConnectionResult> ConnectToSession(string sessionID, string nameInput, bool isPublic)
        {
            // runner�𐶐�
            MyRunner = Instantiate(runnerPrefab);

            // SessionID�����L���̏ꍇ�A����10�̃����_�����������đ��
            if (sessionID.Length == 0)
            {
                sessionID = CreateRandomNumberString(randomIdLength);
            }

            // StartGameArgs���쐬
            StartGameArgs customGameArgs = new()
            {
                GameMode = GameMode.Shared,
                SessionName = sessionID,
            };

            var result = await MyRunner.StartGame(customGameArgs);

            if (result.Ok)
            {
                // menuData�𐶐�
                MyRunner.Spawn(menuDataPrefab).GetComponent<NetworkedData_Menu>();

                // ���[�U�[�l�[����ݒ�
                if (nameInput.Length > 0)
                {
                    LocalMenuData.RPC_SetUsername(nameInput);
                }
                else
                {
                    LocalMenuData.RPC_SetUsername("Player" + CreateRandomNumberString(randomNameLength));
                }


                // ���̑ΐ핔���Ɋ��ɐݒ肪�~����Ă��Ȃ��i���������ŏ��̎Q���ҁj�Ȃ���J�Ɋւ���ݒ��K�p
                if (!MyRunner.SessionInfo.Properties.ContainsKey("random"))
                {
                    // �����_���ΐ�ɂ������������邩�̐ݒ�
                    Dictionary<string, SessionProperty> customProps = new()
                    {
                        { "random", isPublic }
                    };

                    // �K�p
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

        // [�����_���ΐ�]
        // �߂�l��bool�͎���state�Ɉڍs���Ă悢���̔���Ɏg��
        public async Task<ConnectionResult> ConnectToRandomSession(string nameInput, bool createSession)
        {
            // runner�𐶐�
            MyRunner = Instantiate(runnerPrefab);

            // �����_���ΐ�ɂ������������镔�����������邽�߂̃t�B���^�[
            Dictionary<string, SessionProperty> customProps = new()
            {
                { "random", true }
            };

            // StartGameArgs���쐬�i���̒i�K�ł�join���������Ȃ��j
            StartGameArgs customGameArgs = new()
            {
                GameMode = GameMode.Shared,
                SessionProperties = customProps,
                EnableClientSessionCreation = false
            };

            var firstResult = await MyRunner.StartGame(customGameArgs);

            // ����ɐڑ��ł����ꍇ�i�܂�����̕�����join�ł����ꍇ�j
            if (firstResult.Ok)
            {
                // �V�K�ΐ핔���̍쐬��false�ɐݒ肳��Ă��Ď������������ɂ��Ȃ��ꍇ
                // �܂����ĕ���������Ă��܂����ꍇ�A�ΐ핔���𔲂���
                if (!createSession && MyRunner.SessionInfo.PlayerCount == 1)
                {
                    await MyRunner.Shutdown();
                    return ConnectionResult.NOT_FOUND;
                }

                // menuData�𐶐�
                MyRunner.Spawn(menuDataPrefab).GetComponent<NetworkedData_Menu>();

                // ���[�U�[�l�[����ݒ�
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
            // GameNotFound�Őڑ��Ɏ��s�����ꍇ�i�܂�join���镔����������Ȃ������ꍇ�j
            else if (firstResult.ShutdownReason == ShutdownReason.GameNotFound)
            {
                if (!createSession)
                {
                    await MyRunner.Shutdown();
                    return ConnectionResult.NOT_FOUND;
                }
                // ��xMyRunner������
                await MyRunner.Shutdown();

                // �ēxNetworkRunner�𐶐�
                MyRunner = Instantiate(runnerPrefab);

                // ConnectToSession���\�b�h��p���ă����_���ΐ�\�ȑΐ핔�����쐬
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