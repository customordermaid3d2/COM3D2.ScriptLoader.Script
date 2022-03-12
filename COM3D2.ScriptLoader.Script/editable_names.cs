/*
    Allows to edit maid names in Maid Edit after they have been created.

    Needs ScriptLoader to work.
 */

using UnityEngine;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

public static class EditableNames
{
    public static void Main()
    {
        Harmony.CreateAndPatchAll(typeof(EditableNames));
    }

    [HarmonyPatch(typeof(ProfileCtrl), "SetEnableInput")]
    [HarmonyPrefix]
    public static void OnSetEnableInput(ref bool enabledInput)
    {
        enabledInput = true;
    }
}