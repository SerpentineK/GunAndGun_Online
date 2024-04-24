using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gunner : MonoBehaviour
{
    // 銃士のデータ
    public GunnerData data;

    // データ入力用の領域
    [SerializeField] private TMP_Text gunnerNameArea;
    [SerializeField] private SpriteRenderer gunnerImageArea;

    // 詳細情報
    public int agility;
    public int hand;
    public EffectHub passiveEffect;
    public string passiveEffectText;
    public string flavorText;
    public bool isPlayer;

    public void InputGunnerData()
    {
        gunnerNameArea.SetText(data.GetGunnerName());
        gunnerImageArea.sprite = data.GetGunnerImage();
        if (isPlayer) { gunnerImageArea.transform.localScale = data.GetPlayerScale(); }
        else { gunnerImageArea.transform.localScale = data.GetOpponentScale(); }
        agility = data.GetGunnerAgility();
        hand = data.GetGunnerHand();
        passiveEffectText = data.GetGunnerAbility();
        flavorText = data.GetGunnerFlavorText();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
