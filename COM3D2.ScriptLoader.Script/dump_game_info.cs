// #author ghorsington
// #name Dump Game Info
// #desc Writes some COM game info into output_log.txt

using UnityEngine;
using System.Reflection;
using System.IO;
using BepInEx;
using System;

public static class DumpGameInfo {
    static TextWriter textWriter;
    public static void Main() {
        textWriter = (TextWriter) Activator.CreateInstance(typeof(Application).Assembly.GetType("UnityEngine.UnityLogWriter"));

        Log($"{new string('#', 10)}[START]Game info{new string('#', 10)}");

        Log("# Generic info");
        Log($"* Process name: {Paths.ProcessName}");
        Log($"* Game Path: {Paths.GameRootPath}");

        Log("\n");
        Log("# Game versions");
        Log($"* From update.lst: {GameUty.GetGameVersionText()}");
        Log($"* From DLL: {GameUty.GetBuildVersionText()}");
        // FIXME: CM info cannot be printed this early
        // Log($"* CM version (0.0 if not linked): {GameUty.GetLegacyGameVersionText()}");
        Log("\n");
        Log($"# BepInEx patchers:");
        LogDlls(Paths.PatcherPluginPath);

        Log("\n");
        Log($"# BepInEx plugins:");
        LogDlls(Paths.PluginPath);

        Log("\n");
        Log($"# Sybaris patchers:");
        LogDlls(Path.Combine(Paths.GameRootPath, "Sybaris"), SearchOption.TopDirectoryOnly);

        Log("\n");
        Log($"# UnityInjector plugins:");
        LogDlls(Path.Combine(Path.Combine(Paths.GameRootPath, "Sybaris"), "UnityInjector"), SearchOption.TopDirectoryOnly);

        Log($"{new string('#', 10)}[END]Game info{new string('#', 10)}");
    }

    private static void LogDlls(string path, SearchOption searchOption = SearchOption.AllDirectories) {
        if(!Directory.Exists(path)) {
            Log($"WARN: Path {path} does not exist!");
            return;
        }

        foreach (var item in Directory.GetFiles(path, "*.dll", searchOption))
            Log(item);
    }

    private static void Log(string s) {
        textWriter.WriteLine(s);
    }
}