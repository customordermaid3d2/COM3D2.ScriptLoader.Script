// #author Doc
// #name SaveSettingsInGame
// #desc Conversion for a basic plug that forces the game to save settings upon closing the UI or doing something.

using UnityEngine;
using UnityEngine.SceneManagement;
using HarmonyLib;
using BepInEx;

public static class SaveSettingsInGame {

    static Harmony instance;

    public static void Main() 
	{
		SceneManager.sceneLoaded += SaveSettingsInGame.OnSceneLoaded;
    }

    public static void Unload() 
	{
        instance?.UnpatchAll(instance.Id);
        instance = null;
    }
	
	public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "SceneTitle")
		{
			instance = Harmony.CreateAndPatchAll(typeof(SaveSettingsInGame));
			SceneManager.sceneLoaded -= SaveSettingsInGame.OnSceneLoaded;
		}
	}

    [HarmonyPatch(typeof(ConfigMgr), "CloseConfigPanel")]
    [HarmonyPostfix]
	public static void SaveConfig()
	{
		GameMain.Instance.CMSystem.SaveIni();
	}
	
	[HarmonyPatch(typeof(CMSystem), "SetEditColorPresetData")]
	[HarmonyPatch(typeof(CMSystem), "SetSystemVers")]
	[HarmonyPostfix]
	public static void SaveSystemConfig()
	{	
		GameMain.Instance.CMSystem.SaveSystem();
	}
}