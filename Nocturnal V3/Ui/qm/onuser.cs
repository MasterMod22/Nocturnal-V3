﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Nocturnal.Apis.qm;
using Nocturnal.Settings.wrappers;
using Nocturnal.Apis;
using System.IO;
using VRC;

namespace Nocturnal.Ui.qm
{
    internal class onuser
    {
        internal static void Onuser()
        {
            var menu = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions").gameObject;
            Buttons.Button(menu, "Whitelist Anticrash", () => {
                var id = GameObject.Find("/_Application").transform.Find("UIManager/SelectedUserManager").gameObject.GetComponent<VRC.DataModel.UserSelectionManager>().field_Private_APIUser_1.id;

                if (!File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\AntiCrash.whitelist").Contains(id))
                {
                    File.AppendAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\AntiCrash.whitelist", $"\n{id}");
                    Settings.Download_Files.userwhitelist = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\AntiCrash.whitelist");
                }

            });

            Buttons.Button(menu, "Remove Whitelist Anticrash", () => {
                var id = GameObject.Find("/_Application").transform.Find("UIManager/SelectedUserManager").gameObject.GetComponent<VRC.DataModel.UserSelectionManager>().field_Private_APIUser_1.id;

                if (File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\AntiCrash.whitelist").Contains(id))
                {
                    var ac = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\AntiCrash.whitelist");
                    var splited = ac.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    string becomingback = "";
                    for (int i = 0; i < splited.Length; i++)
                    {
                        var trimduser = splited[i].Trim();
                        if (trimduser != id)
                            becomingback += $"{trimduser}\n";
                    }
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\AntiCrash.whitelist", becomingback);

                    Settings.Download_Files.userwhitelist = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\AntiCrash.whitelist");
                }

            });


            Buttons.Button(menu, "Target User", () => Nocturnal.Settings.wrappers.Target.Targetuser(GameObject.Find("/_Application").transform.Find("UIManager/SelectedUserManager").gameObject.GetComponent<VRC.DataModel.UserSelectionManager>().field_Private_APIUser_1.id));
            Buttons.Button(menu, "Teleport", () =>
            {

                var User = GameObject.Find("/_Application").transform.Find("UIManager/SelectedUserManager").gameObject.GetComponent<VRC.DataModel.UserSelectionManager>().field_Private_APIUser_1.id;
                           VRC.Player.prop_Player_0.transform.position = User.getuserbyid().transform.position;
              
            });
            Buttons.Button(menu, "Force Clone", () =>
            {
                var aviid = "";
                var User = GameObject.Find("/_Application").transform.Find("UIManager/SelectedUserManager").gameObject.GetComponent<VRC.DataModel.UserSelectionManager>().field_Private_APIUser_1.id;
                try
                {
                    var user = User.getuserbyid();

                    if (user.field_Private_APIUser_0.id == User)
                            aviid = user.prop_ApiAvatar_0.id;
  
                    exploits.misc.changetoavi(aviid);
                }
                catch { }


            });
            Buttons.Button(menu, "Copy uid", () => System.Windows.Forms.Clipboard.SetText(GameObject.Find("/_Application").transform.Find("UIManager/SelectedUserManager").gameObject.GetComponent<VRC.DataModel.UserSelectionManager>().field_Private_APIUser_1.id));

            Buttons.Button(menu, "Lewd", () => {

                    var id = GameObject.Find("/_Application").transform.Find("UIManager/SelectedUserManager").gameObject.GetComponent<VRC.DataModel.UserSelectionManager>().field_Private_APIUser_1.id;
                exploits.forcelewd.foreelwed(extensions.getuserbyid(id));
            });

        }


    }
}
