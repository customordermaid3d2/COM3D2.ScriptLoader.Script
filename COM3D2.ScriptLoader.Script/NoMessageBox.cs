using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

	public static class NoMessageBox
    {
		static Harmony instance;

		public static void Main()
		{
			if (instance != null)
				instance.UnpatchAll(instance.Id);
			instance = Harmony.CreateAndPatchAll(typeof(NoMessageBox));
		}
		
				public static void Unload()
		{
			if (instance != null)
				instance.UnpatchAll(instance.Id);
			instance = null;
		}
		
		[HarmonyPatch(typeof(NDebug), "MessageBox"), HarmonyPrefix]
		private static bool MessageBox(string f_strTitle, string f_strMsg) // string __m_BGMName 못가져옴
		{			
			Debug.LogError($"{f_strTitle} , {f_strMsg}");
			return false;
		}
	}