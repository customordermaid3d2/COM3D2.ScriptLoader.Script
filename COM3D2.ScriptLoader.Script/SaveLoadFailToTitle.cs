using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.SaveLoadFailToTitle.Script
{
    internal class SaveLoadFailToTitle
    {
        static Harmony instance;

        public static void Main()
        {
            instance = Harmony.CreateAndPatchAll(typeof(SaveLoadFailToTitle));
        }

        public static void Unload()
        {
            instance?.UnpatchAll(instance.Id);
            instance = null;
        }

        [HarmonyPatch(typeof(GameMain), "Deserialize", new Type[] { typeof(int), typeof(bool) })]
        [HarmonyPostfix]
        public static void Deserialize(GameMain __instance, ref bool __result, int f_nSaveNo, bool scriptExec = true)
        {
            if (!__result)
            {
                Debug.LogError("GameMain.Deserialize SaveLoadFailToTitle");
                GameMain.Instance.LoadScene("SceneToTitle");
            }
        }
    }
}
