// #author Doc
// #name FastFade
// #desc A safer, simpler FastFade.

using UnityEngine;
using HarmonyLib;
using BepInEx;

public static class FastFade {

    static Harmony instance;

    public static void Main() 
	{
		if (instance == null)
        instance = Harmony.CreateAndPatchAll(typeof(FastFade));
    }

    public static void Unload() 
	{
        instance?.UnpatchAll(instance?.Id);
        instance = null;
    }

    [HarmonyPatch(typeof(CameraMain), "FadeIn")]
	[HarmonyPatch(typeof(CameraMain), "FadeInNoUI")]
	[HarmonyPatch(typeof(CameraMain), "FadeOut")]
	[HarmonyPatch(typeof(CameraMain), "FadeOutNoUI")]
    [HarmonyPrefix]
    public static void HookLoadingEnd(ref float f_fTime) 
	{
		if (f_fTime > 0.1f)
		{
			f_fTime = 0.1f;
		}
    }
	
	[HarmonyPatch(typeof(TweenAlpha), "Begin")]
    [HarmonyPrefix]
	public static void HookTween(ref float duration)
	{	
		if (duration > 0.1f)
		{
			duration = 0.1f;
		}	
	}
}