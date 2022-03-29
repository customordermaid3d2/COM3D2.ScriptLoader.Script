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
            instance = null;
        }

        [HarmonyPatch(typeof(MessageWindowMgr), "Update")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Start(IEnumerable<CodeInstruction> instructions)
        {
            try
            {
                CodeMatcher codeMatcher = new CodeMatcher(instructions)
                    .MatchForward(false
                    , new CodeMatch(OpCodes.Call, AccessTools.Method(typeof(Input), "GetKeyDown", new Type[] { typeof(KeyCode) }))
                    );                               

                if (codeMatcher.Pos == codeMatcher.Length)
                {
                    Debug.LogError($"MessageWindowMgr.Update not Match {codeMatcher.Pos} {codeMatcher.Length}");
                    return instructions;
                }

                Debug.LogWarning($"MessageWindowMgr.Update succ patch");

                return codeMatcher
                    .SetInstruction(
                        new CodeInstruction(
                            OpCodes.Call,
                            AccessTools.Method(typeof(keyChange), "GetKeyState", new Type[] { typeof(KeyCode) })
                            )
                    )
                    .InstructionEnumeration();
            }
            catch (Exception e)
            {
                Debug.LogError($"MessageWindowMgr.Update {e}");
                return instructions;
            }
        }

        public static bool GetKeyState(KeyCode keyCode)
        {
            return Input.GetKeyDown(keyCode) || Input.GetKeyDown(KeyCode.Space);
        }

    }
}
