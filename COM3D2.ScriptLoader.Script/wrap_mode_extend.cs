// #author ghorsington
// #name Texture Wrap Extender
// #desc All texture with _twr_ in the name (_twr) at the end are repeated.

using UnityEngine;
using HarmonyLib;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Reflection;

public static class WrapModeExtend {
    //const string TWR_INFIX = "";
    //const string TWR_POSTFIX = "";

    static Harmony instance;

    public static void Main() {
        if (instance == null)
            instance = Harmony.CreateAndPatchAll(typeof(WrapModeExtend));
    }

    public static void Unload() {
        instance?.UnpatchAll(instance.Id);
        instance = null;
    }

    public static TextureWrapMode FixWrapMode(Texture2D tex, TextureWrapMode twm) {
        //if (tex.name.EndsWith(TWR_POSTFIX) || tex.name.Contains(TWR_INFIX))
        return TextureWrapMode.Repeat;
        //return twm;
    }

    [HarmonyPatch(typeof(ImportCM), "ReadMaterial")]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> ReadMaterialTranspiler(IEnumerable<CodeInstruction> instrs, ILGenerator il) {
        var loc = il.DeclareLocal(typeof(TextureWrapMode));
        var target = AccessTools.PropertySetter(typeof(Texture), "wrapMode");
        foreach(var ins in instrs) {
            if(ins.opcode == OpCodes.Callvirt && ((MethodInfo) ins.operand == target)){
                yield return new CodeInstruction(OpCodes.Stloc, loc);
                yield return new CodeInstruction(OpCodes.Dup);
                yield return new CodeInstruction(OpCodes.Ldloc, loc);
                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(WrapModeExtend), "FixWrapMode"));
            }
            yield return ins;
        }
    }
}