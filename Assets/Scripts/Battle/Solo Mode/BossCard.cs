using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCard : Card
{
    // このメタカードに紐づいている視認可能なカード
    public BossVisibleCard bossVisibleCard;

    // このメタカードに紐づいているEffectHub、つまり効果群
    public EffectHub[] cardEffectHubs;

    // データ受け渡し用のプロパティ
    public string cardNameENG;
    public string[] effectTexts;
}
