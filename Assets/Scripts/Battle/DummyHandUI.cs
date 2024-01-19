using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �_�~�[��D����N���X
/// </summary>
public class DummyHandUI : MonoBehaviour
{
    // �_�~�[��D����pHorizontalLayoutGroup
    [SerializeField] private HorizontalLayoutGroup layoutGroup = null;
    // �_�~�[��D�v���n�u
    [SerializeField] private GameObject dummyHandPrefab = null;

    // ���������_�~�[��D�̃��X�g
    private List<Transform> dummyHandList;

    /// <summary>
    /// �w��̖����ɂȂ�悤�_�~�[��D���쐬�܂��͍폜����
    /// </summary>
    /// <param name="value">�ݒ薇��</param>
    public void SetHandNum(int value)
    {
        if (dummyHandList == null)
        {// ������s��
         // ���X�g�V�K����
            dummyHandList = new List<Transform>();
            AddHandObj(value);
        }
        else
        {
            // ���݂���ω����閇�����v�Z
            int differenceNum = value - dummyHandList.Count;
            // �_�~�[��D�쐬�E�폜
            if (differenceNum > 0) // ��D��������Ȃ�_�~�[��D�쐬
                AddHandObj(differenceNum);
            else if (differenceNum < 0) // ��D������Ȃ�_�~�[��D�폜
                RemoveHandObj(differenceNum);
        }
    }

    /// <summary>
    /// �_�~�[��D���w�薇���ǉ�����
    /// </summary>
    private void AddHandObj(int value)
    {
        // �ǉ��������I�u�W�F�N�g�쐬
        for (int i = 0; i < value; i++)
        {
            // �I�u�W�F�N�g�쐬
            var obj = Instantiate(dummyHandPrefab, transform);
            // ���X�g�ɒǉ�
            dummyHandList.Add(obj.transform);
        }
    }
    /// <summary>
    /// �_�~�[��D���w�薇���폜����
    /// </summary>
    private void RemoveHandObj(int value)
    {
        // �폜�����𐳐��Ŏ擾
        value = Mathf.Abs(value);
        // �폜�������I�u�W�F�N�g�폜
        for (int i = 0; i < value; i++)
        {
            if (dummyHandList.Count <= 0)
                break;

            // �I�u�W�F�N�g�폜
            Destroy(dummyHandList[0].gameObject);
            // ���X�g����폜
            dummyHandList.RemoveAt(0);
        }
    }

    /// <summary>
    /// �Y���ԍ��̃_�~�[��D�̍��W��Ԃ�
    /// </summary>
    public Vector2 GetHandPos(int index)
    {
        if (index < 0 || index >= dummyHandList.Count)
            return Vector2.zero;
        // �_�~�[��D�̍��W��Ԃ�
        return dummyHandList[index].position;
    }

    /// <summary>
    /// ���C�A�E�g�̎�������@�\�𑦍��ɓK�p����
    /// </summary>
    public void ApplyLayout()
    {
        layoutGroup.CalculateLayoutInputHorizontal();
        layoutGroup.SetLayoutHorizontal();
    }
}