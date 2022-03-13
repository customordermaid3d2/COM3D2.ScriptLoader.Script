using System;
using HarmonyLib;
using UnityEngine;


    public static class CharacterSelectManagerSort
    {
        static Harmony instance;
		//static List<Maid> maids;
		
        public static void Main()
        {
            instance = Harmony.CreateAndPatchAll(typeof(CharacterSelectManagerSort));
			//maids = GameMain.Instance.CharacterMgr.GetStockMaidList();
        }

        public static void Unload()
        {
            if (instance != null)
			{
                instance.UnpatchAll(instance.Id);
			}
            instance = null;
        }

        [HarmonyPatch(typeof(CharacterSelectManager), "SortMaidStandard", typeof(Maid),typeof(Maid))]
        [HarmonyPrefix]
        public static bool SortMaidStandard1(ref int __result, Maid maid_a, Maid maid_b)
        {
			//Sort order is head maid, wives, originals, transfers, and subs. When maid types are equal, sort order is contract type, maid last name, maid first name, going a level deeper until a difference is found.
			__result = maid_a.status.leader.CompareTo(maid_b.status.leader) * -1;
			
			//Since marriage information can be held in two places due to CM transfers, we use an elaborate conditional to check if a maid is married or not.
			if (__result == 0)
			{
				bool MarriageStatusA = (maid_a.status.OldStatus != null && (maid_a.status.OldStatus.isMarriage || maid_a.status.OldStatus.isNewWife)) || (maid_a.status.specialRelation == MaidStatus.SpecialRelation.Married);
				
				bool MarriageStatusB = (maid_b.status.OldStatus != null && (maid_b.status.OldStatus.isMarriage || maid_b.status.OldStatus.isNewWife)) || (maid_b.status.specialRelation == MaidStatus.SpecialRelation.Married);
			
				__result = MarriageStatusA.CompareTo(MarriageStatusB) * -1;
			} 
			
			if (__result == 0)
			{				
				//When types are equal, maids are sorted by contract.
				if (maid_a.status.heroineType == maid_b.status.heroineType)
				{
					if (maid_a.status.mainChara != maid_b.status.mainChara)
					{
						if (maid_a.status.mainChara)
						{
							__result = -1;
						}
						else 
						{					
							__result = 1;						
						}
					}
					
					if (__result == 0)
					{
						//Compare contracts. We multiply by one to invert the order since the enums are in the reverse order we want. This way the order is: exclusive, free, trainee.
						__result = maid_a.status.contract.CompareTo(maid_b.status.contract) * -1;
					}
					
					//Maids are of the same contract
					if (__result == 0)
					{
						__result = maid_a.status.lastName_.CompareTo(maid_b.status.lastName_);
					}
					
					//Maids are of the same last name? Plausible.
					if (__result == 0)
					{
						__result = maid_a.status.firstName_.CompareTo(maid_b.status.firstName_);
					}
				}
				//When first maid is original, naturally original is always first
				else if(maid_a.status.heroineType == MaidStatus.HeroineType.Original)
				{
					__result = -1;
				}
				//When maid is sub, she will always go last.
				else if (maid_a.status.heroineType == MaidStatus.HeroineType.Sub)
				{
					__result = 1;
				}
				//When maid is a transfer, she'll take a middle spot after originals and before subs so more logic is needed.
				else
				{
					//If original than we lose, she goes first.
					if(maid_b.status.heroineType == MaidStatus.HeroineType.Original)
					{
						__result = 1;
					}
					else
					{
						//If not original then safely assume sub, we win.
						__result = -1;
					}
				}
			}
			
			return false;
        }

    }