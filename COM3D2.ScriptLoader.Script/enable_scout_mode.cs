// #author ghorsington
// #name Enable scout mode?
// #desc Forces scout mode to be enabled?
using HarmonyLib;
using scoutmode;

public static class EnableScoutMode {
    public static void Main() {
        Harmony.CreateAndPatchAll(typeof(EnableScoutMode));
    }

    [HarmonyPatch(typeof(ScoutManager), "isModeEnabled", MethodType.Getter)]
    [HarmonyPrefix]
    public static bool GetIsScoutMode(ref bool __result) {
        __result = PluginData.IsEnabled("GP001FB");
        return false;
    }
}