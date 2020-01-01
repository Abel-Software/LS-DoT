using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rage;
using Rage.Native;
using RAGENativeUI;
using RAGENativeUI.Elements;

[assembly: Rage.Attributes.Plugin("LS DoT", Description = "Version 2.1", Author = "AbelGaming")]
namespace LS_DoT
{
    internal static class Settings
    {
        internal static Keys OpenMenu = Keys.F8;

        internal static void LoadSettings()
        {
            string path = "plugins/LSDoT.ini";
            InitializationFile ini = new InitializationFile(path);
            ini.Create();

            OpenMenu = ini.ReadEnum<Keys>("Keys", "OpenMenu", Keys.F8);

            //Game Log
            Game.LogTrivial("LS DoT settings have been loaded.");
        }
    }

    public static class EntryPoint
    {
        private static UIMenu mainMenu;
        private static MenuPool _menuPool;
        private static Player player = Game.LocalPlayer;
        private static Vector3 playerlocation = Game.LocalPlayer.Character.Position;

        //Cones
        private static Rage.Object cone;

        //Vector 3 Locations
        

        //Blips
        

        //Speed Zones
        private static uint speedzone1;
        private static Blip SpeedArea1;

        public static void Main()
        {
            Settings.LoadSettings();

            Game.DisplayNotification("~y~Los Santos Department of Transportation ~w~by ~b~Abel Gaming~w~ has loaded.");

            _menuPool = new MenuPool();
            mainMenu = new UIMenu("Los Santos ~y~DoT", "Mod by Abel Gaming | Version 1.0");
            _menuPool.Add(mainMenu);
            PlayerModelMenu(mainMenu);
            VehicleMenu(mainMenu);
            DoTTools(mainMenu);

            //Important Things
            mainMenu.AllowCameraMovement = true;
            mainMenu.MouseControlsEnabled = false;
            MainLogic();
            GameFiber.Hibernate();
        }

        public static void PlayerModelMenu(UIMenu menu)
        {
            //Create sub menu
            var playermodelmenu = _menuPool.AddSubMenu(menu, "Player Models");
            for (int i = 0; i < 1; i++) ;
            playermodelmenu.AllowCameraMovement = true;
            playermodelmenu.MouseControlsEnabled = false;

            //Construction 1
            var construction1 = new UIMenuItem("Male Construction", "");
            playermodelmenu.AddItem(construction1);
            playermodelmenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == construction1)
                {
                    Game.LocalPlayer.Model = "S_M_Y_Construct_01";
                    Game.LocalPlayer.Character.RandomizeVariation();
                    Game.LogTrivial("Model changed to " + Game.LocalPlayer.Model.Name);
                }
            };

