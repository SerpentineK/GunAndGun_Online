
using System.Linq;

// 右上のメニューから開ける設定画面で変更できる設定、その項目を扱うstaticクラス
public static class PlayerSettings
{
    // 音量設定
    // getとsetを使うことにより無理やり特定の値以外受け付けない変数を作っている

    private static int _masterVolume = 100;
    private static int _bgmVolume = 100;
    private static int _sfxVolume = 100;


    public static int MasterVolume 
    {
        get { return _masterVolume; }
        set 
        { 
            if (0 > value) { _masterVolume = 0; }
            else if (100 < value) { _masterVolume = 100; }
            else { _masterVolume = value; }
        }
    }
    public static int BgmVolume
    {
        get { return _bgmVolume; }
        set
        {
            if (0 > value) { _bgmVolume = 0; }
            else if (100 < value) { _bgmVolume = 100; }
            else { _bgmVolume = value; }
        }
    }
    public static int SfxVolume
    {
        get { return _sfxVolume; }
        set
        {
            if (0 > value) { _sfxVolume = 0; }
            else if (100 < value) { _sfxVolume = 100; }
            else { _sfxVolume = value; }
        }
    }

    public static bool muteAll = false;

    // 操作設定

    public enum CardUsageMethod
    {
        DragToCenter,
        DoubleClick
    }
    
    public static CardUsageMethod cardUsageMethod = CardUsageMethod.DragToCenter;
    public static bool singularCardConfirmation = true;
    public static bool selectionByNumberKey = false;

    // 画面設定
    // getとsetを使うことにより無理やり特定の値以外受け付けない変数を作っている・セカンドシーズン

    public static readonly string[] aspectRatioArray = { "4x3", "16x9", "16x10"};
    public static readonly int[] maxFramerateArray = { 24, 30, 60, 120, 144, 240 };
    
    private static string _aspectRatio = "16x9";
    private static int _maxFramerate = 60;

    public static string AspectRatio 
    {
        get { return _aspectRatio; }
        set 
        { 
            if (aspectRatioArray.Contains(value)) { _aspectRatio = value; }
            else { return; } 
        } 
    }
    public static int MaxFramerate 
    {
        get { return _maxFramerate; }
        set {
            if (maxFramerateArray.Contains(value)) { _maxFramerate = value; }
            else { return; }
        }
    }
    public static bool vsyncEnabled = true;

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

    public static SpecialEffectHandling specialEffectHandling = SpecialEffectHandling.ShowAll;
    public static InformationDisplay informationDisplay = InformationDisplay.RightClick;
}
