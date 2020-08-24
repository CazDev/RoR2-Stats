using System;
using System.Linq;
using UnityEngine;
using RoR2;

namespace UmbraRoR
{
    public class PlayerMod
    {
        public static int damagePerLvl = 10;
        public static int CritPerLvl = 1;
        public static float attackSpeed = 1;
        public static float armor = 0;
        public static float movespeed = 7;
        public static int jumpCount = 1;
        public static ulong xpToGive = 50;
        public static uint moneyToGive = 50;
        public static uint coinsToGive = 50;

        public static void GiveBuff(GUIStyle buttonStyle,string buttonId)
        {
            int buttonPlacement = 1;
            foreach (string buffName in Enum.GetNames(typeof(BuffIndex)))
            {
                DrawMenu.DrawButton(buttonPlacement, buttonId, buffName, buttonStyle);
                buttonPlacement++;
            }
        }

        public static void RemoveAllBuffs()
        {
            foreach (string buffName in Enum.GetNames(typeof(BuffIndex)))
            {
                BuffIndex buffIndex = (BuffIndex)Enum.Parse(typeof(BuffIndex), buffName);
                while (Main.LocalPlayerBody.HasBuff(buffIndex))
                {
                    Main.LocalPlayerBody.RemoveBuff(buffIndex);
                }
            }
        }

        // self explanatory
        public static void GiveXP()
        {
            Main.LocalPlayer.GiveExperience(xpToGive);
        }

        public static void GiveMoney()
        {
            Main.LocalPlayer.GiveMoney(moneyToGive);
        }

        //uh, duh.
        public static void GiveLunarCoins()
        {
            Main.LocalNetworkUser.AwardLunarCoins(coinsToGive);
        }

        public static void LevelPlayersCrit()
        {
            try
            {
                Main.LocalPlayerBody.levelCrit = (float)CritPerLvl;
            }
            catch (NullReferenceException)
            {
            }
        }

        public static void LevelPlayersDamage()
        {
            try
            {
                Main.LocalPlayerBody.levelDamage = (float)damagePerLvl;
            }
            catch (NullReferenceException)
            {
            }
        }

        public static void SetplayersAttackSpeed()
        {
            try
            {
                Main.LocalPlayerBody.baseAttackSpeed = attackSpeed;
            }
            catch (NullReferenceException)
            {
            }
        }

        public static void SetplayersArmor()
        {
            try
            {
                Main.LocalPlayerBody.baseArmor = armor;
            }
            catch (NullReferenceException)
            {
            }
        }

        public static void SetplayersMoveSpeed()
        {
            try
            {
                Main.LocalPlayerBody.baseMoveSpeed = movespeed;
            }
            catch (NullReferenceException)
            {
            }
        }

        public static void AimBot()
        {
            if (Utility.CursorIsVisible())
            {
                return;
            }

            var localUser = LocalUserManager.GetFirstLocalUser();
            var controller = localUser.cachedMasterController;
            if (!controller)
            {
                return;
            }

            var body = controller.master.GetBody();
            if (!body)
            {
                return;
            }

            var inputBank = body.GetComponent<InputBankTest>();
            var aimRay = new Ray(inputBank.aimOrigin, inputBank.aimDirection);
            var bullseyeSearch = new BullseyeSearch();
            var team = body.GetComponent<TeamComponent>();
            bullseyeSearch.teamMaskFilter = TeamMask.all;
            bullseyeSearch.teamMaskFilter.RemoveTeam(team.teamIndex);
            bullseyeSearch.filterByLoS = true;
            bullseyeSearch.searchOrigin = aimRay.origin;
            bullseyeSearch.searchDirection = aimRay.direction;
            bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
            bullseyeSearch.maxDistanceFilter = float.MaxValue;
            bullseyeSearch.maxAngleFilter = 20f;// ;// float.MaxValue;
            bullseyeSearch.RefreshCandidates();
            var hurtBox = bullseyeSearch.GetResults().FirstOrDefault();
            if (hurtBox)
            {
                Vector3 direction = hurtBox.transform.position - aimRay.origin;
                inputBank.aimDirection = direction;
            }
        }

        //Respawn... Not sure how to implement it.
        public static void AttemptRespawn()
        {
            if (!Main.LocalHealth.alive)
            {
                Main.LocalPlayer.RespawnExtraLife();
                Debug.Log($"{Main.log}: Respawned");
            }
        }

        public static void GodMode()
        {
            Main.LocalHealth.godMode = true;
        }

        public static SurvivorIndex GetCurrentCharacter()
        {
            var bodyIndex = BodyCatalog.FindBodyIndex(Main.LocalPlayerBody);
            var survivorIndex = SurvivorCatalog.GetSurvivorIndexFromBodyIndex(bodyIndex);
            return survivorIndex;
        }

        public static void ChangeCharacter(GUIStyle buttonStyle, string buttonId)
        {
            int buttonPlacement = 1;
            foreach (var prefab in Main.bodyPrefabs)
            {
                DrawMenu.DrawButton(buttonPlacement, buttonId, prefab.name.Replace("Body", ""), buttonStyle);
                buttonPlacement++;
            }
        }

        public static void UnlockAll()
        {
            //Goes through resource file containing all unlockables... Easily updatable, just paste "RoR2.UnlockCatalog" and GetAllUnlockable does the rest.
            //This is needed to unlock logs
            foreach (var unlockableName in Main.unlockableNames)
            {
                var unlockableDef = UnlockableCatalog.GetUnlockableDef(unlockableName);
                NetworkUser networkUser = Util.LookUpBodyNetworkUser(Main.LocalPlayerBody);
                if (networkUser)
                {
                    networkUser.ServerHandleUnlock(unlockableDef);
                }
            }

            //Gives all achievements.
            var achievementManager = AchievementManager.GetUserAchievementManager(LocalUserManager.GetFirstLocalUser());
            foreach (var achievement in AchievementManager.allAchievementDefs)
            {
                achievementManager.GrantAchievement(achievement);
            }

            //Give all survivors
            var profile = LocalUserManager.GetFirstLocalUser().userProfile;
            foreach (var survivor in SurvivorCatalog.allSurvivorDefs)
            {
                if (profile.statSheet.GetStatValueDouble(RoR2.Stats.PerBodyStatDef.totalTimeAlive, survivor.bodyPrefab.name) == 0.0)
                    profile.statSheet.SetStatValueFromString(RoR2.Stats.PerBodyStatDef.totalTimeAlive.FindStatDef(survivor.bodyPrefab.name), "0.1");
            }

            //All items and equipments
            foreach (string itemName in Enum.GetNames(typeof(ItemIndex)))
            {
                ItemIndex itemIndex = (ItemIndex)Enum.Parse(typeof(ItemIndex), itemName);
                profile.DiscoverPickup(PickupCatalog.FindPickupIndex(itemIndex));
            }

            foreach (string equipmentName in Enum.GetNames(typeof(EquipmentIndex)))
            {
                EquipmentIndex equipmentIndex = (EquipmentIndex)Enum.Parse(typeof(EquipmentIndex), equipmentName);
                profile.DiscoverPickup(PickupCatalog.FindPickupIndex(equipmentIndex));
            }
        }
    }
}
