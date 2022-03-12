using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.MotionWindowAddMyPose.Plugin
{
    internal class MotionWindowAddMyPose
    {
        static Harmony instance;

        public static void Main()
        {
            if (instance == null)
                instance = Harmony.CreateAndPatchAll(typeof(MotionWindowAddMyPose));
        }

        public static void Unload()
        {
            instance?.UnpatchAll(instance.Id);
            instance = null;
        }

        private static void Log(string s)
        {
            Debug.Log("MotionWindowAddMyPose : " + s);
        }

        static KeyValuePair<string, UnityEngine.Object> kp = new KeyValuePair<string, UnityEngine.Object>("マイポーズ", null);

        [HarmonyPatch(typeof(MotionWindow), "AddMyPose")]
        [HarmonyPostfix]
        public static void AddMyPose(MotionWindow __instance, string full_path)
        {
            foreach (var item in __instance.PopupAndTabList.PopUpList.onChangePopUpListValue)
            {
                item.Invoke(kp);
            }
        }
    }
}
