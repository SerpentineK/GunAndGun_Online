using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

// �v���n�u�ɂ������邽�߂̃N���X
public class Gun : MonoBehaviour
{
    // �f�[�^�x�[�X��̋@�e�f�[�^
    public GunsData data;

    // �@�e�f�[�^����͂���I�u�W�F�N�g
    [SerializeField] private TMP_Text gunNameObj;
    [SerializeField] private SpriteRenderer gunImageObj;
    [SerializeField] private TMP_Text bulletCounterObj;
    [SerializeField] private TMP_Text reloadCounterObj;
    [SerializeField] private Transform bulletArea;

    // �e�e�A�C�R���̏��
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Sprite emptyBulletSprite;
    [SerializeField] private Sprite loadedBulletSprite;
    [SerializeField] private Sprite specialBulletSprite;

    // ���ݗL����FieldManager��EffectManager
    public FieldManager FM;
    public EffectManager EM;

    // Magazine��Field(�@�e�ɑ��U���ꂽ�J�[�h�̍s��ƂȂ�̈�)
    public MagazineField magazineField;

    // �@�e�Ɍ��ѕt�����R�D��Field
    public DeckField deckField;

    // �@�e�̃p�����[�^�[(�퓬UI�ɂĎQ��/�X�V�������)
    public int maxBulletCapacity = 0;
    public int maxReloadPerTurn = 0;
    public int currentBullets = 0;
    public int currentReload = 0;

    // �@�e�̏d��
    public int gunWeight;

    // �e�e�A�C�R���I�u�W�F�N�g�̔z��
    private GameObject[] bulletIconArray;
    
    // ���ʂȏe�e(�e�e�J�[�h)�ɂ�鑕�U���ۂ��𔻕ʂ���z��
    private bool[] isSpecialArray;

    // �@�e�̏ڍ׏��(�@�e�ւ̃N���b�N�ŕ\���������)
    private Sprite gunImage;
    private string gunName;
    private string gunId;
    private string gunAbility;
    private string gunFlavorText;
    private Dictionary<int, string[]> bulletsToEffects;

    // �@�e�C���[�W��Scale
    private Vector3 gunImageScale;

    // ���ݎˌ������Ƃ��A����/�����ɗ^�������
    string[] currentFireEffect;

    // �ˌ����ɔ��˂��ꂽ�e�ۂ̃I�u�W�F�N�g(�ˌ��ւ�HIT�C���Ȃǂ͂���Ɏ��������)
    FiredProjectile projectile;

    // �v���C���[(���_)�̋@�e���ۂ�
    public bool isPlayer;

    // ���E�ǂ���ɑ�������Ă��邩
    public bool isEquippedToRight;

    // �I�[�o�[�N���b�N�p�̃v���p�e�B
    // true�ł���Ύˌ�/���U���ł����A���ʂ������ɂȂ�(�O���t�B�b�N�M���Ă���������)
    public bool isOverclocked = false;

    public void InputGunData()
    {
        // �@�e�f�[�^��ϐ��֊i�[
        gunName = data.GetGunName();
        gunId = data.GetGunId();
        gunWeight = data.GetGunWeight();
        gunImage = data.GetGunImage();
        gunAbility = data.GetGunAbility();
        gunFlavorText = data.GetGunFlavorText();
        gunImageScale = data.GetImageScale();
        maxBulletCapacity = data.GetMaximumBulletCapacity();
        maxReloadPerTurn = data.GetMaximumReloadPerTurn();
        currentBullets = 0;
        currentReload = 0;
        bulletsToEffects = data.GetBulletsToEffects();

        // �@�e�f�[�^��v�f�֓���
        gameObject.name = "Gun_" + gunId;
        gunNameObj.SetText(gunName);
        gunImageObj.sprite = gunImage;
        gunImageObj.transform.localScale = gunImageScale;
        bulletCounterObj.SetText(string.Format("{0:00}/{1:00}", currentBullets, maxBulletCapacity));
        reloadCounterObj.SetText(string.Format("{0:00}", maxReloadPerTurn));

        // �z��̏�����
        bulletIconArray = new GameObject[maxBulletCapacity]; 
        isSpecialArray = new bool[maxBulletCapacity];
        for (int i = 0; i < maxBulletCapacity; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab, bulletArea);
            newBullet.name=string.Format("Bullet_{0:00}", i);
            bulletIconArray[i] = newBullet;
            isSpecialArray[i] = false;
        }
    }

    public void UpdateGunGraphics()
    {
        bulletCounterObj.SetText(string.Format("{0:00}/{1:00}", currentBullets, maxBulletCapacity));
        reloadCounterObj.SetText(string.Format("{0:00}", maxReloadPerTurn-currentReload));
        for(int i = 0;i < maxBulletCapacity; i++)
        {
            if (i < currentBullets) 
            {
                if (isSpecialArray[i])
                {
                    bulletIconArray[i].GetComponentInChildren<Image>().sprite = specialBulletSprite;
                }
                else
                {
                    bulletIconArray[i].GetComponentInChildren<Image>().sprite = loadedBulletSprite;
                }
            }
            else { break; }
        }
    }

    public void ReloadCard(Card cardToReload)
    {
        if (cardToReload.cardType==CardData.CardType.SpecialBullet)
        {
            isSpecialArray[currentBullets] = true;
        }
        else
        {
            isSpecialArray[currentBullets] = false;
        }

        currentBullets++;
        currentReload++;
        
        FM.TransferCard(cardToReload.currentField, magazineField, cardToReload);
        EM.RecieveCue(Effect.EventCue.PlayerReload);
    }



    public string[] GetCurrentFireEffect()
    {
        foreach (var item in bulletsToEffects)
        {
            if (currentBullets >= item.Key) { currentFireEffect = item.Value; }
        }
        return currentFireEffect;
    }

    public void FireBullets() 
    {
        GetCurrentFireEffect();
        var currentHit = currentFireEffect[0];
        var currentSideEffect = currentFireEffect[1];
        projectile = new FiredProjectile();
        projectile.HIT = int.Parse(currentHit);
        if (currentSideEffect.Contains("RECOIL")) { projectile.RECOIL = int.Parse(currentSideEffect); }
        else if (currentSideEffect.Contains("BREAK")) { projectile.BREAK= int.Parse(currentSideEffect); }
        else if (currentSideEffect.Contains("BURN")) { projectile.BURN= int.Parse(currentSideEffect); }
        else if (currentSideEffect.Contains("JOLT")) { projectile.JOLT = int.Parse(currentSideEffect); }
        if (currentSideEffect.Contains("RECOVER")) { projectile.sideEffect = FiredProjectile.SideEffect.RECOVER; }
        else if (currentSideEffect.Contains("ANNIHILATION")) { projectile.sideEffect = FiredProjectile.SideEffect.ANNIHILATION; }
        else if (currentSideEffect.Contains("RETRIEVAL")) { projectile.sideEffect = FiredProjectile.SideEffect.RETRIEVAL; }
        EM.RecieveCue(Effect.EventCue.UponPlayerGunFire, projectile);
    }
}
