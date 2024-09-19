using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GunsData : ScriptableObject
{
    public enum GunType
    {
        LIGHT,
        HEAVY,
        SPECIAL
    }

    [SerializeField] private GameManager.CARD_POOL cardPool;
    [SerializeField] private string gunId;
    [SerializeField] private Sprite gunImage;
    [SerializeField] private string gunName;
    [SerializeField] private Sprite gunLabel;
    [TextArea(3, 10)]
    [SerializeField] private string gunAbility;
    [TextArea(3, 10)]
    [SerializeField] private string gunFlavorText;
    [SerializeField] private int maximumBulletCapacity;
    [SerializeField] private int maximumReloadPerTurn;
    [SerializeField] private int gunWeight;
    [SerializeField] private GunType gunType;
    [SerializeField] private int[] bulletsToFire;
    [SerializeField] private string[] hitOfFire;
    [SerializeField] private string[] sideEffectOfFire;
    [SerializeField] private float iconWidth;
    [SerializeField] private float iconHeight;
    [SerializeField] private float iconBoundsWidth;
    [SerializeField] private Vector3 gunImageScale;
    [SerializeField] private CardDatabase deckDatabase;
    [SerializeField] private Sprite gunDetails;

    // 紐づいているEffectHub
    public EffectHub effectHub;

    // 射撃に必要な装填数をKeyに、射撃時の効果をValueにもつDictionary型のオブジェクト
    private Dictionary<int, string[]> bulletsToEffects;

    public GameManager.CARD_POOL GetCardPool() { return cardPool; }
    public string GetGunId() {  return gunId; }
    public Sprite GetGunImage() {  return gunImage; }
    public string GetGunName() {  return gunName; }
    public Sprite GetGunLabel() { return gunLabel; }
    public string GetGunAbility() {  return gunAbility.Replace("\\n","\n"); }
    public string GetGunFlavorText() { return gunFlavorText.Replace("\\n", "\n"); }
    public int GetMaximumBulletCapacity() {  return maximumBulletCapacity; }
    public int GetMaximumReloadPerTurn() {  return maximumReloadPerTurn; }
    public int GetGunWeight() {  return gunWeight; }
    public GunType GetGunType() {  return gunType; }
    public int[] GetBulletsToFire() {  return bulletsToFire; }
    public string[] GetHitOfFire() {  return hitOfFire; }
    public string[] GetSideEffectOfFire() {  return sideEffectOfFire; }
    public Dictionary<int, string[]> GetBulletsToEffects()
    { 
        bulletsToEffects = new Dictionary<int, string[]>();
        for(int i = 0; i < sideEffectOfFire.Length; i++)
        {
            string[] resultOfFire = { hitOfFire[i], sideEffectOfFire[i] };
            bulletsToEffects.Add(bulletsToFire[i], resultOfFire);
        } 
        return bulletsToEffects;
    }
    public float GetIconWidth() {  return iconWidth; }
    public float GetIconHeight() {  return iconHeight; }
    public float GetIconBounds() { return iconBoundsWidth; }
    public Vector3 GetImageScale() { return gunImageScale; }
    public CardDatabase GetDeckDatabase() { return deckDatabase; }

    public Sprite GunDetails { get { return gunDetails; } }

}
