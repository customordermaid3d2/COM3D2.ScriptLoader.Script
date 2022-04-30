// #author PainBrioché
// #name UnlockAllSkills
// #desc Unlock or Reset all valid Yotogi Skills for a maid.
// Press U while in Maid Admin with the desired Maid selected to Unlock all valid Yotogi Skills
// Press R while in Maid Admin with the desired Maid selected to Reset Yotogi Skills to their natural state.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HarmonyLib;
using MaidStatus;
using MaidStatus.CsvData;
using Yotogis;

public class UnlockAllSkills3
{
    #region config
    // Key to use to toggle between lock/unlock
    // Refer to Unity docs for available keys:
    // https://docs.unity3d.com/ScriptReference/KeyCode.html
    static readonly KeyCode UNLOCK_KEYCODE = KeyCode.U;
    static readonly KeyCode RESET_KEYCODE = KeyCode.R;
    #endregion config



    public static bool isMaidManagement = false;
    public static bool isUnlockGlobal;
    public static GameObject gameObject;

    public static void Main()
    {
        gameObject = new GameObject();
        gameObject.AddComponent<MB>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void Unload()
    {
        GameObject.Destroy(gameObject);
        gameObject = null;
    }


    public static void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        isMaidManagement = scene.buildIndex == 7;
    }

    class MB : MonoBehaviour
    {
        Harmony instance;

        void Awake()
        {
            DontDestroyOnLoad(this);
            instance = Harmony.CreateAndPatchAll(typeof(MB));
        }

        void OnDestroy()
        {
            instance.UnpatchAll(instance.Id);
        }

        void Update()
        {
            if (isMaidManagement)
            {
                if (Input.GetKeyDown(UNLOCK_KEYCODE))
                {
                    ChoiceConfirmationWarning(true);
                }
                if (Input.GetKeyDown(RESET_KEYCODE))
                {
                    ChoiceConfirmationWarning(false);
                }
            }
        }

        void ChoiceConfirmationWarning(bool isUnlock)
        {
            MaidManagementMain mMM = GameObject.FindObjectOfType<MaidManagementMain>();
            Maid selectedMaid = mMM.select_maid_;
            string warningMessage;
            string resultMessage;

            warningMessage = isUnlock ? $"You're about to unlock all {selectedMaid.status.callName}'s Yotogi Skills \n Are you sure ?" : $"You're about to reset {selectedMaid.status.callName}'s Yotogi Skills \n Are you sure ?";
            resultMessage = isUnlock ? $"{selectedMaid.status.callName}'s Yotogi skills have been Unlocked!" : $"{selectedMaid.status.callName}'s Yotogi skills have been reset to their natural state!";

            GameMain.Instance.SysDlg.Show(warningMessage, SystemDialog.TYPE.YES_NO, delegate
            {
                RelearnSkills(isUnlock, selectedMaid);

                GameMain.Instance.SysDlg.Close();
                GameMain.Instance.SysDlg.Show(resultMessage, SystemDialog.TYPE.OK, new SystemDialog.OnClick(GameMain.Instance.SysDlg.Close));

            }, new SystemDialog.OnClick(GameMain.Instance.SysDlg.Close));
        }

        // Wipe all skills and repopulate
        void RelearnSkills(bool isUnlock, Maid maid)
        {
            maid.status.yotogiSkill.Clear();
            Debug.Log($"■■■■■ Skill list cleared, begining skill attribution. ■■■■■");

            isUnlockGlobal = isUnlock;
            List<Skill.Data> learnPossibleSkills = Skill.GetLearnPossibleSkills(maid.status);            
            
            // Increase her yotogiCount by 1, so Yotogi is unlocked in memory mode.
            if (isUnlock && maid.status.playCountYotogi == 0) { maid.status.playCountYotogi += 1; }


            isUnlockGlobal = false;

            foreach (Skill.Data skill in learnPossibleSkills)
            {
                Debug.Log($"■ {skill.name} skill has been added to her skill list.");
                maid.status.yotogiSkill.Add(skill.id);
            }

            string logEndMessage = isUnlock ? $"■■■■■ {maid.status.callName}'s skills have been Unlocked! ■■■■■" : $"■■■■■ {maid.status.callName}'s skills have been reset to their natural state! ■■■■■";
            Debug.Log(logEndMessage);
        }


        #region UnlockSkills Section
        // Checks for all valid Yotogi Classes rather than only already acquires ones.
        public static List<YotogiClass.Data> GetPossibleClassDatas(Status status, AbstractClassData.ClassType classTypeFlags)
        {
            List<YotogiClass.Data> datas = new List<YotogiClass.Data>();
            List<YotogiClass.Data> allBaseClassData = YotogiClass.GetAllDatas(true);

            foreach (YotogiClass.Data data in allBaseClassData)
            {
                if ((classTypeFlags & data.classType) != 0 && data.learnConditions.isFutureLearnPossible(status))
                {
                    datas.Add(data);
                }
            }

            return datas;
        }

        // You cannot ignore Yotogi Class check as it would lead to invalid skills, everything else is fair game.
        [HarmonyPatch(typeof(Skill.Data), "IsCheckGetSkill")]
        [HarmonyPrefix]
        public static bool DisableChecks(Skill.Data __instance, Status status, bool is_seikeiken_check, ref bool __result)
        {
            if (isUnlockGlobal)
            {
                __result = true;

                if (__instance.getcondition_data.yotogi_class != null)
                {
                    List<YotogiClass.Data> possibleYotogiClassDatas = GetPossibleClassDatas(status, AbstractClassData.ClassType.Share | AbstractClassData.ClassType.New);
                    if (!possibleYotogiClassDatas.Contains(__instance.getcondition_data.yotogi_class))
                    {
                        Debug.Log($"◆ {__instance.name} was ignored because this personality doesn't have acces to {__instance.getcondition_data.yotogi_class.uniqueName}");
                        __result = false;
                    }
                }

                return false;
            }
            return true;
        }
        #endregion
    }
}