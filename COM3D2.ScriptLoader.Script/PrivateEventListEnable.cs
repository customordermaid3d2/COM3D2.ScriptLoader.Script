using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using PrivateMaidMode;

	public static class PrivateEventListEnable
    {
		static Harmony instance;

		public static void Main()
		{
			if (instance != null)
				instance.UnpatchAll(instance.Id);
			instance = Harmony.CreateAndPatchAll(typeof(PrivateEventListEnable));

		}

		public static void Unload()
		{
			if (instance != null)
				instance.UnpatchAll(instance.Id);
			instance = null;
		}

		/*
		*/
		[HarmonyPatch(typeof(PrivateEventSelectPanel), "InstantiateUnitOBject")]
		[HarmonyPrefix]
		public static void InstantiateUnitOBject(ref bool isExec)
		{
			isExec = true;
		}
		

		private static Maid smaid
		{
			get
			{
				return (!(PrivateModeMgr.Instance.PrivateMaid != null)) ? GameMain.Instance.CharacterMgr.GetMaid(0) : PrivateModeMgr.Instance.PrivateMaid;
			}
		}

		//public void Setup(Maid overRideMaid = null, DataBase.BG overRideBg = null)
		[HarmonyPatch(typeof(PrivateEventSelectPanel), "Setup")]
		[HarmonyPrefix]
		public static bool Setup(PrivateEventSelectPanel __instance,
			Maid overRideMaid , DataBase.BG overRideBg , UIGrid ___unitGrid, List<GameObject> ___unitList, Transform ___conditionParent)
		{
			foreach (GameObject obj in ___unitList)
			{
				UnityEngine.Object.Destroy(obj);
			}
			___unitList.Clear();
			___conditionParent.gameObject.SetActive(false);
			DataBase.BG bg = (overRideBg != null) ? overRideBg : PrivateModeMgr.Instance.SelectBG;
			Maid maid = (!(overRideMaid == null)) ? overRideMaid : smaid;
			if (bg == null || maid == null)
			{
				return false;
			}
			DataBase.BG.Event @event = bg.GetEvent(GameMain.Instance.CharacterMgr.status.isDaytime);
			foreach (DataBase.BG.Event.PointData pointData in @event.eventPointList)
			{
				bool flag = pointData.IsExec(maid);// && @event.isNoon == GameMain.Instance.CharacterMgr.status.isDaytime;
				//int nextScenarioIndex = @event.GetNextScenarioIndex(maid, pointData);
				int no = pointData.no;
				for (int j = 0; j < pointData.information.Count; j++)
				{
					bool isAlreadyReaded = @event.IsFinishedReadingFile(maid, pointData, j);
					DataBase.BG.Event.PointData.Information information = pointData.information[j];
					if (information.iconType != DataBase.BG.Event.PointData.Information.IconType.NTR || !GameMain.Instance.CharacterMgr.status.lockNTRPlay)
					{
						bool isExec = flag;//&& nextScenarioIndex == j;
						PrivateEventListUnit privateEventListUnit = __instance.InstantiateUnitOBject(information, no, isExec, isAlreadyReaded);
						___unitGrid.AddChild(privateEventListUnit.transform);
						___unitGrid.repositionNow = true;
						___unitList.Add(privateEventListUnit.gameObject);
					}
				}
			}
			if (___unitList.Count <= 0)
			{
				PrivateEventListUnit privateEventListUnit2 = __instance.InstantiateEmptyUnitOBject();
				___unitGrid.AddChild(privateEventListUnit2.transform);
				___unitGrid.repositionNow = true;
				___unitList.Add(privateEventListUnit2.gameObject);
			}
			return false;
		}

		
	}