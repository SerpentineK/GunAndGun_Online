using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GunnerSelector : MonoBehaviour
{
    // ���݂̃Q�[�����[�h
    public GameManager.GAME_MODE gameMode;

    // �X�̏e�m��Selectable
    public GunnerSelectable[] gunnerSelectables;

    // �g�p����e�m�̃f�[�^�x�[�X���X�g
    public List<GunnerDataBase> databases;

    // �g�p����\���̂���e�m�̃f�[�^���X�g
    public List<GunnerData> possibleDataList;

    // �g�p����ƌ��܂����e�m�̃f�[�^���X�g
    public List<GunnerData> finalDataList;

    // ����Active��Target��i����Selectable
    public GunnerSelectable activeSelectable;

    // �e�m�����A���̏���ۊǂ���ϐ�
    public GunnerData selectedGunnerData;

    // ����̑I�������e�m�̃f�[�^
    public GunnerData opponentSelectionData;

    // �I�񂾏e�m��\������̈�
    [SerializeField] private TMP_Text infoArea_gunner;

    public HorizontalLayoutGroup myLayoutGroup;
    public ContentSizeFitter mySizeFitter;

    public void OrganizeGunners()
    {
        possibleDataList = new List<GunnerData>();
        finalDataList = new List<GunnerData>();

        // ��x�S�Ă�Selectable���A�N�e�B�u������
        foreach (var item in gunnerSelectables)
        {
            if (item.gameObject.activeSelf) { item.gameObject.SetActive(false); }
        }

        // �f�[�^�x�[�X�Q�̈��̃��X�g��possibleDataList�ɒǉ�
        foreach (var item in databases)
        {
            possibleDataList.AddRange(item.GetGunnerDataList());
        }

        // NORMAL�Q�[�����[�h�̂Ƃ�(�܂�A�J�[�h�v�[����BLOCK�Ɍ��肳��Ă���g�p�����e�m���f�[�^���X�g�̑S�Ăł���Ƃ�)
        if (gameMode == GameManager.GAME_MODE.NORMAL)
        {
            finalDataList = possibleDataList;
            ActivateGunnerSelectables(finalDataList.Count);
        }
        // UNLIMITED�Q�[�����[�h�̂Ƃ�(�܂�A�����_���ɏe�m4�l��I�яo���Ă����I�����Ƃ��Ē񎦂���Ƃ�)
        else
        {
            for(int i = 0;i < 4; i++)
            {
                int randomId=Random.Range(0, possibleDataList.Count);
                finalDataList.Add(possibleDataList[randomId]);
                possibleDataList.RemoveAt(randomId);
            }
            ActivateGunnerSelectables(finalDataList.Count);
        }
        myLayoutGroup.CalculateLayoutInputHorizontal();
        myLayoutGroup.SetLayoutHorizontal();
        Canvas.ForceUpdateCanvases();
        mySizeFitter.enabled = false;
        mySizeFitter.enabled = true;
    }

    public void ActivateGunnerSelectables(int num)
    {
        for (int i = 0; i < num; i++)
        {
            var selectable = gunnerSelectables[i];
            selectable.gameObject.SetActive(true);
            selectable.data = finalDataList[i];
            selectable.SetupGunner();
            if(selectable.data == opponentSelectionData)
            {
                selectable.SetInavailable();
            }
        }
    }
}
