using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Entity
{

    // プレイヤーに帰属するマネージャー
    public FieldManager FM;
    public EffectManager EM;
    public SkillManager SM;

    // StartSceneにて選択されたデータ
    public GunnerData gunnerData;
    public GunsData rightGunsData;
    public GunsData leftGunsData;
    public SkillData skillData;

    // ターンプレイヤーか否か
    public bool isTurn;

    // プレイヤー名
    public string nickName = "";

    // このプレイヤーの銃士、機銃、技能オブジェクト
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
 
    
    /* 
    public void DrawCardsAsRule(Field deck)
    {
        int currentHandNum = FM.hand.cardCount;
        int numToDraw = handNum - currentHandNum;
        if (numToDraw > 0)
        {
            FM.DrawFromDeck(numToDraw, deck);
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
                FM.TransferCard(card.currentField, FM.reaction, card);
            }
            else if (card.cardType == CardData.CardType.Mechanism)
            {
                EM.ActivateMechanism(card);
                FM.TransferCard(card.currentField, FM.mechanism, card);
            }
        }
    }
    */
}