            //Construction 2
            var construction2 = new UIMenuItem("Male Construction", "");
            playermodelmenu.AddItem(construction2);
            playermodelmenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == construction2)
                {
                    Game.LocalPlayer.Model = "S_M_Y_Construct_02";
                    Game.LocalPlayer.Character.RandomizeVariation();
                    Game.LogTrivial("Model changed to " + Game.LocalPlayer.Model.Name);
                }
            };

            //Mechanic
            var mechanic = new UIMenuItem("Mechanic", "");
            playermodelmenu.AddItem(mechanic);
            playermodelmenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == mechanic)
                {
                    Game.LocalPlayer.Model = "S_M_M_AutoShop_01";
                    Game.LocalPlayer.Character.RandomizeVariation();
                    Game.LogTrivial("Model changed to " + Game.LocalPlayer.Model.Name);
                }
            };

            //Garbage
            var garbage = new UIMenuItem("Garbage Worker", "");
            playermodelmenu.AddItem(garbage);
            playermodelmenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == garbage)
                {
                    Game.LocalPlayer.Model = "S_M_Y_Garbage";
                    Game.LocalPlayer.Character.RandomizeVariation();
                    Game.LogTrivial("Model changed to " + Game.LocalPlayer.Model.Name);
                }
            };
        }

        public static void VehicleMenu(UIMenu menu)
        {
            //Create sub menu
            var vehiclemenusub = _menuPool.AddSubMenu(menu, "Vehicles");
            for (int i = 0; i < 1; i++) ;
            vehiclemenusub.AllowCameraMovement = true;
            vehiclemenusub.MouseControlsEnabled = false;

            //Add Pick Up Truck Menu
            var pickuptrucks = _menuPool.AddSubMenu(vehiclemenusub, "Pick Ups");
            for (int i = 0; i < 1; i++) ;

            //Vapid Utility
            var vapid = new UIMenuItem("Vapid Utility", "");
            pickuptrucks.AddItem(vapid);
            pickuptrucks.OnItemSelect += (sender, item, index) =>
            {
                if (item == vapid)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("utillitruck3", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Sadler Utility
            var Sadler = new UIMenuItem("Sadler Utility", "");
            pickuptrucks.AddItem(Sadler);
            pickuptrucks.OnItemSelect += (sender, item, index) =>
            {
                if (item == Sadler)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("sadler", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Bison Utility
            var bison1 = new UIMenuItem("Bison Utility", "McGill-Olsen Livery");
            pickuptrucks.AddItem(bison1);
            pickuptrucks.OnItemSelect += (sender, item, index) =>
            {
                if (item == bison1)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("bison2", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Bison Utility
            var bison2 = new UIMenuItem("Bison Utility", "The Mighty Bush Livery");
            pickuptrucks.AddItem(bison2);
            pickuptrucks.OnItemSelect += (sender, item, index) =>
            {
                if (item == bison2)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("bison3", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Add Tow Truck Menu
            var towtrucks = _menuPool.AddSubMenu(vehiclemenusub, "Tow Trucks");
            for (int i = 0; i < 1; i++) ;

            //Small Tow Truck
            var small = new UIMenuItem("Small Tow Truck", "");
            towtrucks.AddItem(small);
            towtrucks.OnItemSelect += (sender, item, index) =>
            {
                if (item == small)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("towtruck2", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Large Tow Truck
            var large = new UIMenuItem("Large Tow Truck", "");
            towtrucks.AddItem(large);
            towtrucks.OnItemSelect += (sender, item, index) =>
            {
                if (item == large)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("towtruck", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Add Heavy Equipment Menu
            var heavyequipment = _menuPool.AddSubMenu(vehiclemenusub, "Heavy Equipment");
            for (int i = 0; i < 1; i++) ;

            //Large Crane Truck
            var crane = new UIMenuItem("Utility Crane Truck", "");
            heavyequipment.AddItem(crane);
            heavyequipment.OnItemSelect += (sender, item, index) =>
            {
                if (item == crane)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("utillitruck", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Large Utility Truck
            var largeutility = new UIMenuItem("Large Utility Truck", "");
            heavyequipment.AddItem(largeutility);
            heavyequipment.OnItemSelect += (sender, item, index) =>
            {
                if (item == largeutility)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("utillitruck2", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //HVY Dump Truck
            var hvydump = new UIMenuItem("HVY Dump Truck", "");
            heavyequipment.AddItem(hvydump);
            heavyequipment.OnItemSelect += (sender, item, index) =>
            {
                if (item == hvydump)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("dump", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Rubble Dump Truck
            var rubbledump = new UIMenuItem("Rubble Dump Truck", "");
            heavyequipment.AddItem(rubbledump);
            heavyequipment.OnItemSelect += (sender, item, index) =>
            {
                if (item == rubbledump)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("rubble", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Concrete Mixer
            var concretemixer = new UIMenuItem("Concrete Mixer", "");
            heavyequipment.AddItem(concretemixer);
            heavyequipment.OnItemSelect += (sender, item, index) =>
            {
                if (item == concretemixer)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("mixer2", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //LS Works Department Truck
            var lsworkstruck = new UIMenuItem("LS Works Department Truck", "");
            heavyequipment.AddItem(lsworkstruck);
            heavyequipment.OnItemSelect += (sender, item, index) =>
            {
                if (item == lsworkstruck)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("boxville", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Flatbed
            var Flatbed = new UIMenuItem("Flatbed", "");
            heavyequipment.AddItem(Flatbed);
            heavyequipment.OnItemSelect += (sender, item, index) =>
            {
                if (item == Flatbed)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("flatbed", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Add Heavy Equipment Menu
            var machinery = _menuPool.AddSubMenu(vehiclemenusub, "Machinery");
            for (int i = 0; i < 1; i++) ;

            //Bulldozer
            var bulldozer = new UIMenuItem("Bulldozer", "");
            machinery.AddItem(bulldozer);
            machinery.OnItemSelect += (sender, item, index) =>
            {
                if (item == bulldozer)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("bulldozer", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Forklift
            var Forklift = new UIMenuItem("Forklift", "");
            machinery.AddItem(Forklift);
            machinery.OnItemSelect += (sender, item, index) =>
            {
                if (item == Forklift)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("forklift", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Cutter
            var Cutter = new UIMenuItem("Cutter", "");
            machinery.AddItem(Cutter);
            machinery.OnItemSelect += (sender, item, index) =>
            {
                if (item == Cutter)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Delete();
                    }
                    Vehicle car = new Vehicle("cutter", Game.LocalPlayer.Character.Position);
                    Game.LocalPlayer.Character.WarpIntoVehicle(car, -1);
                    Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                }
            };

            //Add Heavy Equipment Menu
            var trailers = _menuPool.AddSubMenu(vehiclemenusub, "Trailers");
            for (int i = 0; i < 1; i++) ;

            //Small Pick-Up Truck Trailer
            var trailersmall = new UIMenuItem("Small Pickup Trailer", "");
            trailers.AddItem(trailersmall);
            trailers.OnItemSelect += (sender, item, index) =>
            {
                if (item == trailersmall)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Vehicle car = new Vehicle("trailersmall", Game.LocalPlayer.Character.Position.Around(5f));
                        Vehicle currentvehicle = Game.LocalPlayer.Character.CurrentVehicle;
                        currentvehicle.Trailer = car;
                        Game.LogTrivial("Vehicle: " + car.Model.Name + " has been spawned.");
                    }
                }
            };

            //Add Options Menu
            var options = _menuPool.AddSubMenu(vehiclemenusub, "Vehicle Options");
            for (int i = 0; i < 1; i++) ;

            //Small Pick-Up Truck Trailer
            var repair = new UIMenuItem("Repair Vehicle", "");
            options.AddItem(repair);
            options.OnItemSelect += (sender, item, index) =>
            {
                if (item == repair)
                {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(true))
                    {
                        Game.LocalPlayer.Character.CurrentVehicle.Repair();
                    }
                }
            };
        }

        public static void DoTTools(UIMenu menu)
        {
            var dottoolsub = _menuPool.AddSubMenu(menu, "Tools");
            for (int i = 0; i < 1; i++) ;
            dottoolsub.AllowCameraMovement = true;
            dottoolsub.MouseControlsEnabled = false;

            var speedzones = _menuPool.AddSubMenu(dottoolsub, "Speed Zones");
            for (int i = 0; i < 1; i++) ;

            //Add Speed Zone - 10MPH
            var ten = new UIMenuItem("10 MPH", "");
            speedzones.AddItem(ten);
            speedzones.OnItemSelect += (sender, item, index) =>
            {
                if (item == ten)
                {
                    if (ten.Text == "10 MPH")
                    {
                        speedzone1 = World.AddSpeedZone(Game.LocalPlayer.Character.Position, 20f, 10.0f);
                        SpeedArea1 = new Blip(Game.LocalPlayer.Character.Position, 20f);
                        SpeedArea1.Alpha = 0.5f;
                        SpeedArea1.Color = Color.Orange;
                        Vehicle car = Game.LocalPlayer.Character.CurrentVehicle;
                        Game.DisplayNotification("~y~Speed Zone: ~o~10 MPH");
                        ten.Text = "Remove Current Speed Zone";
                        Game.LogTrivial("Speed zone added");
                    }
                    else
                    {
                        World.RemoveSpeedZone(speedZoneHandle: speedzone1);
                        SpeedArea1.Delete();
                        Vehicle car = Game.LocalPlayer.Character.CurrentVehicle;
                        ten.Text = "10 MPH";
                        Game.LogTrivial("Speed zone removed");
                    }
                }
            };

            var roadtools = _menuPool.AddSubMenu(dottoolsub, "Road Tools");
            for (int i = 0; i < 1; i++) ;

            //Road Cone
            var roadcone = new UIMenuItem("Road Cone", "~r~Will be added later on. Currently experiencing issues.");
            roadcone.Enabled = false;
            roadtools.AddItem(roadcone);
            roadtools.OnItemSelect += (sender, item, index) =>
            {
                if (item == roadcone)
                {
                    Vector3 spawnposition = Game.LocalPlayer.Character.FrontPosition;
                    cone = new Rage.Object("prop_roadcone01a", spawnposition, Game.LocalPlayer.Character.Heading);
                    
                    Game.LogTrivial("Road cone spawned");
                }
            };

            var misc = _menuPool.AddSubMenu(dottoolsub, "Misc Tools");
            for (int i = 0; i < 1; i++) ;

            //Clean World
            var cleanworld = new UIMenuItem("Clean World", "");
            misc.AddItem(cleanworld);
            misc.OnItemSelect += (sender, item, index) =>
            {
                if (item == cleanworld)
                {
                    World.CleanWorld(false, true, false, false, false, true);
                }
            };
        }

        //Last Thing
        public static void MainLogic()
        {
            GameFiber.StartNew(delegate
            {
                while (true)
                {
                    GameFiber.Yield();
                    if (Game.IsKeyDown(Settings.OpenMenu))
                    {
                        mainMenu.Visible = !mainMenu.Visible;
                    }
                    _menuPool.ProcessMenus();
                }
            });
        }
    }
}
