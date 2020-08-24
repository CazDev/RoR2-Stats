using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using RoR2;
using UnityEngine;

namespace UmbraRoR
{
    public class Utility : MonoBehaviour
    {
        #region Menu Resets
        // Reset menu when you return to main menu
        public static void ResetMenu()
        {
            Main._ifDragged = false;
            Main._CharacterCollected = false;
            Main._isStatMenuOpen = false;
            Main.unloadConfirm = false;
            ItemManager.itemsToRoll = 5;
            ItemManager.isDropItemForAll = false;
            ItemManager.isDropItemFromInventory = false;
            ItemManager.allItemsQuantity = 1;
            PlayerMod.damagePerLvl = 10;
            PlayerMod.CritPerLvl = 1;
            PlayerMod.attackSpeed = 1;
            PlayerMod.armor = 0;
            PlayerMod.movespeed = 7;
            PlayerMod.jumpCount = 1;
            PlayerMod.xpToGive = 50;
            PlayerMod.moneyToGive = 50;
            PlayerMod.coinsToGive = 50;
        }

        public static void CloseAllMenus()
        {
            Main._CharacterCollected = false;
        }

        // Soft reset when moving to next stage to keep player stat mods and god mode between stages
        public static void SoftResetMenu()
        {
            Main._isMenuOpen = !Main._isMenuOpen;
            Main.GetCharacter();
            Main._isMenuOpen = !Main._isMenuOpen;
            Main.godToggle = !Main.godToggle;
            Main.GetCharacter();
            Main.godToggle = !Main.godToggle;
        }
        #endregion

        #region Get Lists
        public static List<string> GetAllUnlockables()
        {
            var unlockableName = new List<string>();

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "UmbraRoR.Resources.Unlockables.txt";

            Stream stream = assembly.GetManifestResourceStream(resourceName);
            StreamReader reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                string line1 = reader.ReadLine();
                line1 = line1.Replace("UnlockableCatalog.RegisterUnlockable(\"", "");
                line1 = line1.Replace("\", new UnlockableDef", "");
                line1 = line1.Replace("true", "");
                line1 = line1.Replace("});", "");
                line1 = line1.Replace("=", "");
                line1 = line1.Replace("\"", "");
                line1 = line1.Replace("false", "");
                line1 = line1.Replace(",", "");
                line1 = line1.Replace("hidden", "");
                line1 = line1.Replace("{", "");
                line1 = line1.Replace("nameToken", "");
                line1 = line1.Replace(" ", "");
                string[] lineArray = line1.Split(null);
                foreach (var line in lineArray)
                {
                    // TODO: Simplify later
                    if (line.StartsWith("Logs.") || line.StartsWith("Characters.") || line.StartsWith("Items.") || line.StartsWith("Skills.") || line.StartsWith("Skins.") || line.StartsWith("Shop.") || line.StartsWith("Artifacts.") || line.StartsWith("NewtStatue."))
                    {
                        unlockableName.Add(line);
                    }
                }
            }
            return unlockableName;
        }

        public static List<GameObject> GetBodyPrefabs()
        {
            List<GameObject> bodyPrefabs = new List<GameObject>();

            foreach (var prefab in BodyCatalog.allBodyPrefabs)
            {
                if (prefab.name != "ScavSackProjectile")
                {
                    bodyPrefabs.Add(prefab);
                }
            }
            return bodyPrefabs;
        }

        public static List<EquipmentIndex> GetEquipment()
        {
            List<EquipmentIndex> equipment = new List<EquipmentIndex>();

            string[] unreleasedEquipment = { "Count" };
            // string[] unreleasedEquipment = { "SoulJar", "AffixYellow", "AffixGold", "GhostGun", "OrbitalLaser", "Enigma", "LunarPotion", "SoulCorruptor", "Count" };
            foreach (string equipmentName in Enum.GetNames(typeof(EquipmentIndex)))
            {
                bool unreleasednullEquipment = unreleasedEquipment.Any(equipmentName.Contains);
                if (!unreleasednullEquipment)
                {
                    EquipmentIndex equipmentIndex = (EquipmentIndex)Enum.Parse(typeof(EquipmentIndex), equipmentName);
                    equipment.Add(equipmentIndex);
                }
            }
            return equipment;
        }

