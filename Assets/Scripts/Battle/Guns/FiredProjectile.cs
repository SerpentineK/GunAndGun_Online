using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredProjectile : Object
{
    // ����ɗ^����_���[�W
    public int HIT;
    // �����ɗ^����_���[�W
    public int RECOIL;
    // ����̃Z�b�g����Ă���J�[�h�A���U�A�{���e�[�W�A�܂��̓R�X�g1�ȉ��́y�@�\�z�����̐��܂Ŏ̂ĎD�ɒu��
    public int BREAK;
    // ����Ƀo�[���J�E���^�[(�^�[���J�n���e�J�E���^�[�ɂ�3�_���[�W)�����̐��u��
    public int BURN;
    // ����̎�D���炱�̐��̂ĎD�ɒu��
    public int JOLT;
    

    public enum SideEffect 
    {
        // ����Ƀ_���[�W��^�������A���̕������񕜂���
        RECOVER,
        // ����Ƀ_���[�W��^�������A���̕���������̎R�D�̏ォ��̂ĎD�ɒu��
        ANNIHILATION,
        // �ˌ������ꍇ�A�^�[���I�����Ɏ̂ĎD����J�[�h��1����D�ɉ�����
        RETRIEVAL
    }

    public SideEffect sideEffect;
}
