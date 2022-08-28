using HarmonyLib;
using Honeymoon;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.ScriptLoader.Script
{
    public static class TaskUnitIsEnabled
    {
        static Harmony instance;

        public static void Main()
        {
            if (instance == null)
                instance = Harmony.CreateAndPatchAll(typeof(TaskUnitIsEnabled));
        }

        public static void Unload()
        {
            instance?.UnpatchAll(instance.Id);
            instance = null;
        }

        [HarmonyPatch(typeof(TaskUnit), "isEnabled", MethodType.Setter)]
        [HarmonyPrefix]
        public static void isEnabledPre(ref bool __0)
        {
            __0 = true;
        }
    }
}
