using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunTarget : MonoBehaviour
{
    // この機銃のGunsData
    public GunsData data;

    // データ入力用の領域
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text nameArea;
    [SerializeField] private GameObject abilityLabel;
    [SerializeField] private TMP_Text abilityArea;
    [SerializeField] private TMP_Text maxBulletArea;
    [SerializeField] private TMP_Text maxReloadArea;
    [SerializeField] private TMP_Text typeArea;
    [SerializeField] private TMP_Text weightArea;
    [SerializeField] private GameObject fireBulletsLabel;
    [SerializeField] private GameObject fireEffectsLabel;
    [SerializeField] private FireObject[] fireObjects;
    [SerializeField] private TMP_Text flavorTextArea;
    [SerializeField] private GunConfirmButton confirmButton;
    [SerializeField] private GameObject inavailableDisplay;

    public void InputGunData()
    {
        gameObject.name = data.GetGunId()+"_Target";
        spriteRenderer.sprite = data.GetGunImage();
        nameArea.SetText(data.GetGunName());
        string ability = data.GetGunAbility();
        // アビリティがあれば表示
        if (ability!="") { abilityArea.SetText(ability); }
        // なければアビリティ欄を消去
        else { 
            if (abilityLabel.activeSelf) { abilityLabel.SetActive(false); } 
            if (abilityArea.gameObject.activeSelf) { abilityArea.gameObject.SetActive(false); }
        }
        maxBulletArea.SetText(string.Format("{0:00}", data.GetMaximumBulletCapacity()));
        maxReloadArea.SetText(string.Format("{0:00}", data.GetMaximumReloadPerTurn()));
        typeArea.SetText(data.GetGunType().ToString());
        weightArea.SetText(string.Format("{0:00}", data.GetGunWeight()));
        var bullets = data.GetBulletsToFire();
        var hits = data.GetHitOfFire();
        var effects = data.GetSideEffectOfFire();

        foreach (var item in fireObjects)
        {
            if (item.gameObject.activeSelf) { item.gameObject.SetActive(false); }
        }
        
        if (bullets.Length == 0)
        {
            if (fireEffectsLabel.activeSelf) { fireEffectsLabel.SetActive(false); }
            if (fireBulletsLabel.activeSelf) { fireBulletsLabel.SetActive(false); }
        }

        for (int i = 0; i < bullets.Length; i++)
        {
            string fireText;
            string hit = hits[i];
            string effect = effects[i];
            FireObject obj=fireObjects[i];
            fireText = "HIT:" + hit + " (" + effect + ")";
            if (hit.Equals("0")) { fireText = effect; }
            else if (effect.Equals("N/A")) {  fireText = "HIT:"+hit; }
            obj.bulletsRequired = bullets[i];
            obj.effectOfFire = fireText;
            obj.InitializeFireObject();
        }

        flavorTextArea.SetText(data.GetGunFlavorText());

        confirmButton.confirmedData = data;
    }

    public void SetInavailableForTarget()
    {
        if (confirmButton.gameObject.activeSelf) { confirmButton.gameObject.SetActive(false); }
        if (!inavailableDisplay.activeSelf) { inavailableDisplay.SetActive(true); }
    }
}
