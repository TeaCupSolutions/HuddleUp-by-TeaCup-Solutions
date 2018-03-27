namespace StaticValuesNamespace
{
    public static class StaticValues
{
    private static bool isReplay, isAtStartup = true;
    private static string replayName;
    private static string replayBaseDir;

    static StaticValues()
    {
        isReplay = false;
        replayName = "";
        replayBaseDir = "RecordedInput";
    }

    public static bool IsReplay
    {
        get
        {
            return isReplay;
        }
        set
        {
            isReplay = value;
        }
    }

    public static string ReplayName
    {
        get
        {
            return replayName;
        }
        set
        {
            replayName = value;
        }
    }

    public static string ReplayBaseDir
    {
        get
        {
            return replayBaseDir;
        }
        set
        {
            replayBaseDir = value;
        }
    }

    public static bool IsAtStartup
    {
            get
            {
                return isAtStartup;
            }
            set
            {
                isAtStartup = value;
            }
    }
    }
}