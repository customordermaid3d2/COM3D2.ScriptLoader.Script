// #author Doc
// #name NameExtender
// #desc BepinEx Script conversion of NameExtender. Just allows you to write longer names. Fixes some issues.

using UnityEngine;
using HarmonyLib;
using BepInEx;

public static class NameExtender {

    static Harmony instance;
	static int newLength = 20; 

    public static void Main() 
	{
		if (instance == null)
        instance = Harmony.CreateAndPatchAll(typeof(NameExtender));
    }

    public static void Unload() 
	{
        instance?.UnpatchAll(instance.Id);
        instance = null;
    }

    [HarmonyPatch(typeof(UIInput), "Init")]
    [HarmonyPostfix]
    public static void InputConstructHook(ref UIInput __instance) 
	{
		if (__instance.characterLimit < newLength)
		{
			__instance.characterLimit = newLength;
		}
    }
	
	[HarmonyPatch(typeof(ProfileCtrl), "AdjustStrLengthIfOver")]
	[HarmonyPatch(typeof(MaidStatus.Status), "ConvertString")]
    [HarmonyPrefix]
	public static void HookTween(ref int __1)
	{	
		if (__1 < newLength)
		{
			__1 = newLength;
		}
	}
}