// #author ghorsington
// #name Add subs to old yotogi
// #desc Add simple subtitles to Neigh Service when running in CM mode

using HarmonyLib;
using UnityEngine;

public static class AddSubsToOldYotogi {
    static Harmony instance;
    static float nextLength = -1;
    static SubtitleMovieManager subMgr;

    public static void Main() {
        instance = Harmony.CreateAndPatchAll(typeof(AddSubsToOldYotogi));
    }

    public static void Unload() {
		if(instance != null)
			instance.UnpatchAll(instance.Id);
        instance = null;
        if (subMgr != null)
            SubtitleMovieManager.DestroyGlobalInstance();
        subMgr = null;
    }

    [HarmonyPatch(typeof(YotogiKagManager), "TagTalk")]
    [HarmonyPostfix]
    public static void PostTagTalk(KagTagSupport tag_data) {
        if (!tag_data.IsValid("voice")) return;
        var voiceTargetMaid = BaseKagManager.GetVoiceTargetMaid(tag_data);
        if(!voiceTargetMaid.AudioMan.isPlay()) return;
        nextLength = voiceTargetMaid.AudioMan.GetLength();
    }

    [HarmonyPatch(typeof(YotogiKagManager), "TagHitRet")]
    [HarmonyPrefix]
    public static bool PreTagHitRet(YotogiKagManager __instance, KagTagSupport tag_data, ref YotogiOldManager ___yotogi_old_mgr_) {
        if (subMgr == null || ___yotogi_old_mgr_ == null) return true;
        var text = __instance.kag.GetText();
        if (string.IsNullOrEmpty(text)) return true;
        if (nextLength < 0) nextLength = 10;
        subMgr.Play(text, (int)(nextLength * 1000));
        nextLength = -1;
        return true;
    }

    [HarmonyPatch(typeof(YotogiOldManager), "Awake")]
    [HarmonyPostfix]
    public static void OnOldYotogiInit() {
        subMgr = SubtitleMovieManager.GetGlobalInstance(false);
        subMgr.transform.localPosition = new Vector3(-459f, -150f, 0);
    }

    [HarmonyPatch(typeof(YotogiOldManager), "OnDestroy")]
    [HarmonyPostfix]
    public static void PostOldYotogiInit() {
        SubtitleMovieManager.DestroyGlobalInstance();
        subMgr = null;
    }
}