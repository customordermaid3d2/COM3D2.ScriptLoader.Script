using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.ScriptLoader.Script
{
    internal class SimpleExperienceSystemSetMax
    {
        static Harmony instance;

        public static void Main()
        {
            instance = Harmony.CreateAndPatchAll(typeof(SimpleExperienceSystemSetMax));
        }

        public static void Unload()
        {
            instance?.UnpatchAll(instance.Id);
            instance = null;
        }

        [HarmonyPatch(typeof(SimpleExperienceSystem), MethodType.Constructor, new Type[] { } )]
        [HarmonyPatch(typeof(SimpleExperienceSystem), MethodType.Constructor, typeof(List<int>))]
        [HarmonyPatch(typeof(SimpleExperienceSystem), MethodType.Constructor, typeof(SimpleExperienceSystem))]
        [HarmonyPostfix]
        public static void CharacterSelectManagerPost(SimpleExperienceSystem __instance)
        {
            __instance.AddExp(__instance.GetMaxLevelNeedExp());
        }
    }
}
