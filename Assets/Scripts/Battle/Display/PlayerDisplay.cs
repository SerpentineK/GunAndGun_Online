using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ���[�J���ɂ̂ݑ��݂���A�e�v���C���[�̏��\���p�N���X�B
// �C���X�^���X�͊��ɂ�2�A�܂莩�@�ƓG�@��1���B
// ���̃N���X�̓Q�[���̋����ɉe����^���邱�Ƃ͂Ȃ��A�����w�߂��󂯂ĕ\����ς��邾���B
public class PlayerDisplay : MonoBehaviour
{
    // �v���C���[��
    public string nickName = "";

    // ���̃v���C���[�̏e�m�A�@�e�A�Z�\�I�u�W�F�N�g
    public Gunner gunner;
    public Gun rightGun;
    public Gun leftGun;
    public Skill skill;

    // �^�[���v���C���[���ۂ�
    public bool isTurn;

    // �v���C���[�̊e��\��
    public TMP_Text nicknameDisplay;
    public TMP_Text HP_Display;
}
