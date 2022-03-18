//using COM3D2.DanceCameraMotion.Plugin;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace COM3D2.ResetMuneYurePatch.Script
{
    internal static class ResetMuneYurePatch
    {
        static Harmony instance;

        public static void Main()
        {
            SceneManager.sceneLoaded += SaveSettingsInGame.OnSceneLoaded;
        }

        public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "SceneWarning")
            {
                try
                {
                    if (instance == null)
                        instance = Harmony.CreateAndPatchAll(typeof(ResetMuneYurePatch));
                }
                catch (Exception e)
                {
                    Log(e.ToString());
                }
                SceneManager.sceneLoaded -= SaveSettingsInGame.OnSceneLoaded;
            }
        }

        public static void Unload()
        {
            instance?.UnpatchAll(instance.Id);
            instance = null;
        }

        private static void Log(string s)
        {
            Debug.Log("ResetMuneYurePatch : " + s);
        }

        //[HarmonyPatch("COM3D2.DanceCameraMotion.Plugin.MaidSubManager",(string) "ResetMuneYure")]
        //[HarmonyPatch("COM3D2.DanceCameraMotion.Plugin.MaidSubManager", "ResetMuneYure", MethodType.Normal)]
        //[HarmonyPatch(typeof(COM3D2.DanceCameraMotion.Plugin.MaidSubManager), "ResetMuneYure")]
        [HarmonyPrefix]
        public static bool ResetMuneYure(Maid maid)
        {
            try
            {
                if (maid.body0.jbMuneL.BlendValueON != 1f)
                {
                    maid.body0.jbMuneL.BlendValueON = 1f;
                }
                if (maid.body0.jbMuneR.BlendValueON != 1f)
                {
                    maid.body0.jbMuneR.BlendValueON = 1f;
                }
            }
            catch (Exception e)
            {
                Log(e.ToString());
            }
            return false;
        }
    }
}
