using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Fusion;

public class StartSceneManager : MonoBehaviour
{
    BasicControls controls;

    public GameManager.GAME_MODE gameMode;
    public GameManager.SELECTION_TURN selectionTurn;
    public GameManager.CARD_SETS cardSetNumber;
    public GameManager.CARD_POOL[] cardPools;
    public GameManager.CARD_BLOCK cardBlock;

    [SerializeField] private GunnerDataBase gunnerDataBase01;
    [SerializeField] private GunnerDataBase gunnerDataBase02;
    [SerializeField] private GunnerDataBase gunnerDataBase03;
    [SerializeField] private GunnerDataBase gunnerDataBase04;

    [SerializeField] private GunsDataBase gunsDataBase01;
    [SerializeField] private GunsDataBase gunsDataBase02;
    [SerializeField] private GunsDataBase gunsDataBase03;
    [SerializeField] private GunsDataBase gunsDataBase04;

    public static GunnerData opponentGunner = null;
    public static GunsData opponentGun01 = null;
    public static GunsData opponentGun02 = null;
    public static SkillData opponentSkill = null;
    public static GunnerData playerGunner = null;
    public static GunsData playerGun01 = null;
    public static GunsData playerGun02 = null;
    public static SkillData playerSkill = null;

    [SerializeField] private GameObject viewParent;
    [SerializeField] private GameObject[] viewArray;
    [SerializeField] private StandbyView standbyView;
    [SerializeField] private SelectionInfoUI infoUI;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private GunnerSelector gunnerSelector;
    [SerializeField] private GunSelector firstGunSelector;
    [SerializeField] private GunSelector secondGunSelector;
    [SerializeField] private SkillSelector skillSelector;

    private readonly string[] phaseNames = { "none", "銃士選択", "第一機銃選択", "第二機銃選択", "技能選択" };

    private readonly GameManager.CARD_POOL[] block01 = { GameManager.CARD_POOL.GunAndGun, GameManager.CARD_POOL.OverHeat };
    private readonly GameManager.CARD_POOL[] block02 = { GameManager.CARD_POOL.WShout, GameManager.CARD_POOL.UltraBommy };

    public bool GunnerToken { get; private set; } = false;
    public bool FirstGunToken { get; private set; } = false;
    public bool SecondGunToken { get; private set; } = false;
    public bool SkillToken { get; private set; } = false;


    public void InitializeViews()
    {
        InputCardBlock();
        SetControls();
        ChangeToView(0);
        infoUI.displayedGameMode = gameMode;
        infoUI.displayedCardSetNum = cardSetNumber;
        infoUI.displayedCardPool = cardBlock;
        infoUI.SetSelectionText();
    }

    public void SetControls()
    {
        controls ??= new();
        controls.startup.Enable();
    }

    public void DisposeControls()
    {
        controls.startup.Disable();
        controls.Dispose();
    }

    public void InputCardBlock()
    {
        if (Enumerable.SequenceEqual(cardPools, block01))
        {
            cardBlock = GameManager.CARD_BLOCK.BLOCK_01;
            gameMode = GameManager.GAME_MODE.NORMAL;
        }
        else if (Enumerable.SequenceEqual(cardPools, block02))
        {
            cardBlock = GameManager.CARD_BLOCK.BLOCK_02;
            gameMode = GameManager.GAME_MODE.NORMAL;
        }
        else if (cardPools.Length == 1)
        {
            cardBlock = GameManager.CARD_BLOCK.CUSTOM;
            gameMode = GameManager.GAME_MODE.NORMAL;
        }
        else
        {
            cardBlock = GameManager.CARD_BLOCK.CUSTOM;
            gameMode = GameManager.GAME_MODE.UNLIMITED;
        }
    }

    public void SetGunnerPool()
    {
        var gunnerDict = new Dictionary<GameManager.CARD_POOL, GunnerDataBase>()
        {
            {GameManager.CARD_POOL.GunAndGun, gunnerDataBase01},
            {GameManager.CARD_POOL.OverHeat, gunnerDataBase02},
            {GameManager.CARD_POOL.WShout, gunnerDataBase03},
            {GameManager.CARD_POOL.UltraBommy, gunnerDataBase04}
        };
        gunnerSelector.gameMode = gameMode;
        foreach (var item in cardPools)
        {
            gunnerSelector.databases.Add(gunnerDict[item]);
        }
    }

