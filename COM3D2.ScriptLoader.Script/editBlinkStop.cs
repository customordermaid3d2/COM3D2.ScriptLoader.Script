// #author Doc
// #name EditBlinkStop
// #desc A simpler but more solid implementation of EditBlinkStop. Completely disables blinking in Edit Mode.

using UnityEngine;
using UnityEngine.SceneManagement;
using HarmonyLib;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Reflection;
using System;

public static class EditBlinkStop2
{
	static Harmony instance;
	
	//Simple control bool for our property getter.
	static bool InEditMode;

	//Simple property getter. We're using a property because it allows for more versatile condition returns and I'm not sure if we may have more logic later.
	public static bool AllowBlinking
	{
		get
		{
			return !InEditMode;
		}
	}

	//Basic patch code. Also subscribes to level changes so we can tell when we're in Edit Mode.
	public static void Main()
	{
		instance = Harmony.CreateAndPatchAll(typeof(EditBlinkStop2));
		
		SceneManager.sceneLoaded += OnSceneLoaded;
		
	}

	//Basic unload code complete with unsubscribing.
	public static void Unload()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		instance?.UnpatchAll(instance.Id);
		instance = null;
	}
	
	//Simple load scene event handler
	public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name.Equals("SceneEdit"))
		{
			
			InEditMode = true;
			
		} 
		else 
		{
			
			InEditMode = false;
			
		}
	}

	//Where the magic happens. All it does is inject a conditional to the if(this.boMabataki) check converting it into if(this.boMabataki && EditBlinkStop2.AllowBlinking) 
	[HarmonyPatch(typeof(Maid), "Update")]
	[HarmonyTranspiler]
	public static IEnumerable<CodeInstruction> AddConditional(IEnumerable<CodeInstruction> instrs)
	{

		//Initial checkpoint puts us where we want, which is looking at the Brfalse opcode.
		var checkpoint = new CodeMatcher(instrs)
			.MatchForward(true,
				new CodeMatch(OpCodes.Ldarg_0),
				new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(Maid), "boMabataki")),
				new CodeMatch(OpCodes.Brfalse)
			);

		//Snag the instruction of brfalse so we can reuse it's operand (aka where to jump if AllowBlinking turns out to be false).
		var instruct = checkpoint.Instruction;

		//Inject our conditional. Easy and simple, taking care to advance 1 so we're injecting after brfalse.
		var result =
			checkpoint
			.Advance(1)
			.Insert(
				new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(EditBlinkStop2), "AllowBlinking")),
				new CodeInstruction(OpCodes.Brfalse, instruct.operand)
			)
			.InstructionEnumeration();

		return result;
	}
}