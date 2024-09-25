using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �l�b�g���[�N�֘A�̏������i��}�l�[�W���[�N���X
// ���̃N���X���̂͋��L����Ȃ��̂ŁANetworkBehaviour�ł͂Ȃ�MonoBehaviour����p������
namespace Metaphysics
{
    public class NetworkingManager : MonoBehaviour
    {
        // NetworkRunner�̃v���n�u
        [SerializeField] private GameObject runnerPrefab;

        // �e��f�[�^�̃v���n�u
        [SerializeField] private NetworkObject menuDataPrefab;

        // runner�i�[�p�̕ϐ�
        private NetworkRunner runner;

        // �e��f�[�^�i�[�p�ϐ�
        private NetworkedData_Menu menuData;

        public void EnterSession(string sessionID, string nameInput)
        {
            // runner�𐶐�
            runner = Instantiate(runnerPrefab).GetComponent<NetworkRunner>();

            // NetworkRunner.StartGame()���Ăяo���ASession�ɐڑ�
            // SessionName���󔒂̏ꍇ��Fusion�Ń����_���ȕ����Ɍq���ł����炵��
            // (�󂢂Ă镔�����Ȃ��ꍇ�̓����_����ID�ł̕��������܂ł���Ă������ۂ�)
            runner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Shared,
                SessionName = sessionID,
                MatchmakingMode = Fusion.Photon.Realtime.MatchmakingMode.FillRoom
            });

            // menuData�𐶐�
            NetworkObject tempObject = runner.Spawn(menuDataPrefab);
            menuData = tempObject.GetComponent<NetworkedData_Menu>();

            // ���[�U�[�l�[����ݒ�
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