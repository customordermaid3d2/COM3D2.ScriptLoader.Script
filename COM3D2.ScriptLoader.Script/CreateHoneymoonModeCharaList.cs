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
    public static class CreateHoneymoonModeCharaList
    {
        static Harmony instance;

        public static void Main()
        {
            instance = Harmony.CreateAndPatchAll(typeof(CreateHoneymoonModeCharaList));
        }

        public static void Unload()
        {
            instance?.UnpatchAll(instance.Id);
            instance = null;
        }

        [HarmonyPatch(typeof(SceneCharacterSelect), "CreateHoneymoonModeCharaList")]
        [HarmonyPrefix]
        // public static List<string> CreateHoneymoonModeCharaList()
        public static bool CreateHoneymoonModeCharaListPre(ref List<string> __result)
        {
            Debug.LogWarning($"CreateHoneymoonModeCharaList : {SceneCharacterSelect.chara_guid_stock_list.Count}");
            SceneCharacterSelect.chara_guid_stock_list.Clear();
            List<Maid> list = new List<Maid>();
            CharacterSelectManager.DefaultMaidList(list);
            foreach (Maid maid in list)
            {
                if (!maid.boNPC
                    //&& (maid.status.seikeiken == Seikeiken.Yes_No || maid.status.seikeiken == Seikeiken.Yes_Yes) 
                    //&& maid.status.relation >= Relation.Lover 
                    //&& maid.status.contract != Contract.Trainee 
                    && HoneymoonDatabase.enabledPersonalList.Contains(maid.status.personal.id))
                {
                    SceneCharacterSelect.chara_guid_stock_list.Add(maid.status.guid);
                }
            }
            __result = new List<string>(SceneCharacterSelect.chara_guid_stock_list);
            Debug.LogWarning($"CreateHoneymoonModeCharaList : {__result.Count}");
            return false;
        }

    }
}
