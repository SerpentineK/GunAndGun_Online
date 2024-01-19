using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionInfoUI : MonoBehaviour
{
    public GameManager.GAME_MODE displayedGameMode;
    public GameManager.CARD_SETS displayedCardSetNum;
    public GameManager.CARD_BLOCK displayedCardPool;
    public string opponentGunnerName = "???";
    public string opponentFirstGunName = "???";
    public string opponentSecondGunName = "???";
    public string playerGunnerName = "???";
    public string playerFirstGunName = "???";
    public string playerSecondGunName = "???";
    [SerializeField] private TMP_Text gameModeArea;
    [SerializeField] private TMP_Text cardSetArea;
    [SerializeField] private TMP_Text cardPoolArea;
    [SerializeField] private TMP_Text opponentGunnerArea;
    [SerializeField] private TMP_Text opponentFirstGunArea;
    [SerializeField] private TMP_Text opponentSecondGunArea;
    [SerializeField] private TMP_Text playerGunnerArea;
    [SerializeField] private TMP_Text playerFirstGunArea;
    [SerializeField] private TMP_Text playerSecondGunArea;

    public void SetSelectionText()
    {
        gameModeArea.SetText(displayedGameMode.ToString());
        cardSetArea.SetText(displayedCardSetNum.ToString());
        cardPoolArea.SetText(displayedCardPool.ToString().Replace("_"," "));
    }

    public void UpdateSelectionText()
    {
        opponentGunnerArea.SetText(opponentGunnerName);
        opponentFirstGunArea.SetText(opponentFirstGunName);
        opponentSecondGunArea.SetText(opponentSecondGunName);
        playerGunnerArea.SetText(playerGunnerName);
        playerFirstGunArea.SetText(playerFirstGunName);
        playerSecondGunArea.SetText(playerSecondGunName);
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
