using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ローカルな設定、つまり音量とかアスペクト比とかの設定を保存/反映するためのクラス
public class LocalSettings : MonoBehaviour
{
    // 音量設定
    // getとsetを使うことにより無理やり特定の値以外受け付けない変数を作っている

    [SerializeField] private int _masterVolume = 100;
    [SerializeField] private int _bgmVolume = 100;
    [SerializeField] private int _sfxVolume = 100;


    public int MasterVolume
    {
        get { return _masterVolume; }
        set
        {
            if (0 > value) { _masterVolume = 0; }
            else if (100 < value) { _masterVolume = 100; }
            else { _masterVolume = value; }
        }
    }
    public int BgmVolume
    {
        get { return _bgmVolume; }
        set
        {
            if (0 > value) { _bgmVolume = 0; }
            else if (100 < value) { _bgmVolume = 100; }
            else { _bgmVolume = value; }
        }
    }
    public int SfxVolume
    {
        get { return _sfxVolume; }
        set
        {
            if (0 > value) { _sfxVolume = 0; }
            else if (100 < value) { _sfxVolume = 100; }
            else { _sfxVolume = value; }
        }
    }

    public bool muteAll = false;

    // 操作設定

    public enum CardUsageMethod
    {
        DragToCenter,
        DoubleClick
    }

    public CardUsageMethod cardUsageMethod = CardUsageMethod.DragToCenter;
    public bool singularCardConfirmation = true;
    public bool selectionByNumberKey = false;

    // 画面設定
    // getとsetを使うことにより無理やり特定の値以外受け付けない変数を作っている・セカンドシーズン

    public readonly string[] aspectRatioArray = { "4x3", "16x9", "16x10" };
    public readonly int[] maxFramerateArray = { 24, 30, 60, 120, 144, 240 };

    [SerializeField] private string _aspectRatio = "16x9";
    [SerializeField] private int _maxFramerate = 60;

    public string AspectRatio
    {
        get { return _aspectRatio; }
        set
        {
            if (aspectRatioArray.Contains(value)) { _aspectRatio = value; }
            else { return; }
        }
    }
    public int MaxFramerate
    {
        get { return _maxFramerate; }
        set
        {
            if (maxFramerateArray.Contains(value)) { _maxFramerate = value; }
            else { return; }
        }
    }
    public bool vsyncEnabled = true;

    // その他の設定

    public enum SpecialEffectHandling
    {
        ShowAll,
        FastMode,
        SkipNormalEffects,
        SkipAll
    }

    public enum InformationDisplay
    {
        RightClick,
        Hold,
        SingleClick
    }

    public SpecialEffectHandling specialEffectHandling = SpecialEffectHandling.ShowAll;
    public InformationDisplay informationDisplay = InformationDisplay.RightClick;
}
