using System.Collections.Generic;
using Modding;
using Modding.Utils;
using UnityEngine;

namespace MoreMapMarkers;

public class MMMGlobalSettings
{
    public int Number_Of_Additional_Markers_Per_Color = 12;
}

public class MoreMapMarkers : Mod, IGlobalSettings<MMMGlobalSettings>
{
    public MoreMapMarkers() : base("More Map Markers")
    {
        On.GameMap.Start += CreateMoreMarkers;
        ModHooks.GetPlayerIntHook += ReturnMoreMarkesForOurCustomOnes;
        ModHooks.SetPlayerIntHook += SaveLessMarkesBecauseOurCustomOnes;
    }

    private int ReturnMoreMarkesForOurCustomOnes(string name, int orig)
    {
        if (name == "spareMarkers_b")
        {
            return GlobalSettings.Number_Of_Additional_Markers_Per_Color + orig;
        }
        if (name == "spareMarkers_r")
        {
            return GlobalSettings.Number_Of_Additional_Markers_Per_Color + orig;
        }
        if (name == "spareMarkers_w")
        {
            return GlobalSettings.Number_Of_Additional_Markers_Per_Color + orig;
        }
        if (name == "spareMarkers_y")
        {
            return GlobalSettings.Number_Of_Additional_Markers_Per_Color + orig;
        }
        return orig;
    }

    private int SaveLessMarkesBecauseOurCustomOnes(string name, int orig)
    {
        if (name == "spareMarkers_b")
        {
            return orig - GlobalSettings.Number_Of_Additional_Markers_Per_Color;
        }
        if (name == "spareMarkers_r")
        {
            return orig - GlobalSettings.Number_Of_Additional_Markers_Per_Color;
        }
        if (name == "spareMarkers_w")
        {
            return orig - GlobalSettings.Number_Of_Additional_Markers_Per_Color;
        }
        if (name == "spareMarkers_y")
        {
            return orig - GlobalSettings.Number_Of_Additional_Markers_Per_Color;
        }
        return orig;
    }

    private void CreateMoreMarkers(On.GameMap.orig_Start orig, GameMap self)
    {
        orig(self);
        GameObject origBlue = self.gameObject.transform.Find("Map Markers/B1").gameObject;
        GameObject origRed = self.gameObject.transform.Find("Map Markers/R1").gameObject;
        GameObject origWhite = self.gameObject.transform.Find("Map Markers/W1").gameObject;
        GameObject origYellow = self.gameObject.transform.Find("Map Markers/Y1").gameObject;
        List<GameObject> listMarkersBlue = new List<GameObject>(self.mapMarkersBlue);
        List<GameObject> listMarkersRed = new List<GameObject>(self.mapMarkersRed);
        List<GameObject> listMarkersWhite = new List<GameObject>(self.mapMarkersWhite);
        List<GameObject> listMarkersYellow = new List<GameObject>(self.mapMarkersYellow);
        for (int i = 0; i < GlobalSettings.Number_Of_Additional_Markers_Per_Color; i++)
        {
            int useNum = i + 7; // W1 - W6, so start at W7
            GameObject copyBlue = GameObject.Instantiate(origBlue, origBlue.transform.parent);
            copyBlue.name = $"B{useNum}";
            listMarkersBlue.Add(copyBlue);
            GameObject copyRed = GameObject.Instantiate(origRed, origRed.transform.parent);
            copyRed.name = $"R{useNum}";
            listMarkersRed.Add(copyRed);
            GameObject copyWhite = GameObject.Instantiate(origWhite, origWhite.transform.parent);
            copyWhite.name = $"W{useNum}";
            listMarkersWhite.Add(copyWhite);
            GameObject copyYellow = GameObject.Instantiate(origYellow, origYellow.transform.parent);
            copyYellow.name = $"Y{useNum}";
            listMarkersYellow.Add(copyYellow);
        }
        self.mapMarkersBlue = listMarkersBlue.ToArray();
        self.mapMarkersRed = listMarkersRed.ToArray();
        self.mapMarkersWhite = listMarkersWhite.ToArray();
        self.mapMarkersYellow = listMarkersYellow.ToArray();
    }

    public static MMMGlobalSettings GlobalSettings { get; protected set; } = new MMMGlobalSettings();
    public void OnLoadGlobal(MMMGlobalSettings s) => GlobalSettings = s;
    public MMMGlobalSettings OnSaveGlobal() => GlobalSettings;
}