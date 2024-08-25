using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunSelector : MonoBehaviour
{
    // ���݂̃Q�[�����[�h
    public GameManager.GAME_MODE gameMode;

    // �X�̋@�e��Candidate
    public GunCandidate[] gunCandidates;

    // �g�p����@�e�̃f�[�^�x�[�X���X�g
    public List<GunsDataBase> databases;

    // �g�p����\���̂���@�e�̃f�[�^���X�g
    public List<GunsData> possibleDataList;

    // �g�p����ƌ��܂����@�e�̃f�[�^���X�g
    public List<GunsData> finalDataList;

    // ����Active��Target�ƕR�Â���Candidate
    public GunCandidate activeCandidate;

    // �@�e�����A���̏���ۊǂ���ϐ�
    public GunsData selectedGunData;

    // �I��s�\�ȋ@�e�̃��X�g�i�����⑊�肪�I���ς݂̋@�e�j
    public List<GunsData> inavailableGunsDataList;

    // �I�񂾏e�m��\������̈�
    [SerializeField] private TMP_Text infoArea_gunner;

    public HorizontalLayoutGroup myLayoutGroup;
    public ContentSizeFitter mySizeFitter;

    private readonly GunsData.GunType[] gunTypes = { GunsData.GunType.LIGHT, GunsData.GunType.HEAVY, GunsData.GunType.SPECIAL};

    public void OrganizeGuns()
    {
        possibleDataList = new List<GunsData>();
        finalDataList = new List<GunsData>();

        // ��x�S�Ă�Candidate���A�N�e�B�u��
        foreach (var item in gunCandidates)
        {
            if (item.gameObject.activeSelf) { item.gameObject.SetActive(false); }
        }

        // �f�[�^�x�[�X�Q�̈��̃��X�g��possibleDataList�ɒǉ�
        foreach (var item in databases)
        {
            possibleDataList.AddRange(item.GetGunsDataList());
        }

        // Gamemode��NORMAL�̂Ƃ�(�܂�A�J�[�h�v�[����BLOCK�Ɍ��肳��Ă���g�p�����@�e���f�[�^���X�g�̑S�Ăł���Ƃ�)
        if (gameMode == GameManager.GAME_MODE.NORMAL)
        {
            finalDataList = possibleDataList;
            ActivateGunCandidates(finalDataList.Count);
        }
        // UNLIMITED�Q�[�����[�h�̂Ƃ�(�܂�A�����_���ɋ@�e8����I�яo���Ă����I�����Ƃ��Ē񎦂���Ƃ�)
        else
        {
            foreach(var item in gunTypes)
            {
                var temporaryList = possibleDataList.FindAll(x => x.GetGunType() == item);
                for (int i = 0; i < 2; i++)
                {
                    int randomId = Random.Range(0, temporaryList.Count);
                    finalDataList.Add(temporaryList[randomId]);
                    temporaryList.RemoveAt(randomId);
                }
            }
            ActivateGunCandidates(finalDataList.Count);
        }
        GunCandidate firstGunCandidate = gunCandidates[0];
        firstGunCandidate.ActivateTarget();
        activeCandidate = firstGunCandidate;
    }

    // GunCandidate���Z�b�g�A�b�v����֐�
    // (GunTarget��SetupGun�֐��ŌX��GunCandidate�ɕR�Â������̂��Z�b�g�A�b�v����邽�߂����ł͐G��Ȃ�)
    public void ActivateGunCandidates(int num)
    {
        for(int i = 0; i < num; i++)
        {
            var candidate = gunCandidates[i];
            candidate.data = finalDataList[i];
            candidate.SetupGun();
            if (inavailableGunsDataList.Contains(candidate.data)) 
            {
                candidate.SetInavailableForCandidate();
                candidate.target.SetInavailableForTarget();
            }
        }
    }

}
