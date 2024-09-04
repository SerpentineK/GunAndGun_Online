
public static class PlayerSettings
{
    public static bool muteAll = false;
    public static int masterVolume = 100;
    public static int bgmVolume = 100;
    public static int sfxVolume = 100;

    public enum CardUsageMethod
    {
        DragToCenter,
        DoubleClick
    }
    
    public static CardUsageMethod cardUsageMethod;
    public static bool singularCardConfirmation;
    public static bool selectionByNumberKey;

    public static string[] aspectRatioArray = new string[] { "4x3", "16x9", "16x10" };
    public static int[] maxFramerateArray = new int[] { 24, 30, 60, 120, 144, 240 };

    public static string aspectRatio;
    public static int maxFramerate;
    public static bool vsync;


    public static void UpdateSoundSettings(bool mute, int general, int bgm, int sfx)
    {
        muteAll = mute;
        masterVolume = general;
        bgmVolume = bgm;
        sfxVolume = sfx;
    }

    public static void UpdateControlsSettings(CardUsageMethod method, bool confirmation, bool selection)
    {
        cardUsageMethod = method;
        singularCardConfirmation = confirmation;
        selectionByNumberKey = selection;
    }

    
}
