using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


	public static class PrivateMaidList
	{
		static Harmony instance;
		static HashSet<string> replaceText=new HashSet<string>();

		public static void Main()
		{
			if (instance != null)
				instance.UnpatchAll(instance.Id);
			instance = Harmony.CreateAndPatchAll(typeof(PrivateMaidList));
			//foreach (var item in GameUty.loadArchiveList)
			//{
			//	Debug.Log(item);
			//}
			if (GameUty.loadArchiveList != null)
            {
                NewMethod();
            }
        }

		public static void NewMethod()
        {
            if (GameUty.loadArchiveList.Contains("script_gp002"))
            {
                Debug.Log("PrivateMaidList.script_gp002");
                replaceText.Add("A");
                replaceText.Add("A1");
                replaceText.Add("B");
                replaceText.Add("B1");
                replaceText.Add("C");
                replaceText.Add("C1");
                replaceText.Add("D");
                replaceText.Add("D1");
                replaceText.Add("E");
                replaceText.Add("E1");
                replaceText.Add("F");
                replaceText.Add("F1");
                replaceText.Add("G");
                replaceText.Add("G1");
                replaceText.Add("H1");
                replaceText.Add("J1");
            }
            if (GameUty.loadArchiveList.Contains("script_gp002wedd"))
            {
                Debug.Log("PrivateMaidList.script_gp002wedd");
                replaceText.Add("K1");
                replaceText.Add("L1");
                replaceText.Add("M1");
                replaceText.Add("N1");
            }
        }

        public static void Unload()
		{
			if (instance != null)
				instance.UnpatchAll(instance.Id);
			instance = null;
		}

		[HarmonyPatch(typeof(uGUICharacterSelectManager), "PrivateMaidList")]
		[HarmonyPrefix]
		public static bool PrivateMaidListPre(List<Maid> drawList)
		{
			Debug.Log($"PrivateMaidList {replaceText.Count}");
			CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
			for (int i = 0; i < characterMgr.GetStockMaidCount(); i++)
			{
				Maid stockMaid = characterMgr.GetStockMaid(i);
				Status status = stockMaid.status;
				
				if (stockMaid.status.heroineType == HeroineType.Sub)
					continue;

				if (stockMaid.boNPC || stockMaid.boMAN)
					continue;

				if (replaceText.Contains(status.personal.replaceText) && !stockMaid.IsCrcBody)
				//if (status.specialRelation == SpecialRelation.Married && !stockMaid.IsCrcBody)
				{
					drawList.Add(stockMaid);
				}
			}
			return false;
		}

		[HarmonyPatch(typeof(GameUty), "Init")]
		[HarmonyPostfix]
		public static void InitPost()
		{
			NewMethod();
		}
	}