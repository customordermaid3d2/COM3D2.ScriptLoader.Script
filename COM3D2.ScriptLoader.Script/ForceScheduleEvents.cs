// #author PainBrioché
// #name Force Schedule Events
// #desc Removes checks for Schedules Yotogi Events.
using UnityEngine;
using UnityEngine.SceneManagement;
using HarmonyLib;
using Schedule;
using System.Collections.Generic;
using MaidStatus;

public class ForceScheduleEvents
{
    #region config
    // Plugin starts with H-Event enabled true/false (default: false)
    static readonly bool startsUnlocked = false;

    // Key to use to toggle between lock/unlock
    // Refer to Unity docs for available keys:
    // https://docs.unity3d.com/ScriptReference/KeyCode.html
    static readonly KeyCode TOGGLE_KEYCODE = KeyCode.E;
    #endregion config

    internal static bool canToggle = false;
    public static GameObject gameObject;

    public static void Main()
    {
        gameObject = new GameObject();
        gameObject.AddComponent<MB>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public static void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        canToggle = scene.buildIndex == 3 || scene.buildIndex == 1;
    }

    public static void Unload()
    {
        GameObject.Destroy(gameObject);
        gameObject = null;
    }

    class MB : MonoBehaviour
    {
        Harmony instance;
        static bool isUnlock = startsUnlocked;

        void Awake()
        {
            DontDestroyOnLoad(this);
            instance = Harmony.CreateAndPatchAll(typeof(MB));
        }

        void Update()
        {
            if (Input.GetKeyDown(TOGGLE_KEYCODE) && canToggle)
            {
                Toggle();
            }
        }

        void OnDestroy()
        {
            instance.UnpatchAll(instance.Id);
        }

        void Toggle()
        {
            isUnlock = !isUnlock;
            string message = isUnlock ? "Schedule Events are Unlocked" : "Schedule Events are Locked";
            Debug.LogWarning(message);
        }

        // Simply remove most of the checks needed to enable an event.
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Schedule.ScheduleAPI), nameof(ScheduleAPI.EnableNightWork))]
        public static bool EnableNightWorkPrefix(ref bool __result, int workId, Maid maid)
        {
            if (isUnlock)
            {
                __result = true;

                ScheduleCSVData.Yotogi yotogi = ScheduleCSVData.YotogiData[workId];

                if (!DailyMgr.IsLegacy && yotogi.mode == ScheduleCSVData.ScheduleBase.Mode.CM3D2)
                {
                    __result = false;
                }
                if (DailyMgr.IsLegacy && yotogi.mode == ScheduleCSVData.ScheduleBase.Mode.COM3D)
                {
                    __result = false;
                }

                if (maid != null)
                {
                    if (yotogi.isCheckBodyType && maid.IsCrcBody)
                    {
                        __result = false;
                    }
                    if ((yotogi.isNewBodyBlock && maid.IsCrcBody) || !yotogi.CheckMainHeroineBodyTypeMatch(maid))
                    {
                        __result = false;
                    }
                }

                return false;
            }
            return true;
        }

        // Overrides the tandem selection for Harem Events.
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ScheduleAPI), nameof(ScheduleAPI.GetNewYotogiHaremPairCandidateList))]
        public static bool GetNewYotogiHaremPairCandidateListPrefix(ref List<Maid> __result, ScheduleCSVData.Yotogi workData, List<int> condPersonalIds, Maid excludeMaid = null)
        {
            if (isUnlock)
            {
                List<Maid> list = new List<Maid>();

                for (int i = 0; i < GameMain.Instance.CharacterMgr.GetStockMaidCount(); i++)
                {
                    Maid stockMaid = GameMain.Instance.CharacterMgr.GetStockMaid(i);
                    if (stockMaid != null && stockMaid.status.heroineType != HeroineType.Sub)
                    {
                        if (!(excludeMaid != null) || !(excludeMaid.status.guid == stockMaid.status.guid))
                        {
                            if (condPersonalIds.Contains(stockMaid.status.personal.id))
                            {
                                if (excludeMaid.IsCrcBody == stockMaid.IsCrcBody)
                                {
                                    if (workData.id == 10072 || workData.id == 21160)
                                    {
                                        if (!workData.isCheckGP002Personal || PluginData.IsEnabledForGP002Personal(stockMaid.status.personal.uniqueName))
                                        {
                                            list.Add(stockMaid);
                                        }
                                    }
                                    else if (!workData.isCheckGP002Personal || PluginData.IsEnabledForGP002Personal(stockMaid.status.personal.uniqueName))
                                    {
                                        list.Add(stockMaid);
                                    }
                                }
                            }
                        }
                    }
                }
                __result = list;

                return false;
            }
            return true;
        }

        // Enable H-Events in memory Mode
        [HarmonyPostfix]
        [HarmonyPatch(typeof(FreeModeItemVip), nameof(FreeModeItemVip.CreateItemVipList))]
        public static void CreateItemVipListPostfix(ref List<FreeModeItemVip> __result)
        {
            if (isUnlock)
            {
                foreach (FreeModeItemVip item in __result)
                {
                    item.is_enabled_ = true;
                }
            }
        }
    }
}