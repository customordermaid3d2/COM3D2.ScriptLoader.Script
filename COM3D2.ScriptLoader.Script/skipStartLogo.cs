// #author Doc
// #name skipStartLogo
// #desc Simple logo skipping.

using UnityEngine;
using HarmonyLib;
using BepInEx;

public static class SkipStartLogo {

    static Harmony instance;

    public static void Main() 
	{
        instance = Harmony.CreateAndPatchAll(typeof(SkipStartLogo));
    }

    public static void Unload() 
	{
        instance.UnpatchAll(instance.Id);
        instance = null;
    }

    [HarmonyPatch(typeof(SceneWarning), "GetAnyMouseAndKey")]
	[HarmonyPatch(typeof(SceneLogo), "GetAnyMouseAndKey")]
    [HarmonyPrefix]
    public static bool HookLoadingEnd(ref bool __result) 
	{
		__result = true;
		
		return false;
    }
}