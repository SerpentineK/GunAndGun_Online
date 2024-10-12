using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Metaphysics
{
    public class NetworkedData_Menu : NetworkBehaviour
    {
        public bool IsLocal => HasStateAuthority;

        public override void Spawned()
        {
            base.Spawned();

            // Metaphysics�V�[�����擾
            Scene metaScene = SceneManager.GetSceneByName("Metaphysics");

            // Metaphysics����NetworkingManager�̃R���|�[�l���g������Ă��Đe�I�u�W�F�N�g�ɂ���
            foreach (var rootObj in metaScene.GetRootGameObjects())
            {
                if (rootObj.TryGetComponent<NetworkingManager>(out var networking))
                {
                    this.transform.parent = networking.transform;
                    if (IsLocal)
                    {
                        networking.LocalMenuData = this;
                        
                    }
                    else
                    {
                        networking.RemoteMenuData = this;
                    }
                }
            }
        }

        [Networked] public string Username { get; set; }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        public void RPC_SetUsername(string username)
        {
            Username = username;
            gameObject.name = Username + "_MenuData";
        }
    }
}