    public void SetGunPool(GunSelector gunSelector)
    {
        var gunsDict = new Dictionary<GameManager.CARD_POOL, GunsDataBase>()
        {
            {GameManager.CARD_POOL.GunAndGun, gunsDataBase01},
            {GameManager.CARD_POOL.OverHeat, gunsDataBase02},
            {GameManager.CARD_POOL.WShout, gunsDataBase03},
            {GameManager.CARD_POOL.UltraBommy, gunsDataBase04}
        };
        gunSelector.gameMode = gameMode;
        foreach (var item in cardPools)
        {
            gunSelector.databases.Add(gunsDict[item]);
        }
    }

    public void UpdateInfoText(int phaseNum)
    {

        playerGunner = LocalPlayerData.GunnerData;


        string nextText = string.Format("PHASE-{0:00} {1}", phaseNum, phaseNames[phaseNum]);
        progressText.SetText(nextText);
        if (opponentGunner != null) { infoUI.opponentGunnerName = opponentGunner.GetGunnerName(); }
        if (opponentGun01 != null) { infoUI.opponentFirstGunName = opponentGun01.GetGunName().Replace(" ", "\n"); }
        if (opponentGun02 != null) { infoUI.opponentSecondGunName = opponentGun02.GetGunName().Replace(" ", "\n"); }
        if (playerGunner != null) { infoUI.playerGunnerName = playerGunner.GetGunnerName(); }
        if (playerGun01 != null) { infoUI.playerFirstGunName = playerGun01.GetGunName().Replace(" ", "\n"); }
        if (playerGun02 != null) { infoUI.playerSecondGunName = playerGun02.GetGunName().Replace(" ", "\n"); }
        infoUI.UpdateSelectionText();
    }

    public void UpdateSelectionData(int phaseNum)
    {
        if (phaseNum == 1)
        {
            SetGunnerPool();
            if (cardSetNumber == GameManager.CARD_SETS.SINGLE) 
            { 
                gunnerSelector.opponentSelectionData = opponentGunner; 
            }
            gunnerSelector.OrganizeGunners();
        }
        else if (phaseNum == 2)
        {
            SetGunPool(firstGunSelector);
            List<GunsData> selectedList = new();
            if (cardSetNumber == GameManager.CARD_SETS.SINGLE)
            {
                selectedList.Add(opponentGun01);
            }
            firstGunSelector.inavailableGunsDataList = selectedList;
            firstGunSelector.OrganizeGuns();
        }
        else if (phaseNum == 3)
        {
            SetGunPool(secondGunSelector);
            List<GunsData> selectedList = new();
            if (cardSetNumber == GameManager.CARD_SETS.SINGLE)
            {
                selectedList.Add(opponentGun01);
                selectedList.Add(opponentGun02);
            }
            selectedList.Add(playerGun01);
            secondGunSelector.inavailableGunsDataList = selectedList;
            secondGunSelector.OrganizeGuns();
        }
        else if (phaseNum == 4)
        {
            skillSelector.selectedGunnerData = playerGunner;
            skillSelector.OrganizeSkills();
        }
    }

    public void UpdateControls(int phaseNum)
    {
        if (phaseNum == 1)
        {
            controls.startup.selectCandidate.performed += ClickInGunnerSelection;
        }
        else if(phaseNum == 2)
        {
            controls.startup.selectCandidate.performed -= ClickInGunnerSelection;
            controls.startup.selectCandidate.performed += ClickInFirstGunSelection;
        }
        else if (phaseNum == 3)
        {
            controls.startup.selectCandidate.performed -= ClickInFirstGunSelection;
            controls.startup.selectCandidate.performed += ClickInSecondGunSelection;
        }
        else if (phaseNum == 4)
        {
            controls.startup.selectCandidate.performed -= ClickInSecondGunSelection;
            controls.startup.selectCandidate.performed += ClickInSkillSelection;
        }
    }

    public void UpdateStandbyView()
    {
        standbyView.playerGunner = playerGunner;
        standbyView.playerGun01 = playerGun01;
        standbyView.playerGun02 = playerGun02;
        standbyView.opponentGunner = opponentGunner;
        standbyView.opponentGun01 = opponentGun01;
        standbyView.opponentGun02 = opponentGun02;
        standbyView.UpdateStandbyInfo();
    }

    public void ChangeToView(int nextPhaseNum)
    {
        GameObject nextView = viewArray[nextPhaseNum];
        foreach (var item in viewArray)
        {
            if (item.activeSelf) { item.SetActive(false); }
        }
        nextView.SetActive(true);
        UpdateSelectionData(nextPhaseNum);
        UpdateInfoText(nextPhaseNum);
        UpdateControls(nextPhaseNum);
    }

