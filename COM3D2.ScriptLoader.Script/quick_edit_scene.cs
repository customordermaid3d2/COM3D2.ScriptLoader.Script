// #author ghorsington
// #name Quick load into Maid Edit
// #desc Quickly loads into Maid Editor from any menu. Used mainly for mod dev purposes.

using UnityEngine;
using HarmonyLib;

public static class QuickAddStockMaid {
    // Key to use to load into the Maid Editor
    // Refer to Unity docs for available keys:
    // https://docs.unity3d.com/ScriptReference/KeyCode.html
    static KeyCode TOGGLE_KEYCODE = KeyCode.I;

    static GameObject gameObject;
    public static void Main() {
        gameObject = new GameObject();
        gameObject.AddComponent<MB>();
    }

    public static void Unload() {
        GameObject.Destroy(gameObject);
        gameObject = null;
    }

    class MB : MonoBehaviour {
        static readonly string sceneMenuLoadScript =        "@AllProcPropSeqStart maid=0 sync\n"+
                                                            "@eval exp=\"SetTmpFlag('新規雇用メイド',1)\"\n"+
                                                            "@SceneCall name=SceneEdit qasm original";
        static readonly string editDoneScript = "@jump file=\"DaytimeMain\" label=\"*昼メニュー\"";

        Harmony instance;

        void Awake() {
            DontDestroyOnLoad(this);
            instance = Harmony.CreateAndPatchAll(typeof(MB));
        }

        void OnDestroy() {
            instance.UnpatchAll(instance.Id);
        }

        static bool isInQuickEdit = false;

        [HarmonyPatch(typeof(SceneEdit), "Awake")]
        [HarmonyPrefix]
        static void OnSceneEditAwake() {
            var tagBackup = GameMain.Instance.ScriptMgr.adv_kag.tag_backup;
            if(tagBackup == null || !tagBackup.TryGetValue("name", out string name) || name != "SceneEdit")
                return;
            
            isInQuickEdit = tagBackup.ContainsKey("qasm");
            Debug.Log($"Is in quick edit mode: {isInQuickEdit}");
        }

        [HarmonyPatch(typeof(SceneEdit), "OnEndScene")]
        [HarmonyPostfix]
        static void OnSceneEditEnd() {
            if(!isInQuickEdit)
                return;
            
            isInQuickEdit = false;
            Debug.Log($"Going back to daily menu");
            GameMain.Instance.ScriptMgr.adv_kag.kag.LoadScenarioString(editDoneScript);
            GameMain.Instance.ScriptMgr.adv_kag.kag.Exec();
        }

        void Update() {
            if(GameMain.Instance.GetNowSceneName() == "SceneTitle" && !GameMain.Instance.MainCamera.IsFadeProc() && Input.GetKeyDown(TOGGLE_KEYCODE)) {
                if(GameMain.Instance.GetNowSceneName() == "SceneEdit") {
                    Debug.Log("Already in the editor!");
                    return;
                }
                var maid = GameMain.Instance.CharacterMgr.AddStockMaid();
                Debug.Log($"Added stock meido: {maid}");

                GameMain.Instance.MainCamera.FadeOut(f_dg: () => {
                    GameMain.Instance.CharacterMgr.SetActiveMaid(maid, 0);
                    GameMain.Instance.ScriptMgr.adv_kag.kag.LoadScenarioString(sceneMenuLoadScript);
                    GameMain.Instance.ScriptMgr.adv_kag.kag.Exec();
                });
            }
        }
    }
}