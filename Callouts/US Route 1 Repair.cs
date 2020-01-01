using System;
using System.Drawing;
using Rage;
using Rage.Native;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Engine.Scripting.Entities;

namespace Department_of_Transportation_Callouts.Callouts
{
    [CalloutInfo("US Route 1 Repair", CalloutProbability.Medium)]
    public class USR1 : Callout
    {
        //Private References
        private Vector3 truckspawn;
        private Ped AIWorker;
        private Vehicle AITruck;
        private Blip powerstationblip;
        private static uint speedzone;
        private bool OnScene = false;
        private bool Conversation = false;


        public override bool OnBeforeCalloutDisplayed()
        {
            //Create Truck Spawn
            truckspawn = new Vector3(1568.51f, 862.2019f, 77.07944f);

            //Create AI
            AIWorker = new Ped("S_M_Y_Construct_01", truckspawn.Around(5f), 23.00689f);
            AIWorker.IsPersistent = true;
            AIWorker.Tasks.StandStill(-1);

            //Create Truck
            AITruck = new Vehicle("utillitruck3", truckspawn, 23.00689f);
            AITruck.IsPersistent = true;

            //Create SpeedZone
            speedzone = World.AddSpeedZone(truckspawn, 15f, 10f);

            //Create Callout Area
            this.ShowCalloutAreaBlipBeforeAccepting(truckspawn, 15f);
            this.AddMinimumDistanceCheck(5f, truckspawn);
            this.AddMaximumDistanceCheck(300f, truckspawn);

            //Create Callout Message
            this.CalloutMessage = "Power station repair on US Route 1";
            this.CalloutPosition = truckspawn;

            //Play Scanner Audio
            Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_TRAFFIC_ALERT IN_OR_ON_POSITION", this.truckspawn);

            //Friendly Name
            FriendlyName = "Power station repair";

            //Last Line
            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            //Create Blip To Truck
            powerstationblip = AITruck.AttachBlip();
            powerstationblip.Color = Color.Yellow;

            //Draw Route
            powerstationblip.EnableRoute(Color.Yellow);
            powerstationblip.IsRouteEnabled = true;
            powerstationblip.RouteColor = Color.Yellow;

            //Displays Notification
            Game.DisplayNotification("Make your way to the power station to make repairs.");

            //Last Line
            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            //First Line
            base.OnCalloutNotAccepted();
            if (AIWorker.Exists()) { AIWorker.Dismiss(); }
            if (AITruck.Exists()) { AITruck.Dismiss(); }
            if (powerstationblip.Exists()) { powerstationblip.Delete(); }
        }

        public override void Process()
        {
            //First Line
            base.Process();

            if (!OnScene && Game.LocalPlayer.Character.DistanceTo(AITruck) < 20f)
            {
                Game.DisplayNotification("When you arrive, talk to the supervisor. He will inform you what to repair.");
                OnScene = true;
            }

            if (OnScene && !Conversation && Game.LocalPlayer.Character.DistanceTo(AIWorker) < 5f)
            {
                //Supervisor
                Game.DisplaySubtitle("~y~Thank you for coming out so soon. Customers in the area are reporting their power being out.", 7500);
                GameFiber.Sleep(5000);

                //You
                Game.DisplaySubtitle("~g~No problem! What can I help you fix?", 5000);
                GameFiber.Sleep(5000);

                //Supervisor
                Game.DisplaySubtitle("~y~I think some lightning struck one of the power boxes. Can you check it out?", 6500);

                Game.DisplayHelp("Go and inspect the power boxes. Press ~r~END~w~ when you are ready to end the call.");

                Conversation = true;
            }

            if (Conversation && Game.IsKeyDown(System.Windows.Forms.Keys.End))
            {
                End();
            }
        }

        public override void End()
        {
            //First Line
            base.End();
            if (AIWorker.Exists()) { AIWorker.Dismiss(); }
            if (AITruck.Exists()) { AITruck.Dismiss(); }
            if (powerstationblip.Exists()) { powerstationblip.Delete(); }
        }
    }
}
