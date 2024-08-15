using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

// �J�[�h��e�m�A�@�e�Ȃǂ̃��[�J�����ւ̕\�����i��}�l�[�W���[�B
// �����̔Ֆʏ���BSM����VirtualGameboard�ւ̎Q�Ƃ������Ă���B
public class DisplayManager : MonoBehaviour
{
    private BattleStateManager BSM = BattleStateManager.instance;

    [SerializeField] private PlayerDisplay MyDisplay;
    [SerializeField] private PlayerDisplay OpponentDisplay;
    
    public void InputPlayerData()
    {
        MyDisplay.gunner.data = BSM.myGameboard.MyPlayer.gunnerData;
        MyDisplay.gunner.InputGunnerData();
        MyDisplay.rightGun.data = BSM.myGameboard.MyPlayer.rightGunsData;
        MyDisplay.rightGun.InputGunData();
        MyDisplay.leftGun.data = BSM.myGameboard.MyPlayer.leftGunsData;
        MyDisplay.leftGun.InputGunData();
        MyDisplay.skill.data = BSM.myGameboard.MyPlayer.skillData;
        MyDisplay.skill.InputSkillData();

        MyDisplay.HP_Display.SetText(string.Format("{0:00}", BSM.myGameboard.MyPlayer.HP));
    }
}
