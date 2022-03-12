// #author GeoffreyTheHorsington & PainBrioché
// #name AllMaidInPrivateMode
// #desc Allows All compatible maids in private mode without needing them to get married
using System.Linq;
using HarmonyLib;
using System.Collections.Generic;

public static class AllMaidInPrivateMode
{
    /* 
    Add or Remove ID from the list depending on the DLCs you have.
    10 Tsundere, 20 Kuudere, 30 Pure, 40 Yandere, 50 Onee-chan, 60 Genki, 70 Sadictic Queen, 80 Muku
    90 Majime, 100 Rindere, 110 Bookworm, 120 Koakuma, 130 Ladylike, 140 Secretary, 150 Imouto               
    160 Wary, 170 Ojousama, 180 Osananajimi, 190 Masochist, 200 Haraguro, 210 Kisakude, 220 Kimajime
    */
    static int[] validPersonalitiesID = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160 };

    public static void Main()
    {
        Harmony.CreateAndPatchAll(typeof(AllMaidInPrivateMode));
    }

    [HarmonyPatch(typeof(uGUICharacterSelectManager), "PrivateMaidList")]
    [HarmonyPrefix]
    public static bool NewPrivateMaidList(List<Maid> drawList)
    {
        drawList.Clear();
        CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
        foreach (Maid maid in characterMgr.GetStockMaidList())
        {
            if (!maid.IsCrcBody && validPersonalitiesID.Contains(maid.status.personal.id) && (maid.status.heroineType == MaidStatus.HeroineType.Original || maid.status.heroineType == MaidStatus.HeroineType.Transfer))
            {
                drawList.Add(maid);
            }
        }
        return false;
    }
}



