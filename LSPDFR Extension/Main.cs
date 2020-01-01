using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Engine.Scripting.Entities;
using Rage;

namespace Department_of_Transportation_Callouts
{
    public class Main : Plugin
    {
        public override void Initialize()
        {
            Functions.OnOnDutyStateChanged += OnOnDutyStateChangedHandler;
            Game.LogTrivial("Plugin Department of Transportation " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " has been initialised.");
            Game.LogTrivial("Go on duty to fully load Department of Transportation Callouts.");
        }

        public override void Finally()
        {
            Game.LogTrivial("Variety Callouts has been cleaned up.");
        }

        private static void OnOnDutyStateChangedHandler(bool OnDuty)
        {
            if (OnDuty)
            {
                Game.DisplayNotification("~y~Department of Transportation Callouts~w~ by ~b~Abel Gaming~w~ has been loaded.");
                Game.LogTrivial("Department of Transportation Callouts has been loaded version 1.0");
                RegisterCallouts();
            }
        }

        private static void RegisterCallouts()
        {
            Functions.RegisterCallout(typeof(Callouts.USR1));
        }
    }
}
