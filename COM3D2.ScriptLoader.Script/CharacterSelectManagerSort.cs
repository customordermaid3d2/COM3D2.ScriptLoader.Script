using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerStatus;
using MaidStatus;

    public static class CharacterSelectManagerSort
    {
        static Harmony instance;

        public static void Main()
        {
        if (instance == null)
            instance = Harmony.CreateAndPatchAll(typeof(CharacterSelectManagerSort));
        }

        public static void Unload()
        {            
            instance?.UnpatchAll(instance.Id);
            instance = null;
        }

        [HarmonyPatch(typeof(CharacterSelectManager), "SortMaidRanking", typeof(Maid),typeof(Maid))]
        [HarmonyPatch(typeof(CharacterSelectManager), "SortMaidStandardNoSchedule", typeof(Maid),typeof(Maid))]
        [HarmonyPatch(typeof(CharacterSelectManager), "SortMaidStandard", typeof(Maid),typeof(Maid))]
        [HarmonyPrefix]
        public static bool SortMaidStandard1(ref int __result, Maid maid_a, Maid maid_b)
        {
			
			// personal id - contract - specialRelation - relation_ - additionalRelation_ - sub(extra) - sub(unique) - sub(id)
            if (maid_a == maid_b) __result = 0; // not delet
            else if (maid_a.status.heroineType == MaidStatus.HeroineType.Sub && maid_b.status.heroineType == MaidStatus.HeroineType.Sub)
            {
                     if (maid_a.status.subCharaStatus.contractText == "エキストラ") __result = -1; // ユニーク
                else if (maid_b.status.subCharaStatus.contractText == "エキストラ") __result = 1; // ユニーク
                else if (maid_a.status.subCharaData.id < maid_b.status.subCharaData.id) __result = -1;
                else if (maid_a.status.subCharaData.id > maid_b.status.subCharaData.id) __result = 1;
                else __result = 0;
            }
            else if (maid_a.status.heroineType == MaidStatus.HeroineType.Sub) __result = 1;
            else if (maid_b.status.heroineType == MaidStatus.HeroineType.Sub) __result = -1;
            else if (maid_a.status.personal.id < maid_b.status.personal.id) __result = -1;
            else if (maid_a.status.personal.id > maid_b.status.personal.id) __result = 1;
            else if (maid_a.status.contract < maid_b.status.contract) __result = -1;
            else if (maid_a.status.contract > maid_b.status.contract) __result = 1;
            else if (maid_a.status.specialRelation < maid_b.status.specialRelation) __result = 1;
            else if (maid_a.status.specialRelation > maid_b.status.specialRelation) __result = -1;
            else if (maid_a.status.relation_ < maid_b.status.relation_) __result = 1;
            else if (maid_a.status.relation_ > maid_b.status.relation_) __result = -1;
            else if (maid_a.status.additionalRelation_ < maid_b.status.additionalRelation_) __result = 1;
            else if (maid_a.status.additionalRelation_ > maid_b.status.additionalRelation_) __result = -1;
            else __result = 0;
			
            // Debug.LogWarning($"SortMaidStandardA {maid_a.status.fullNameEnStyle}, {maid_a.status.subCharaStatus?.id},  {maid_a.status.subCharaStatus?.relationText}, {maid_a.status.subCharaStatus?.uniqueName}, {maid_a.status.subCharaStatus?.contractText}, {maid_a.status.subCharaStatus?.personalText}, {maid_a.status.subCharaStatus?.relationText},");
            // Debug.LogWarning($"SortMaidStandardB {maid_b.status.fullNameEnStyle}, {maid_b.status.subCharaStatus?.id},  {maid_b.status.subCharaStatus?.relationText}, {maid_b.status.subCharaStatus?.uniqueName}, {maid_b.status.subCharaStatus?.contractText}, {maid_b.status.subCharaStatus?.personalText}, {maid_b.status.subCharaStatus?.relationText},");
            // Debug.LogWarning($"{__result}");

            return false;

        }

    }