// #author ghorsington
// #name Report duplicates
// #desc Reports duplicate .menu files in the Mod folder and asks a user to delete them

using UnityEngine;
using HarmonyLib;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;

public static class DedupeMenus {
    static Harmony instance;
    static string[] dupes;

    public static void Main() {
		if (instance == null)
        instance = Harmony.CreateAndPatchAll(typeof(DedupeMenus));
    }

    public static void Unload() {
        instance?.UnpatchAll(instance.Id);
        instance = null;
        dupes = null;
    }

    private static string[] GetDupes(string[] arr) {
        var freqDict = new Dictionary<string, int>();

        foreach (var s in arr)
            freqDict[s] = freqDict.TryGetValue(s, out int value) ? value + 1 : 1;
        
        return freqDict.Where(kv => kv.Value != 1).Select(kv => kv.Key).ToArray();
    }

    [HarmonyPatch(typeof(GameUty), "Init")]
    [HarmonyPostfix]
    public static void InitPostfix() {
        Debug.Log("Collecting dupes");
        dupes = GetDupes(GameUty.m_aryModOnlysMenuFiles);
    }

    [HarmonyPatch(typeof(TitleCtrl), "Init")]
    [HarmonyPostfix]
    public static void TitleInitPostfix() {
        if(dupes != null && dupes.Length != 0) {
            var savePath = Path.Combine(UTY.gameProjectPath, "menu_dupes.log");
            File.WriteAllLines(savePath, dupes);
			if(!GameMain.Instance.SysDlg.isActiveAndEnabled)
			{
            GameMain.Instance.SysDlg.Show(  "WARNING\n" + 
                                            "There are duplicate .menu files in Mod folder. " +
                                            "This might cause duplicated items icons in game. Please remove the duplicates.\n\n" + 
                                            $"List of all found duplicates was saved to {savePath}", SystemDialog.TYPE.OK);
			}
            dupes = null;
        }
    }
}