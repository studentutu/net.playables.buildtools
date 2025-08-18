using Unity.Android.Types;
using UnityEditor;
using UnityEditor.Android;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class PreBuild_AndroidDebugSymbols : IPostprocessBuildWithReport, IPreprocessBuildWithReport
{
    public int callbackOrder { get; }

    DebugSymbolFormat prevFormat;
    DebugSymbolLevel prevLevel;

    public void OnPreprocessBuild(BuildReport report)
    {
        switch (report.summary.platform)
        {
            case BuildTarget.Android:
                prevFormat = UserBuildSettings.DebugSymbols.format;
                prevLevel = UserBuildSettings.DebugSymbols.level;
                UserBuildSettings.DebugSymbols.format = DebugSymbolFormat.IncludeInBundle;
                UserBuildSettings.DebugSymbols.level = DebugSymbolLevel.Full;

                break;
        }
    }

    public void OnPostprocessBuild(BuildReport report)
    {
        if (UserBuildSettings.DebugSymbols.format != prevFormat ||
            UserBuildSettings.DebugSymbols.level != prevLevel)
        {
            UserBuildSettings.DebugSymbols.format = prevFormat;
            UserBuildSettings.DebugSymbols.level = prevLevel;
            AssetDatabase.SaveAssets();
        }
    }
}