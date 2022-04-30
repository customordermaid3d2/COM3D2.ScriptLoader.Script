// #author ghorsington
// #name Add subs to old yotogi
// #desc Add simple subtitles to Neigh Service when running in CM mode

using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;

public static class MemoriesModeUnlock {

    static Harmony instance;

    public static void Main() {
		if (instance == null)
			instance = Harmony.CreateAndPatchAll(typeof(MemoriesModeUnlock));
    }

    public static void Unload() {
		instance?.UnpatchAll(instance.Id);
        instance = null;
    }
	
	[HarmonyPatch(typeof(FreeModeItemEveryday), "IsEnabledFlag")]
	[HarmonyPrefix]
	//public static bool IsEnabledFlag(FreeModeItemEveryday.ScnearioType type, string flag_name)
	public static bool IsEnabledFlag(ref bool __result)
	{
		__result = true;
		return false;
	}
	

	
	[HarmonyPatch(typeof(FreeModeItemVip), "is_enabled" , MethodType.Getter)]
	[HarmonyPrefix]
	//public static bool IsEnabledFlag(FreeModeItemEveryday.ScnearioType type, string flag_name)
	public static bool is_enabled(ref bool __result)
	{
		__result = true;
		return false;
	}
}