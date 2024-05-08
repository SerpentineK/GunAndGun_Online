using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

// プレハブにくっつけるためのクラス
public class Gun : MonoBehaviour
{
    // データベース上の機銃データ
    public GunsData data;

    // 機銃データを入力するオブジェクト
    [SerializeField] private TMP_Text gunNameObj;
    [SerializeField] private SpriteRenderer gunImageObj;
    [SerializeField] private TMP_Text bulletCounterObj;
    [SerializeField] private TMP_Text reloadCounterObj;
    [SerializeField] private Transform bulletArea;

    // 銃弾アイコンの情報
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Sprite emptyBulletSprite;
    [SerializeField] private Sprite loadedBulletSprite;
    [SerializeField] private Sprite specialBulletSprite;

    // 現在有効なFieldManagerとEffectManager
    public FieldManager FM;
    public EffectManager EM;

    // MagazineのField(機銃に装填されたカードの行先となる領域)
    public MagazineField magazineField;

    // 機銃に結び付いた山札のField
    public DeckField deckField;

    // 機銃のパラメーター(戦闘UIにて参照/更新するもの)
    public int maxBulletCapacity = 0;
    public int maxReloadPerTurn = 0;
    public int currentBullets = 0;
    public int currentReload = 0;

    // 機銃の重さ
    public int gunWeight;

    // 銃弾アイコンオブジェクトの配列
    private GameObject[] bulletIconArray;
    
    // 特別な銃弾(銃弾カード)による装填か否かを判別する配列
    private bool[] isSpecialArray;

    // 機銃の詳細情報(機銃へのクリックで表示するもの)
    private Sprite gunImage;
    private string gunName;
    private string gunId;
    private string gunAbility;
    private string gunFlavorText;
    private Dictionary<int, string[]> bulletsToEffects;

    // 機銃イメージのScale
    private Vector3 gunImageScale;

    // 現在射撃したとき、相手/自分に与える効果
    string[] currentFireEffect;

    // 射撃時に発射された弾丸のオブジェクト(射撃へのHIT修正などはこれに手を加える)
    FiredProjectile projectile;

    // プレイヤー(視点)の機銃か否か
    public bool isPlayer;

    // 左右どちらに装備されているか
    public bool isEquippedToRight;

    // オーバークロック用のプロパティ
    // trueであれば射撃/装填ができず、効果が無効になる(グラフィック弄ってもいいかも)
    public bool isOverclocked = false;

    public void InputGunData()
    {
        // 機銃データを変数へ格納
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

        // 機銃データを要素へ入力
        gameObject.name = "Gun_" + gunId;
        gunNameObj.SetText(gunName);
        gunImageObj.sprite = gunImage;
        gunImageObj.transform.localScale = gunImageScale;
        bulletCounterObj.SetText(string.Format("{0:00}/{1:00}", currentBullets, maxBulletCapacity));
        reloadCounterObj.SetText(string.Format("{0:00}", maxReloadPerTurn));

        // 配列の初期化
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