        public static List<ItemIndex> GetItems()
        {
            List<ItemIndex> items = new List<ItemIndex>();

            // List of null items that I remove from the item list. Will change if requested.
            string[] unreleasedItems = { "LevelBonus", "PlantOnHit", "MageAttunement", "BoostHp", "BoostDamage", "CritHeal", "BurnNearby", "CrippleWardOnLevel", "ExtraLifeConsumed", "Ghost", "HealthDecay", "DrizzlePlayerHelper", "MonsoonPlayerHelper", "BoostAttackSpeed", "Count", "None" };
            // string[] unreleasedItems = { "AACannon", "PlasmaCore", "LevelBonus", "CooldownOnCrit", "PlantOnHit", "MageAttunement", "BoostHp", "BoostDamage", "CritHeal", "BurnNearby", "CrippleWardOnLevel", "ExtraLifeConsumed", "Ghost", "HealthDecay", "DrizzlePlayerHelper", "MonsoonPlayerHelper", "TempestOnKill", "BoostAttackSpeed", "Count", "None" };
            foreach (string itemName in Enum.GetNames(typeof(ItemIndex)))
            {
                bool unreleasednullItem = unreleasedItems.Any(itemName.Contains);
                ItemIndex itemIndex = (ItemIndex)Enum.Parse(typeof(ItemIndex), itemName);
                if (!unreleasednullItem)
                {
                    items.Add(itemIndex);
                }
                // Since "Ghost" is null item, "GhostOnKill" was getting removed from item list.
                else if (itemName == "GhostOnKill")
                {
                    items.Add(itemIndex);
                }
            }
            return items;
        }

        public static List<SpawnCard> GetSpawnCards()
        {
            List<SpawnCard> spawnCards = Resources.FindObjectsOfTypeAll<SpawnCard>().ToList();
            return spawnCards;
        }

        public static List<UnityEngine.Object> GetPurchaseInteractions()
        {
            var purchaseInteractables = FindObjectsOfType(typeof(PurchaseInteraction)).ToList();
            return purchaseInteractables;
        }

        public static List<UnityEngine.Object> GetTeleporterInteractions()
        {
            var teleporterInteractions = FindObjectsOfType(typeof(TeleporterInteraction)).ToList();
            return teleporterInteractions;
        }

        public static List<HurtBox> GetHurtBoxes()
        {
            var localUser = LocalUserManager.GetFirstLocalUser();
            var controller = localUser.cachedMasterController;
            if (!controller)
            {
                return null;
            }
            var body = controller.master.GetBody();
            if (!body)
            {
                return null;
            }
            var inputBank = body.GetComponent<InputBankTest>();
            var aimRay = new Ray(inputBank.aimOrigin, inputBank.aimDirection);
            var bullseyeSearch = new BullseyeSearch();
            var team = body.GetComponent<TeamComponent>();
            bullseyeSearch.searchOrigin = aimRay.origin;
            bullseyeSearch.searchDirection = aimRay.direction;
            bullseyeSearch.filterByLoS = false;
            bullseyeSearch.maxDistanceFilter = 125;
            bullseyeSearch.maxAngleFilter = 40f;
            bullseyeSearch.teamMaskFilter = TeamMask.all;
            bullseyeSearch.teamMaskFilter.RemoveTeam(team.teamIndex);
            bullseyeSearch.RefreshCandidates();
            var hurtBoxList = bullseyeSearch.GetResults().ToList();
            return hurtBoxList;
        }
        #endregion

        public static bool CursorIsVisible()
        {
            foreach (var mpeventSystem in RoR2.UI.MPEventSystem.readOnlyInstancesList)
                if (mpeventSystem.isCursorVisible)
                    return true;
            return false;
        }

        public static void LoadAssembly()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                String resourceName = "UmbraRoR." +
                   new AssemblyName(args.Name).Name + ".dll";

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
        }

        #region Debugging
        public static void WriteToLog(string logContent)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, "UmbraLog.txt"), true))
            {
                outputFile.WriteLine(Main.log + logContent);
            }
        }
        #endregion
    }
}
