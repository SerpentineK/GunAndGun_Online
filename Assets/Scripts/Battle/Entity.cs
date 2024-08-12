using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Fusion;

// �Q�[�����ɂ����ăJ�[�h���g���Q�[�����v���C�����̂Ƃ��Ĉ����鑶�݂��ׂĂ�����N���X�B
// �{�X�ƃv���C���[�𓯂��g�g�݂ň��������Ƃ����C�������琶�܂ꂽ�B
public class Entity : NetworkBehaviour
{
    // �q�b�g�|�C���g
    public int HP = 30;
    public TMP_Text HP_Counter;

    // �J�E���^�[��
    [HideInInspector] public int burnCounters = 0;
    [HideInInspector] public int shockCounters = 0;

    // ��D����
    public int handNum;

    // �o�t�E�f�o�t
    // �i�����J�[�h�g�p������ˌ��̃_���[�W�𑝂₵���肷��y�e�e�z�͓����Ƒ������邩��ЂƂ܂����O�A�y�@�\�z�͂�������S�������j
    public enum STATUS_ANOMALY
    {
        None = 0,

        // �X�^�b�N����A�܂蓯���ɏd�ˊ|���ł���o�t�E�f�o�t�i�y�@�\�z�͂��������������j
        Stabilizer, // �X�^�r���C�U�[�i���^�[���I�����A�y�e�e�z���̂ĎD����D����1���܂ő��U�j
        Sniper, // �X�i�C�v�i���̃^�[���������HIT��^���邽��1�n���f�X�j
        SpiralShooter, // �X�p�C�����V���[�^�[�i�����̎ˌ��ɂ���ė^����HIT��1���₷�j
        AutoCannon, // �I�[�g�J�m���i���^�[���J�n�������5HIT�A�f�b�L��1�������Ȃ��������I�ɂ̓X�^�b�N�\�j
        HealingTube, // �q�[�����O�`���[�u�i���^�[���I������1�j
        ReloadSupporter, // �����[�h�T�|�[�^�[�i���^�[���I�����f�b�L�̏ォ��1���܂ő��U�j
        PSNegative, // PS/Negative�i���肪�e�m���ʈȊO�ŉ񕜂����ۉ񕜗ʂƓ�����������̎R�D���̂ĎD�ցA�d�ˊ|���\�Ǝv����j
        BoostSet, // �u�[�X�g�Z�b�g�i���^�[���I�����A�f�b�L�̏ォ��3���܂ł��ꂼ��\���������ő��U�j
        LuckySet, // ���b�L�[�Z�b�g�i���^�[���I�����A�f�b�L�̏ォ��1���܂ŕ\���������ő��U�j
        MaintainanceCore, // �����e�i���X�R�A�i���^�[���J�n���A���̃J�[�h���̂Ă�5�񕜁j
        EndorthermicStructure, // �z�M�@�\�i���肪1HIT�ȏ�^����ꂽ�Ƃ�1�񕜁j
        DreadAmp, // �h���b�h�A���v�i���^�[���I�����A�C�ӂ̐��̃v���C���[��I��ŗ��f�b�L�̏ォ��1����VOLT�֑���j
        HighMetronome, // �n�C���g���m�[���i���^�[���J�n���A�S�v���C���[��2�n���f�X�Ǝ������I�񂾃f�b�L����2�h���[�j
        RedlightSystem, // �g�ӌ���i�{���e�[�W�̖����������葽���Ȃ�A�����̎ˌ��ɂ���ė^����HIT��1���₷�j
        ChainReloader, // �`�F�C�������[�_�[�i���^�[���I�����A�ǂ��炩�̃f�b�L�̏ォ��2�������J���A1���𗠌����ő��U�A1������D�ɉ�����j
        SpinShield, // �X�s���V�[���h�i����̎ˌ��ɂ���ė^����HIT��1���炷�j
        AccumulationDevice, // �~�d�@�\�i���^�[���I�����A�f�b�L�̏ォ��1���܂ő��U�j
        ReactorSystem, // �����@�\�i���肪�e�m���ʈȊO�ŎR�D����h���[�����ۑ����2HIT�j
        HighGravitySphere, // ���d�͌����i����̎ˌ��ɂ����3HIT�ȏ�^������Ƃ��A����Ɂy�@�\�z�Ƃ��Ĕ����ς݂̂��̃J�[�h���f�b�L�̉��֒u���Ă悢�j
        OverpressuredSphere, // �ߕ��׌����i���^�[���J�n���A����̃f�b�L�̏ォ��1���̂ĎD�ɒu���j

