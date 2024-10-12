using Fusion;
using InitialMenu;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Collections.Unicode;
using static MenuStateManager;
using Metaphysics;
using static AsyncSceneLoader;
using BattleStandbyMenu;

namespace InitialMenu
{
    public class InitialMenuState : MonoBehaviour, IState
    {
        // Metaphysics���痈��������͂���ϐ�
        private CardpoolDatabase[] databases;
        private NetworkingManager networkingManager;
        private SelectionRecordManager recordManager;

        // Controller�I�u�W�F�N�g
        [SerializeField] private MenuStateController controller;

        // (�I�����C���ΐ�p) �Z�b�V����ID�ƃj�b�N�l�[������͂��邽�߂̃E�B���h�E
        [Header("Session Window")]
        [SerializeField] private GameObject sessionWindow;

        [Header("Specify Session")]
        [SerializeField] private GameObject specificSessionWindow;
        [SerializeField] private TMP_InputField sessionID_SSW;
        [SerializeField] private TMP_InputField username_SSW;
        [SerializeField] private TMP_Dropdown publicity_SSW;

        [Header("Random Session")]
        [SerializeField] private GameObject randomSessionWindow;
        [SerializeField] private TMP_InputField username_RSW;
        [SerializeField] private TMP_Dropdown matchingMode_RSW;
        [SerializeField] private GameObject errorMessage_RSW;

        [Header("Loading Display")]
        [SerializeField] private GameObject LoadingCanvas;

        private GameObject currentInputWindow = null;

        // (�{�X�o�g���p) �ΐ푊��ɂ���{�X��I�����邽�߂̃E�B���h�E
        [Header("Boss Window")]
        [SerializeField] private GameObject bossWindow;
        [SerializeField] private Transform bossCandidateParent;
        [SerializeField] private GameObject bossCandidatePrefab;
        [SerializeField] private Button bossEntryButton;
        private BossData selectedBossData = null;

        [Header("Tutorial Confirmation Window")]
        [SerializeField] private GameObject tutorialWindow;

        // �e��{�^��
        [Header("Buttons")]
        [SerializeField] private Button battleButton;
        [SerializeField] private Button bossButton;
        [SerializeField] private Button tutorialButton;

        // ���E�̏e�m�C���[�W
        [Header("Gunner Figures")]
        [SerializeField] private SpriteRenderer rightGunnerFigure;
        [SerializeField] private SpriteRenderer leftGunnerFigure;

        // BossCandidate�����邽�߂�Array�i�����K�v�ɂȂ�j
        private BossCandidate[] bossCandidateArray;

