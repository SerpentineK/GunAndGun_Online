using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartState : MonoBehaviour, IState
{
    [SerializeField] private GameManager GM;
    [SerializeField] private StartSceneManager SSM;

    // ゲームモード(1セット、2セット、アンリミテッド1セット、アンリミテッド2セット)と
    // カードプール(2ブロック4種類)の決定は対戦部屋を建てる段階でやりたい。
    // （どうやって対戦始まってから決めればいいかわからないので）

    // このStartStateにおいて行いたいのは、
    // 1: (1セットの場合)コイントス
    // 2: 銃士の決定
    // 3: 機銃の決定
    // 4: 技能の決定
    // 5: 戦闘画面への移行

    public int phase { get; private set; }

    // カードセットの数によって銃士・機銃の選択順が異なるため、
    // 以下の変数によってカードセットの数と先手後手を判定する。
    private GameManager.CARD_SETS cardSetNum;
    private GameManager.SELECTION_TURN turnOfSelection;

    public void EnterState()
    {
        // ローディング画面をいずれ作るので、それを表示するフェーズ
        phase = 0;
    }
    public void ExitState()
    {

    }

    public void InState()
    {
        if (phase == 0) 
        {
            SSM.InitializeViews();
            phase++; 
            SSM.ChangeToView(phase);
        }
        else if (phase == 1)
        {
            if (SSM.GunnerToken)
            {
                phase++;
                SSM.ChangeToView(phase);
            }
        }
        else if (phase == 2)
        {
            if (SSM.FirstGunToken) 
            {
                phase++;
                SSM.ChangeToView(phase);
            }
        }
        else if(phase == 3)
        {
            if (SSM.SecondGunToken)
            {
                phase++;
                SSM.ChangeToView(phase);
            }
        }
        else if (phase == 4)
        {
            if (SSM.SkillToken)
            {
                
            }
        }
    }


}
