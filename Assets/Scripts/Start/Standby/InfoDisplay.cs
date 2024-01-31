using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoDisplay : MonoBehaviour
{
    public SpriteRenderer gunnerSprite;
    public SpriteMask gunnerMask;
    public SpriteRenderer gun01Sprite;
    public SpriteMask gun01Mask;
    public SpriteRenderer gun02Sprite;
    public SpriteMask gun02Mask;

    public TMP_Text playerNameText;
    public TMP_Text gunnerText;
    public TMP_Text gun01Text;
    public TMP_Text gun02Text;

    public GunnerData gunnerData;
    public GunsData gun01Data;
    public GunsData gun02Data;

    public void InputData()
    {
        if (gunnerData != null)
        {
            if (gunnerMask.gameObject.activeSelf)
            {
                gunnerMask.gameObject.SetActive(false);
            }
            if (!gunnerSprite.gameObject.activeSelf)
            {
                gunnerSprite.gameObject.SetActive(true);
            }
            gunnerSprite.sprite = gunnerData.GetGunnerImage();
            gunnerText.SetText(gunnerData.GetGunnerName());
        }
        if (gun01Data != null)
        {
            if (gun01Mask.gameObject.activeSelf)
            {
                gun01Mask.gameObject.SetActive(false);
            }
            if (!gun01Sprite.gameObject.activeSelf)
            {
                gun01Sprite.gameObject.SetActive(true);
            }
            gun01Sprite.sprite = gun01Data.GetGunImage();
            gun01Text.SetText(gun01Data.GetGunName());
        }
        if (gun02Data != null)
        {
            if (gun02Mask.gameObject.activeSelf)
            {
                gun02Mask.gameObject.SetActive(false);
            }
            if (!gun02Sprite.gameObject.activeSelf)
            {
                gun02Sprite.gameObject.SetActive(true);
            }
            gun02Sprite.sprite = gun02Data.GetGunImage();
            gun02Text.SetText(gun02Data.GetGunName());
        }
    }
}