        public void EnterState()
        {
            // InitialMenu�{�̂�\��
            ToggleGameObject(gameObject, true);

            // InputWindow�ABossWindow�ATutorialWindow�ALoadingCanvas���\��
            ToggleGameObject(sessionWindow, false);
            ToggleGameObject(bossWindow, false);
            ToggleGameObject(tutorialWindow, false);
            ToggleGameObject(LoadingCanvas, false);

            // InputWindow�̒l������������
            BatchResetValues();

            // Metaphysics�V�[�����擾
            Scene metaScene = SceneManager.GetSceneByName("Metaphysics");

            // Metaphysics����f�[�^�x�[�X�̎Q�Ƃ�NetworkingManager�̃R���|�[�l���g������Ă���
            foreach (var rootObj in metaScene.GetRootGameObjects())
            {
                if (rootObj.TryGetComponent<DatabaseRef>(out var databaseRef))
                {
                    databases = databaseRef.Databases;
                }
                else if (rootObj.TryGetComponent<NetworkingManager>(out var networking))
                {
                    networkingManager = networking;
                }
                else if (rootObj.TryGetComponent<SelectionRecordManager>(out var recordM))
                {
                    recordManager = recordM;
                }
            }

            List<GunnerData> gunners = new();
            List<BossData> bosses = new();

            foreach (var cardpool in databases)
            {
                gunners.AddRange(cardpool.GunnerDatabase.GetGunnerDataList());
                bosses.AddRange(cardpool.Bosses);
            }

            // ���E�̏e�m�摜�������_���Ɍ���
            GunnerData rightGunnerData = gunners[Random.Range(0, gunners.Count)];
            gunners.Remove(rightGunnerData);
            GunnerData leftGunnerData = gunners[Random.Range(0, gunners.Count)];

            SetGunnerFigures(rightGunnerData, leftGunnerData);

            bossCandidateArray = new BossCandidate[bosses.Count];
            int count = 0;
            if (bossCandidateParent.childCount != 0)
            {
                foreach (Transform child in bossCandidateParent.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            // BossWindow�ɕ\������BossCandidate��ʎY(����̓�����l����)
            foreach (var boss in bosses)
            {
                if (Instantiate(bossCandidatePrefab, bossCandidateParent).TryGetComponent<BossCandidate>(out var candidate))
                {
                    candidate.SetCandidateData(boss);
                    bossCandidateArray[count] = candidate;
                    candidate.initialS = this;
                    count++;
                }
            }

            // BossEntryButton��interactable��؂��Ă����i�{�X��I��łȂ���Ԃ�Entry�����ƍ���̂Łj
            bossEntryButton.interactable = false;
        }

        public void InState() { }

        public void ExitState()
        {
            ToggleGameObject(gameObject, false);
        }

        public void SetGunnerFigures(GunnerData rightData, GunnerData leftData)
        {
            rightGunnerFigure.sprite = rightData.GetGunnerImage();
            leftGunnerFigure.sprite = leftData.GetGunnerImage();
        }

        private void BatchResetValues()
        {
            ToggleGameObject(specificSessionWindow, true);
            ToggleGameObject(randomSessionWindow, false);
            currentInputWindow = specificSessionWindow;

            sessionID_SSW.SetTextWithoutNotify("");
            username_SSW.SetTextWithoutNotify("");
            publicity_SSW.value = 0;

            username_RSW.SetTextWithoutNotify("");
            matchingMode_RSW.value = 0;
            ToggleGameObject(errorMessage_RSW, false);
        }

        public void SelectBossCandidate(BossCandidate selected)
        {
            foreach (var boss in bossCandidateArray)
            {
                boss.ToggleSelectionState(boss == selected);
            }
            selectedBossData = (selected != null) ? selected.MyData : null;
            bossEntryButton.interactable = (selectedBossData != null);
        }

        public void ToggleButtons(bool result)
        {
            battleButton.interactable = result;
            bossButton.interactable = result;
            tutorialButton.interactable = result;
        }

        public void OnButtonPressed_Online()
        {
            ToggleGameObject(sessionWindow, true);
            ToggleButtons(false);
        }

        public void OnButtonPressed_Boss()
        {
            ToggleGameObject(bossWindow, true);
            ToggleButtons(false);
        }

        public void OnButtonPressed_Tutorial()
        {
            ToggleGameObject(tutorialWindow, true);
            ToggleButtons(false);
        }

        public void OnButtonPressed_WindowChange(GameObject nextWindow)
        {
            if (nextWindow != currentInputWindow)
            {
                if (currentInputWindow != null) { ToggleGameObject(currentInputWindow, false); }
                if (nextWindow != null) { ToggleGameObject(nextWindow, true); }
                currentInputWindow = nextWindow;
            }
        }

        public async void OnButtonPressed_EntrySSW()
        {
            string sessionID = sessionID_SSW.text;
            string username = username_SSW.text;
            bool isPublic = publicity_SSW.value == 0;
            ToggleGameObject(LoadingCanvas, true);
            var result = await networkingManager.ConnectToSession(sessionID, username, isPublic);
            ToggleGameObject(LoadingCanvas, false);
            if (result == NetworkingManager.ConnectionResult.CONNECTED)
            {
                controller.SwitchStates(controller.battleStandbyS);
            }
        }

        public async void OnButtonPressed_EntryRSW()
        {
            string username = username_RSW.text;
            bool createSession = matchingMode_RSW.value == 0;
            ToggleGameObject(LoadingCanvas, true);
            var result = await networkingManager.ConnectToRandomSession(username, createSession);
            ToggleGameObject(LoadingCanvas, false);
            if (result == NetworkingManager.ConnectionResult.CONNECTED)
            {
                controller.SwitchStates(controller.battleStandbyS);
            }
            else if (result == NetworkingManager.ConnectionResult.NOT_FOUND)
            {
                ToggleGameObject(errorMessage_RSW, true);
            }
        }

        public void OnButtonPressed_InputExit()
        {
            BatchResetValues();
            ToggleGameObject(sessionWindow, false);
            ToggleButtons(true);
        }

        public void OnButtonPressed_BossEntry()
        {
            recordManager.StartRecording_Singleplayer(selectedBossData);
            controller.SwitchStates(controller.bossStandbyS);
        }

        public void OnButtonPressed_BossExit()
        {
            SelectBossCandidate(null);
            ToggleGameObject(bossWindow, false);
            ToggleButtons(true);
        }
        public void OnButtonPressed_TutorialEntry()
        {
            StartCoroutine(LoadNextSceneAsync("TutorialScene"));
            StartCoroutine(UnloadPreviousSceneAsync(gameObject.scene.name));
        }

        public void OnButtonPressed_TutorialExit()
        {
            ToggleGameObject(tutorialWindow, false);
            ToggleButtons(true);
        }
    }
}