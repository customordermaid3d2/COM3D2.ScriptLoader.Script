using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using HarmonyLib;


namespace COM3D2.ScriptLoader.Script
{
    internal class SceneFreeModeSelectManagerYotogi
    {
        static Harmony instance;

        public static void Main()
        {
            if (instance == null)
                instance = Harmony.CreateAndPatchAll(typeof(SceneFreeModeSelectManagerYotogi));
        }

        public static void Unload()
        {
            instance?.UnpatchAll(instance.Id);
            instance = null;
        }

        [HarmonyPatch(typeof(SceneFreeModeSelectManager), "Start")]
        [HarmonyPostfix]
        public static void Start(SceneFreeModeSelectManager __instance)
        {
            GameObject gameObject = __instance.gameObject.transform.parent.gameObject;
            GameObject childObject3 = UTY.GetChildObject(gameObject, "MenuSelect/Menu/FreeModeMenuButton/夜伽", false);
            if (childObject3.gameObject.activeSelf)
            {
                UIButton component3 = childObject3.GetComponent<UIButton>();
                component3.isEnabled = true;
            }
        }
    }
}
