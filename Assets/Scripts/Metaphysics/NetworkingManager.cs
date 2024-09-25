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

        // �e��f�[�^
        [SerializeField] private NetworkedData_Menu menuData;

        // runner�i�[�p�̕ϐ�
        private NetworkRunner runner;

        public void EnterSession(string sessionID, string nameInput)
        {
            // runner�𐶐�
            runner = Instantiate(runnerPrefab, transform).GetComponent<NetworkRunner>();


            // NetworkRunner.StartGame()���Ăяo���ASession�ɐڑ�
            // SessionName���󔒂̏ꍇ��Fusion�Ń����_���ȕ����Ɍq���ł����炵��
            // (�󂢂Ă镔�����Ȃ��ꍇ�̓����_����ID�ł̕��������܂ł���Ă������ۂ�)
            runner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Shared,
                SessionName = sessionID,
                MatchmakingMode = Fusion.Photon.Realtime.MatchmakingMode.FillRoom
            });

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