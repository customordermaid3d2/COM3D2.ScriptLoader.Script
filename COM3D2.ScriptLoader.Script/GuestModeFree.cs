using HarmonyLib;
using UnityEngine;
using Kasizuki;


public static class GuestModeFree 
{

    static Harmony instance;

    public static void Main() {
        instance = Harmony.CreateAndPatchAll(typeof(GuestModeFree));
    }

    public static void Unload() {
		if(instance != null)
			instance.UnpatchAll(instance.Id);
        instance = null;
    }

	[HarmonyPatch(typeof(PlayData.Data), "IsPassed", MethodType.Getter)]
	[HarmonyPrefix]
	public static bool IsPassed(ref bool __result,int ___ID,string ___drawName)
	{
		Debug.Log($"PlayData.Data.IsPassed : {___ID} , {__result}, {___drawName}");
		__result=true;
		return false;
	}

}