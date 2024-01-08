﻿using StudioCore.Aliases;
using StudioCore.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudioCore.JSON.Assetdex
{
    /// <summary>
    /// Class <c>AssetdexCore</c> contains the <c>AssetReference</c> dictionaries that host the documentation for each asset.
    /// </summary>
    public class AssetdexMain
    {
        private Dictionary<GameType, ModelAliasContainer> assetContainers = new Dictionary<GameType, ModelAliasContainer>();

        public AssetdexMain()
        {
            assetContainers.Add(GameType.Undefined, BuildAssetContainer("DS1")); // Fallback for Undefined project
            assetContainers.Add(GameType.DemonsSouls, BuildAssetContainer("DES"));
            assetContainers.Add(GameType.DarkSoulsPTDE, BuildAssetContainer("DS1"));
            assetContainers.Add(GameType.DarkSoulsRemastered, BuildAssetContainer("DS1R"));
            assetContainers.Add(GameType.DarkSoulsIISOTFS, BuildAssetContainer("DS2S"));
            assetContainers.Add(GameType.Bloodborne, BuildAssetContainer("BB"));
            assetContainers.Add(GameType.DarkSoulsIII, BuildAssetContainer("DS3"));
            assetContainers.Add(GameType.Sekiro, BuildAssetContainer("SDT"));
            assetContainers.Add(GameType.EldenRing, BuildAssetContainer("ER"));
            assetContainers.Add(GameType.ArmoredCoreVI, BuildAssetContainer("AC6"));
        }

        private ModelAliasContainer BuildAssetContainer(string gametype)
        {
            var container = new ModelAliasContainer(gametype);

            return container;
        }

        public Dictionary<string, ModelAliasReference> GetChrEntriesForGametype(GameType gametype)
        {
            var dict = new Dictionary<string, ModelAliasReference>();

            foreach (ModelAliasReference entry in assetContainers[gametype].GetChrEntries())
                if (!dict.ContainsKey(entry.id.ToLower()))
                    dict.Add(entry.id.ToLower(), entry);

            return dict;
        }

        public Dictionary<string, ModelAliasReference> GetObjEntriesForGametype(GameType gametype)
        {
            var dict = new Dictionary<string, ModelAliasReference>();

            foreach (ModelAliasReference entry in assetContainers[gametype].GetObjEntries())
                if (!dict.ContainsKey(entry.id.ToLower()))
                    dict.Add(entry.id.ToLower(), entry);

            return dict;
        }

        public Dictionary<string, ModelAliasReference> GetPartEntriesForGametype(GameType gametype)
        {
            var dict = new Dictionary<string, ModelAliasReference>();

            foreach (ModelAliasReference entry in assetContainers[gametype].GetPartEntries())
                if (!dict.ContainsKey(entry.id.ToLower()))
                    dict.Add(entry.id.ToLower(), entry);

            return dict;
        }

        public Dictionary<string, ModelAliasReference> GetMapPieceEntriesForGametype(GameType gametype)
        {
            var dict = new Dictionary<string, ModelAliasReference>();

            foreach (ModelAliasReference entry in assetContainers[gametype].GetMapPieceEntries())
                if (!dict.ContainsKey(entry.id.ToLower()))
                    dict.Add(entry.id.ToLower(), entry);

            return dict;
        }
    }
}
