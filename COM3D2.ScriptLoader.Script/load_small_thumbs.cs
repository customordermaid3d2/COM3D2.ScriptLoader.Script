// #author ghorsington modified PainBrioché
// #name Small thumbnails from files
// #desc Allows to swap small maid thumbnails like big ones from Thumb folder

using UnityEngine;
using HarmonyLib;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

public static class LoadSmallThumbs {
    static Harmony instance;

    public static void Main() {
        if (instance == null)
            instance = Harmony.CreateAndPatchAll(typeof(LoadSmallThumbs));
    }

    public static void Unload() {
        instance?.UnpatchAll(instance.Id);
        instance = null;
    }

    [HarmonyPatch(typeof(Maid), "SetThumCard")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> SetThumCardTranspiler(IEnumerable<CodeInstruction> instrs) {
        foreach (var ins in instrs) {
            if(ins.opcode == OpCodes.Call && ((MethodInfo)ins.operand).Name == "GetThumIcon")
                yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(Maid), "m_texIcon"));
            else
                yield return ins;
        }
    }

    [HarmonyPatch(typeof(Maid), "GetThumIcon")]
    [HarmonyPrefix]
    public static void GetThumIconPrefix(Maid __instance) {
        if(__instance.status == null)
            return;

        var thumbPath = Path.Combine(UTY.gameProjectPath, "Thumb");
        if (!Directory.Exists(thumbPath))
            Directory.CreateDirectory(thumbPath);

        string iconThumPath;

        var icons = Directory.GetFiles(thumbPath, $"icon_thum_{__instance.status.guid}.png", SearchOption.AllDirectories);
        if (icons.Length == 1)
        {
            iconThumPath = icons[0];
        }            
        else
        {
            return;
        }  

        var name = $"icon_thumb_{File.GetLastWriteTime(iconThumPath).Ticks}";

        if (__instance.m_texIcon != null && __instance.m_texIcon.name == name)
            return;
        
        if(__instance.m_texIcon != null)
            UnityEngine.Object.Destroy(__instance.m_texIcon);
        __instance.m_texIcon = UTY.LoadTexture(iconThumPath);
        __instance.m_texIcon.name = name;
    }
	
    [HarmonyPatch(typeof(Maid), "GetThumCard")]
    [HarmonyPrefix]
    public static bool GetThumCardPrefix(Maid __instance, ref Texture2D __result)
    {
        Texture2D result = null;
        string thumbnailDictionary = Path.Combine(GameMain.Instance.SerializeStorageManager.StoreDirectoryPath, "Thumb");
        if (!Directory.Exists(thumbnailDictionary))
        {
            Directory.CreateDirectory(thumbnailDictionary);
        }

        string[] thumbs = Directory.GetFiles(thumbnailDictionary, $"_tmp_thum_{__instance.status.guid}.png", SearchOption.AllDirectories);
        if (thumbs.Length >= 1)
        {
            result = UTY.LoadTexture(thumbs[0]);
        }
        else
        {
            thumbs = Directory.GetFiles(thumbnailDictionary, $"{__instance.status.guid}.png", SearchOption.AllDirectories);
            if (thumbs.Length >= 1)
            {
                result = UTY.LoadTexture(thumbs[0]);
            }
        }
        __result = result;
        return false;
    }
}