        // �X�^�b�N���Ȃ��o�t�E�f�o�t
        Burst, // �y�Z�\�z�̃o�[�X�g���ʐ��������B��
        Unconscious, // ���|�e�i�J�[�h�̌��ʂɂ�鑕�U���֎~�j
        Staggered, // �_�C�_���̋@�e���ʂ���уI�[�o�[�N���b�N�i�O�҂͕Е��A��҂͗����̋@�e�̎ˌ��֎~�j
        Echoing, // �G�R�[�o���b�g�i�y�Z�\�z�̔����֎~�j
        Detonated, // �J�V���̋@�e���ʁi�y�Z�\�z�̔����֎~�j
        Frozen, // �t���[�Y�o���b�g�i�J�[�h�̌��ʈȊO�ł̑��U�֎~�j
        Poisoned, // �|�C�Y���o���b�g�i����̃f�b�L����̃h���[�֎~�j
        Intercept, // �C���^�[�Z�v�g�i�ˌ��_���[�W�������j
        Smoke, // �X���[�N�i�S�_���[�W�������j
        Overheat, // �I�[�o�[�q�[�g�i�J�[�h�̌��ʂɂ��h���[�̋֎~�j
        Barrage, // �o���[�W�i����́y�Ή��z�ȊO�̃J�[�h���ʂ𖳌����j
        Limited_Action, // �y���~�b�g�F�s���z
        Limited_Bullet, // �y���~�b�g�F�e�e�z
        DisturbedByNoise, // �m�C�Y�W�F�l���[�^�i�^�[���Ɉꖇ�܂łɁy�s���z�𐧌��j
        Blitz, // �u���b�c�i�^�[���I�����A�^�[�����J��Ԃ��j
        Paralyzed, // �p�����C�Y�o���b�g�i�y�s���z�֎~�j
        MinorError, // �G���[�p�b�`�i�J�[�h�̌��ʂɂ�鑕�U���s��ꂽ�ۂɔ�������ˌ��֎~�A���U�O�̏�ԁj
        FatalError, // �G���[�p�b�`�i�J�[�h�̌��ʂɂ�鑕�U���s��ꂽ�ۂɔ�������ˌ��֎~�A���U��̏�ԁj
        Silenced, // ���e�E�Ái�y�s���z�֎~�j
        Released, // �����E���i�y���~�b�g�z�����j
        Bashed, // �@�����i�Е��̋@�e�̎ˌ��֎~�j
        Immortal, // �s���s���i���ʔ������s�k���Ȃ��j
        CurtainCall, // �J�[�e���R�[���i���ʔ������s�k���Ȃ��j
        ModePhalanx, // ���[�h�E�t�@�����N�X�i���^�[���I���������5HIT�A���̌㎩���̂ǂ��炩�̃f�b�L�̏ォ��5�����̂ĎD�ցj 
        Jamming, // �W���~���O�i�Ή����ꂽ����̎ˌ��܂��͋Z�\�́uHIT��^����ȊO�̌��ʁv�����C���t�F�C�Y�I�����܂Ŗ����ɂ���j
        Recharging // ���`���[�W���[�i����^�[���I�����A��D��5���ɂȂ�悤�ɃJ�[�h�������j
    }

    public List<STATUS_ANOMALY> statusList = new();
    
}
