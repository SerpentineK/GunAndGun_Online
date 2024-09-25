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

public class InitialMenuState : MonoBehaviour, IState
{
    // Metaphysics���痈��������͂���ϐ�
    private CardpoolDatabase[] databases;
    private NetworkingManager networkingManager;
    private SelectionRecordManager recordManager;

    // Controller�I�u�W�F�N�g
    [SerializeField] private MenuStateController controller;

    // (�I�����C���ΐ�p) �Z�b�V����ID�ƃj�b�N�l�[������͂��邽�߂̃E�B���h�E
    [Header("Input Window")]
    [SerializeField] private GameObject inputWindow;
    [SerializeField] private Toggle toggle_randomSession;
    [SerializeField] private TMP_Text toggle_footnotes;
    [SerializeField] private TMP_InputField field_sessionID;
    [SerializeField] private TMP_Text sessionID_footnotes;
    [SerializeField] private TMP_InputField field_nickname;

    private readonly string toggle_InactiveNotes = "�����_���ΐ킪�I�t�̏ꍇ\n�w�肵��ID�̑ΐ핔���ɐڑ�����܂�";
    private readonly string toggle_ActiveNotes = "�����_���ΐ킪�I���̏ꍇ\n�����_���ȑΐ핔���ɐڑ�����܂�\n�i�Z�b�V����ID�͕s�v�ł��j";

    private readonly string sessionID_InactiveNotes = "�����_���ΐ킪�I���ɂȂ��Ă��邽��\nID����͂��邱�Ƃ͂ł��܂���";
    private readonly string sessionID_ActiveNotes = "ID�������͂̏ꍇ\n�����_����ID�őΐ핔�����쐬���܂�";

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

        // InputWindow�ABossWindow�ATutorialWindow���\��
        ToggleGameObject(inputWindow, false);
        ToggleGameObject(bossWindow, false);
        ToggleGameObject(tutorialWindow, false);

        // InputWindow�̒l������������
        toggle_randomSession.isOn = false;
        toggle_footnotes.SetText(toggle_InactiveNotes);
        field_sessionID.SetTextWithoutNotify("");
        sessionID_footnotes.SetText(sessionID_ActiveNotes);
        field_nickname.SetTextWithoutNotify("");

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
        if(bossCandidateParent.childCount != 0)
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

    public void InState(){ }

    public void ExitState() 
    {
        ToggleGameObject(gameObject, false);
    }

    public void SetGunnerFigures(GunnerData rightData, GunnerData leftData)
    {
        rightGunnerFigure.sprite = rightData.GetGunnerImage();
        leftGunnerFigure.sprite = leftData.GetGunnerImage();
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

    public void OnButtonPressed_Battle()
    {
        ToggleGameObject(inputWindow, true);
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

    public void OnToggle_RandomSession()
    {
        field_sessionID.interactable = !toggle_randomSession.isOn;
        if (!field_sessionID.interactable)
        {
            field_sessionID.SetTextWithoutNotify("");
            toggle_footnotes.SetText(toggle_ActiveNotes);
            sessionID_footnotes.SetText(sessionID_InactiveNotes);
        }
        else
        {
            toggle_footnotes.SetText(toggle_InactiveNotes);
            sessionID_footnotes.SetText(sessionID_ActiveNotes);
        }
    }

    public void OnButtonPressed_InputEntry()
    {
        string sessionID = field_sessionID.text;
        string nickname = field_nickname.text;
        networkingManager.EnterSession(sessionID, nickname);
        controller.SwitchStates(controller.battleStandbyS);
    }

    public void OnButtonPressed_InputExit()
    {
        field_sessionID.SetTextWithoutNotify("");
        field_nickname.SetTextWithoutNotify("");
        ToggleGameObject(inputWindow, false);
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
