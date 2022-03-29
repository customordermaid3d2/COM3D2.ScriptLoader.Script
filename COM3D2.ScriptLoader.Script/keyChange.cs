using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;

namespace COM3D2.ScriptLoader.Script
{
    internal class keyChange
    {
        static Harmony instance;

        public static void Main()
        {
            if (instance == null)
                instance = Harmony.CreateAndPatchAll(typeof(keyChange));
        }

        public static void Unload()
        {
            instance?.UnpatchAll(instance.Id);
        }

        [HarmonyPatch(typeof(MessageWindowMgr), "Update")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Start(IEnumerable<CodeInstruction> instructions)
        {
            var Index = -1;

            var codes = new List<CodeInstruction>(instructions);
            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldc_I4_S && (SByte)codes[i].operand == (SByte)KeyCode.Return)// && Enum.Equals(KeyCode.Return, codes[i].operand)
                {
                    //MessageWindowMgr.Update 105 , 13 , System.SByte
                    Debug.LogWarning($"MessageWindowMgr.Update {i} , {codes[i].operand} , {codes[i].operand.GetType()}");
                    Index = i;
                    break;
                }
            }
            if (Index > -1)
            {
                codes[Index].operand = (SByte)KeyCode.Space;
            }
            else
            {
                Debug.LogError($"MessageWindowMgr.Update fail");
            }

            return codes.AsEnumerable();
        }
    }
}
