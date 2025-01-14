﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nocturnal;
using Nocturnal.Apis.qm;
using Nocturnal.Settings.wrappers;
using UnityEngine;
using VRC.SDKBase;

namespace Nocturnal.Ui.qm
{
    internal class worldhistory
    {
        internal static GameObject worldhistorymenu = null;
        internal static void createrhistory()
        {
            worldhistorymenu = submenu.Submenu("World History", Main.mainpage);
            Main.mainpage.getmenu().submenu("World History", worldhistorymenu, Settings.Download_Files.worldhistory, true, 2, 2);
        }

        internal static void updatehistory(string worldname, string wolrdid)
        {

            var filewh = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Settings.jsonmanager.worldhistory>>(File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\WorldHistory.json"));
            if (filewh.Any())
                filewh.RemoveAt(filewh.Count - 1);

            var newworldh = new Settings.jsonmanager.worldhistory()
            {
                worldid = wolrdid,

                worldname = worldname,
            };

            filewh.Insert(0, newworldh);

            var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(filewh);
            File.WriteAllText((Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\WorldHistory.json"), serialized);

            var gmj = worldhistorymenu.getmenu().GetComponentsInChildren<UnityEngine.UI.Button>(true).Where(gmj => gmj.gameObject != worldhistorymenu.getmenu().gameObject).ToArray();
            for (int i = 0; i < gmj.Length; i++)
            {
                try
                {
                    GameObject.DestroyImmediate(gmj[i].gameObject);
                }
                catch { }
            }
            var getworldsfromfile = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Settings.jsonmanager.worldhistory>>(File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\WorldHistory.json")).ToArray();
           foreach (var world in getworldsfromfile)
            {

            
                Apis.qm.Buttons.Button(worldhistorymenu.getmenu(), world.worldname, () =>
                 {
                    
                     try
                     {
                         if (!Networking.GoToRoom(world.worldid))
                         {
                             string[] array = world.worldid.Split(':');
                             new PortalInternal().Method_Private_Void_String_String_PDM_0(array[0], array[1]);
                         }
                     }
                     catch (Exception e) {  NocturnalC.log(e); }
                  
                 },true);


              
            }
         }
        }
    }