    public void ForceUpdateLayoutGroup(HorizontalLayoutGroup layoutGroup)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
    }

    public void UpdateHorizontalLayout(HorizontalLayoutGroup layoutGroup)
    {
        layoutGroup.CalculateLayoutInputHorizontal();
        layoutGroup.SetLayoutHorizontal();
    }

    public void ClickInGunnerSelection(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay((Vector3)Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        //マウスクリックした場所からRayを飛ばし、オブジェクトがあればそれを代入
        if (hit)
        {
            GameObject clickedObject = hit.collider.gameObject;

            // TryGetComponentでGunnerConfirmButtonかGunnerSelectableかを判定する
            if (clickedObject.TryGetComponent(out GunnerConfirmButton clickedButton))
            {
                LocalPlayerData.GunnerData = clickedButton.confirmedData;
                GunnerToken = true;
            }
            else if (clickedObject.TryGetComponent(out GunnerSelectable clickedGunner))
            {
                if (clickedGunner == gunnerSelector.activeSelectable) { return; }
                else if (!clickedGunner.isCurrentlySelected) { clickedGunner.ActivateTarget(); }

                gunnerSelector.activeSelectable?.DeactivateTarget();
                gunnerSelector.activeSelectable = clickedGunner;

                UpdateHorizontalLayout(gunnerSelector.myLayoutGroup);
                Canvas.ForceUpdateCanvases();
                gunnerSelector.mySizeFitter.enabled = false;
                gunnerSelector.mySizeFitter.enabled = true;
            }
        }
        else
        {
            gunnerSelector.activeSelectable?.DeactivateTarget();
            gunnerSelector.activeSelectable = null;

            ForceUpdateLayoutGroup(gunnerSelector.myLayoutGroup);
            Canvas.ForceUpdateCanvases();
            gunnerSelector.mySizeFitter.enabled = false;
            gunnerSelector.mySizeFitter.enabled = true;
        }
    }

    public void ClickInFirstGunSelection(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay((Vector3)Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit)
        {
            GameObject clickedObject = hit.collider.gameObject;

            if (clickedObject.TryGetComponent(out GunConfirmButton clickedButton))
            {
                LocalPlayerData.RightGunsData = clickedButton.confirmedData;
                FirstGunToken = true;
            }
            else if (clickedObject.TryGetComponent(out GunCandidate clickedGun))
            {
                if (clickedGun == firstGunSelector.activeCandidate) { return; }
                else if (!clickedGun.IsCurrentlySelected) { clickedGun.ActivateTarget(); }

                firstGunSelector.activeCandidate?.DeactivateTarget();
                firstGunSelector.activeCandidate = clickedGun;

                UpdateHorizontalLayout(firstGunSelector.myLayoutGroup);
                Canvas.ForceUpdateCanvases();
                firstGunSelector.mySizeFitter.enabled = false;
                firstGunSelector.mySizeFitter.enabled = true;
            }
        }
    }

    public void ClickInSecondGunSelection(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay((Vector3)Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit)
        {
            GameObject clickedObject = hit.collider.gameObject;

            if (clickedObject.TryGetComponent(out GunConfirmButton clickedButton))
            {
                LocalPlayerData.LeftGunsData = clickedButton.confirmedData;
                SecondGunToken = true;
            }
            else if (clickedObject.TryGetComponent(out GunCandidate clickedGun))
            {
                if (clickedGun == secondGunSelector.activeCandidate) { return; }
                else if (!clickedGun.IsCurrentlySelected) { clickedGun.ActivateTarget(); }

                secondGunSelector.activeCandidate?.DeactivateTarget();
                secondGunSelector.activeCandidate = clickedGun;

                UpdateHorizontalLayout(secondGunSelector.myLayoutGroup);
                Canvas.ForceUpdateCanvases();
                secondGunSelector.mySizeFitter.enabled = false;
                secondGunSelector.mySizeFitter.enabled = true;
            }
        }
    }

    public void ClickInSkillSelection(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay((Vector3)Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit)
        {
            GameObject clickedObject = hit.collider.gameObject;

            // TryGetComponentでSkillConfirmButtonかSkillCandidateかを判定する
            if (clickedObject.TryGetComponent(out SkillConfirmButton clickedButton))
            {
                LocalPlayerData.SkillData = clickedButton.data;
                SkillToken = true;
            }
            else if (clickedObject.TryGetComponent(out SpriteRenderer clickedSprite))
            {
                var clickedSkill = clickedSprite.GetComponentInParent<SkillCandidate>();
                if (clickedSkill == skillSelector.activeCandidate) { return; }
                else if ((clickedSkill != null) && (!clickedSkill.IsCurrentlySelected))
                {
                    clickedSkill.ActivateTarget();
                    skillSelector.activeCandidate?.DeactivateTarget();
                    skillSelector.activeCandidate = clickedSkill;
                }

            }
        }
    }
}


