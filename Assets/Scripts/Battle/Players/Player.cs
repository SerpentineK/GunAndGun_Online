using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    // �v���C���[�ɋA������}�l�[�W���[
    public FieldManager FM;
    public EffectManager EM;
    public SkillManager SM;

    // StartScene�ɂđI�����ꂽ�f�[�^
    public GunnerData gunnerData;
    public GunsData rightGunsData;
    public GunsData leftGunsData;
    public SkillData skillData;

    // ���_�̃v���C���[���ۂ�
    public bool isProtagonist;

    // �^�[���v���C���[���ۂ�
    public bool isTurn;

    // �q�b�g�|�C���g
    public int HP = 30;
    public TMP_Text HP_Counter;

    // �J�E���^�[��
    [HideInInspector] public int burnCounters = 0;
    [HideInInspector] public int shockCounters = 0;


    // �o�t�E�f�o�t
    // �i�����J�[�h�g�p������ˌ��̃_���[�W�𑝂₵���肷��y�e�e�z�͓����Ƒ������邩��ЂƂ܂����O�A�y�@�\�z�͂�������S�������j

    // �X�^�b�N����A�܂蓯���ɏd�ˊ|���ł���o�t�E�f�o�t�i�y�@�\�z�͂��������������j
    [HideInInspector] public int stabilizers = 0; // �X�^�r���C�U�[�i���^�[���I�����A�y�e�e�z���̂ĎD����D����1���܂ő��U�j
    [HideInInspector] public int snipers = 0; // �X�i�C�v�i���̃^�[���������HIT��^���邽��1�n���f�X�j
    [HideInInspector] public int spiralShooters = 0; // �X�p�C�����V���[�^�[�i�����̎ˌ��ɂ���ė^����HIT��1���₷�j
    [HideInInspector] public int autoCannons = 0; // �I�[�g�J�m���i���^�[���J�n�������5HIT�A�f�b�L��1�������Ȃ��������I�ɂ̓X�^�b�N�\�j
    [HideInInspector] public int healingTubes = 0; // �q�[�����O�`���[�u�i���^�[���I������1�j
    [HideInInspector] public int reloadSupporters = 0; // �����[�h�T�|�[�^�[�i���^�[���I�����f�b�L�̏ォ��1���܂ő��U�j
    [HideInInspector] public int psNegatives = 0; // PS/Negative�i���肪�e�m���ʈȊO�ŉ񕜂����ۉ񕜗ʂƓ�����������̎R�D���̂ĎD�ցA�d�ˊ|���\�Ǝv����j
    [HideInInspector] public int boostSets = 0; // �u�[�X�g�Z�b�g�i���^�[���I�����A�f�b�L�̏ォ��3���܂ł��ꂼ��\���������ő��U�j
    [HideInInspector] public int luckySets = 0; // ���b�L�[�Z�b�g�i���^�[���I�����A�f�b�L�̏ォ��1���܂ŕ\���������ő��U�j
    [HideInInspector] public int maintenanceCores = 0; // �����e�i���X�R�A�i���^�[���J�n���A���̃J�[�h���̂Ă�5�񕜁j
    [HideInInspector] public int endorthermicStructures = 0; // �z�M�@�\�i���肪1HIT�ȏ�^����ꂽ�Ƃ�1�񕜁j
    [HideInInspector] public int dreadAmps = 0; // �h���b�h�A���v�i���^�[���I�����A�C�ӂ̐��̃v���C���[��I��ŗ��f�b�L�̏ォ��1����VOLT�֑���j
    [HideInInspector] public int highMetronomes = 0; // �n�C���g���m�[���i���^�[���J�n���A�S�v���C���[��2�n���f�X�Ǝ������I�񂾃f�b�L����2�h���[�j
    [HideInInspector] public int redLightSystems = 0; // �g�ӌ���i�{���e�[�W�̖����������葽���Ȃ�A�����̎ˌ��ɂ���ė^����HIT��1���₷�j
    [HideInInspector] public int chainReloaders = 0; // �`�F�C�������[�_�[�i���^�[���I�����A�ǂ��炩�̃f�b�L�̏ォ��2�������J���A1���𗠌����ő��U�A1������D�ɉ�����j
    [HideInInspector] public int spinShields = 0; // �X�s���V�[���h�i����̎ˌ��ɂ���ė^����HIT��1���炷�j
    [HideInInspector] public int accumulationDevices = 0; // �~�d�@�\�i���^�[���I�����A�f�b�L�̏ォ��1���܂ő��U�j
    [HideInInspector] public int reactorSystems = 0; // �����@�\�i���肪�e�m���ʈȊO�ŎR�D����h���[�����ۑ����2HIT�j
    [HideInInspector] public int highGravitySpheres = 0; // ���d�͌����i����̎ˌ��ɂ����3HIT�ȏ�^������Ƃ��A����Ɂy�@�\�z�Ƃ��Ĕ����ς݂̂��̃J�[�h���f�b�L�̉��֒u���Ă悢�j
    [HideInInspector] public int overPressuredSpheres = 0; // �ߕ��׌����i���^�[���J�n���A����̃f�b�L�̏ォ��1���̂ĎD�ɒu���j

    // �X�^�b�N���Ȃ��o�t�E�f�o�t
    [HideInInspector] public bool burst = false; // �y�Z�\�z�̃o�[�X�g���ʐ��������B��
    [HideInInspector] public bool unconsicious = false; // ���|�e�i�J�[�h�̌��ʂɂ�鑕�U���֎~�j
    [HideInInspector] public bool staggered = false; // �_�C�_���̋@�e���ʂ���уI�[�o�[�N���b�N�i�O�҂͕Е��A��҂͗����̋@�e�̎ˌ��֎~�j
    [HideInInspector] public bool echoing = false; // �G�R�[�o���b�g�i�y�Z�\�z�̔����֎~�j
    [HideInInspector] public bool detonated = false; // �J�V���̋@�e���ʁi�y�Z�\�z�̔����֎~�j
    [HideInInspector] public bool frozen = false; // �t���[�Y�o���b�g�i�J�[�h�̌��ʈȊO�ł̑��U�֎~�j
    [HideInInspector] public bool poisoned = false; // �|�C�Y���o���b�g�i����̃f�b�L����̃h���[�֎~�j
    [HideInInspector] public bool intercept = false; // �C���^�[�Z�v�g�i�ˌ��_���[�W�������j
    [HideInInspector] public bool smoke = false; // �X���[�N�i�S�_���[�W�������j
    [HideInInspector] public bool overheat = false; // �I�[�o�[�q�[�g�i�J�[�h�̌��ʂɂ��h���[�̋֎~�j
    [HideInInspector] public bool barrage = false; // �o���[�W�i����́y�Ή��z�ȊO�̃J�[�h���ʂ𖳌����j
    [HideInInspector] public bool limited_Action = false; // �y���~�b�g�F�s���z
    [HideInInspector] public bool limited_Bullet = false; // �y���~�b�g�F�e�e�z
    [HideInInspector] public bool disturbedByNoise = false; // �m�C�Y�W�F�l���[�^�i�^�[���Ɉꖇ�܂łɁy�s���z�𐧌��j
    [HideInInspector] public bool blitz = false; // �u���b�c�i�^�[���I�����A�^�[�����J��Ԃ��j
    [HideInInspector] public bool paralyzed = false; // �p�����C�Y�o���b�g�i�y�s���z�֎~�j
    [HideInInspector] public bool minorError = false; // �G���[�p�b�`�i�J�[�h�̌��ʂɂ�鑕�U���s��ꂽ�ۂɔ�������ˌ��֎~�A���U�O�̏�ԁj
    [HideInInspector] public bool fatalError = false; // �G���[�p�b�`�i�J�[�h�̌��ʂɂ�鑕�U���s��ꂽ�ۂɔ�������ˌ��֎~�A���U��̏�ԁj
    [HideInInspector] public bool silenced = false; // ���e�E�Ái�y�s���z�֎~�j
    [HideInInspector] public bool released = false; // �����E���i�y���~�b�g�z�����j
    [HideInInspector] public bool bashed = false; // �@�����i�Е��̋@�e�̎ˌ��֎~�j
    [HideInInspector] public bool immortal = false; // �s���s���i���ʔ������s�k���Ȃ��j
    [HideInInspector] public bool curtainCall = false; // �J�[�e���R�[���i���ʔ������s�k���Ȃ��j
    [HideInInspector] public bool modePhalanx = false; // ���[�h�E�t�@�����N�X�i���^�[���I���������5HIT�A���̌㎩���̂ǂ��炩�̃f�b�L�̏ォ��5�����̂ĎD�ցj 
    [HideInInspector] public bool jamming = false; // �W���~���O�i�Ή����ꂽ����̎ˌ��܂��͋Z�\�́uHIT��^����ȊO�̌��ʁv�����C���t�F�C�Y�I�����܂Ŗ����ɂ���j
    [HideInInspector] public bool recharging = false; // ���`���[�W���[�i����^�[���I�����A��D��5���ɂȂ�悤�ɃJ�[�h�������j


    // ��D����
    public int handNum;

    // ���̃v���C���[�̏e�m�A�@�e�A�Z�\�I�u�W�F�N�g
    public Gunner gunner;
    public Gun rightGun;
    public Gun leftGun;
    public Skill skill;

    public void InputPlayerData() 
    {
        gunner.data = gunnerData;
        gunner.InputGunnerData();
        rightGun.data = rightGunsData;
        rightGun.InputGunData();
        leftGun.data = leftGunsData;
        leftGun.InputGunData();
        skill.data = skillData;
        skill.InputSkillData();
        HP = 30;
        HP_Counter.SetText(string.Format("{0:00}", HP));
        handNum = gunner.hand;
    }
 
    
    public void DrawCardsAsRule()
    {
        int currentHandNum = FM.hand.cardCount;
        int numToDraw = handNum - currentHandNum;
        if (numToDraw > 0)
        {
            FM.DrawFromDeck(numToDraw,FM.leftDeck);
        }
    }

    public void PlayCardFromHand(Card card)
    {
        if (card.currentField != FM.hand)
        {
            return;
        }
        Card.CardStatus status = card.ExamineBeforePlay();
        if (status == Card.CardStatus.PLAYABLE)
        {
            if (card.cardType == CardData.CardType.Action)
            {
                EM.UseAction(card);
                FM.TransferCard(card.currentField, FM.discard, card);
            }
            else if (card.cardType == CardData.CardType.Reaction)
            {
                EM.SetReaction(card);
                FM.TransferCard(card.currentField, FM.set, card);
            }
            else if (card.cardType == CardData.CardType.Mechanism)
            {
                EM.ActivateMechanism(card);
                FM.TransferCard(card.currentField, FM.mechanism, card);
            }
        }
    }
}
