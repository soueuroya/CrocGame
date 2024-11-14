using UnityEngine;

public sealed class Constants
{
    public class Prefs {
        
        public static string masterVolume = "masterVolume";
        public static string musicCapVolume = "musicCapVolume";
        public static string sfxCapVolume = "sfxCapVolume";
        public static string screenModeKey = "screenModeKey";

        public static string saveFileName = "gamesave.txt";
    }

    public class GameSettings{
        public static float transitionAnimationTime = 0.5f;
    }

    public class Colors
    {
        public static Color idleUiColor = new Color(1f, 0f, 0f, 1f);
        public static Color selectedUiColor = new Color(0.55f, 1f, 0.4f, 1f);
    }
}
