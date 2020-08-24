using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UmbraRoR
{
    class Navigation
    {
        public static float menuIndex = 0;
        public static int intraMenuIndex = -1;
        public static Tuple<float, int> highlightedBtn = new Tuple<float, int>(menuIndex, intraMenuIndex);

        public static Dictionary<float, string> ExtentionMenuList = new Dictionary<float, string>()
        {
            { 1.1f, "CharacterMenu" },
            { 1.2f, "BuffMenu" },
            { 1.3f, "StatsModMenu"},
        };

        public static string[] MainBtnNav = { "PlayerMod", "ItemMang", "Teleporter", "Render", "LobbyMang" };
        public static string[] PlayerBtnNav = { "GiveMoney", "GiveCoin", "GiveXP", "DmgPerLVL", "CritPerLVL", "AttSpeed", "Armor", "MoveSpeed", "CharacterMenu", "Stat", "BuffMenu", "RemoveBuffs", "Aimbot", "GodMode", "NoSkillCD", "UnlockAll" };
        public static string[] StatsBtnNav = { "DmgPerLVL", "CritPerLVL", "AttSpeed", "Armor", "MoveSpeed", "Stat" };

        // Goes to previous menu when backspace or left arrow is pressed
        public static void GoBackAMenu()
        {
            switch (Navigation.menuIndex)
            {
                case 0: // Main Menu 
                    {
                        if (intraMenuIndex == 5)
                        {
                            Main.unloadConfirm = false;
                        }
                        else
                        {
                            Main.navigationToggle = false;
                            menuIndex = 0;
                            intraMenuIndex = -1;
                        }
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        // Prevents menuIndex and intraMenuIndex from going out of range
        public static void UpdateIndexValues()
        {
            switch (menuIndex)
            {
                case 0: // Main Menu 0 - 7
                    {
                        if (intraMenuIndex > 7)
                        {
                            intraMenuIndex = 0;
                        }
                        if (intraMenuIndex < 0)
                        {
                            intraMenuIndex = 7;
                        }
                        break;
                    }

                case 1: // Player Management Menu 0 - 10
                    {
                        if (intraMenuIndex > 10)
                        {
                            intraMenuIndex = 0;
                        }
                        if (intraMenuIndex < 0)
                        {
                            intraMenuIndex = 10;
                        }
                        break;
                    }

                case 1.1f: // Change Character Menu
                    {
                        DrawMenu.characterScrollPosition.y = 40 * intraMenuIndex;

                        if (intraMenuIndex > Main.bodyPrefabs.Count - 1)
                        {
                            intraMenuIndex = 0;
                        }
                        if (intraMenuIndex < 0)
                        {
                            intraMenuIndex = Main.bodyPrefabs.Count - 1;
                        }

                        if (DrawMenu.characterScrollPosition.y > (Main.bodyPrefabs.Count - 1) * 40)
                        {
                            DrawMenu.characterScrollPosition = Vector2.zero;
                        }
                        if (DrawMenu.characterScrollPosition.y < 0)
                        {
                            DrawMenu.characterScrollPosition.y = (Main.bodyPrefabs.Count - 1) * 40;
                        }
                        break;
                    }

                case 1.2f: // Give Buff Menu
                    {
                        DrawMenu.buffMenuScrollPosition.y = 40 * intraMenuIndex;

                        if (intraMenuIndex > Enum.GetNames(typeof(BuffIndex)).Length - 1)
                        {
                            intraMenuIndex = 0;
                        }
                        if (intraMenuIndex < 0)
                        {
                            intraMenuIndex = Enum.GetNames(typeof(BuffIndex)).Length - 1;
                        }

                        if (DrawMenu.buffMenuScrollPosition.y > (Enum.GetNames(typeof(BuffIndex)).Length - 1) * 40)
                        {
                            DrawMenu.buffMenuScrollPosition = Vector2.zero;
                        }
                        if (DrawMenu.buffMenuScrollPosition.y < 0)
                        {
                            DrawMenu.buffMenuScrollPosition.y = (Enum.GetNames(typeof(BuffIndex)).Length - 1) * 40;
                        }
                        break;
                    }

                case 1.3f: // Stats Modification Menu 0-5
                    {

                        if (intraMenuIndex > 5)
                        {
                            intraMenuIndex = 0;
                        }
                        if (intraMenuIndex < 0)
                        {
                            intraMenuIndex = 5;
                        }
                        break;
                    }

                case 2: // Movement Menu 0 - 2
                    {
                        if (intraMenuIndex > 2)
                        {
                            intraMenuIndex = 0;
                        }
                        if (intraMenuIndex < 0)
                        {
                            intraMenuIndex = 2;
                        }
                        break;
                    }

                case 3: // Item Management Menu 0 - 8
                    {
                        if (intraMenuIndex > 8)
                        {
                            intraMenuIndex = 0;
                        }
                        if (intraMenuIndex < 0)
                        {
                            intraMenuIndex = 8;
                        }
                        break;
                    }

                case 3.1f: // Give Item Menu
                    {
                        DrawMenu.itemSpawnerScrollPosition.y = 40 * intraMenuIndex;

                        if (intraMenuIndex > Main.items.Count - 1)
                        {
                            intraMenuIndex = 0;
                        }
                        if (intraMenuIndex < 0)
                        {
                            intraMenuIndex = Main.items.Count - 1;
                        }

                        if (DrawMenu.itemSpawnerScrollPosition.y > (Main.items.Count - 1) * 40)
                        {
                            DrawMenu.itemSpawnerScrollPosition = Vector2.zero;
                        }
                        if (DrawMenu.itemSpawnerScrollPosition.y < 0)
                        {
                            DrawMenu.itemSpawnerScrollPosition.y = (Main.items.Count - 1) * 40;
                        }
                        break;
                    }

                case 3.2f: // Give Equip Menu
                    {
                        DrawMenu.equipmentSpawnerScrollPosition.y = 40 * intraMenuIndex;

                        if (intraMenuIndex > Main.equipment.Count - 1)
                        {
                            intraMenuIndex = 0;
                        }
                        if (intraMenuIndex < 0)
                        {
                            intraMenuIndex = Main.equipment.Count - 1;
                        }

                        if (DrawMenu.equipmentSpawnerScrollPosition.y > (Main.equipment.Count - 1) * 40)
                        {
                            DrawMenu.equipmentSpawnerScrollPosition = Vector2.zero;
                        }
                        if (DrawMenu.equipmentSpawnerScrollPosition.y < 0)
                        {
                            DrawMenu.equipmentSpawnerScrollPosition.y = (Main.equipment.Count - 1) * 40;
                        }
                        break;
                    }

                case 4: // Spawn Menu 0 - 5
                    {
                        if (intraMenuIndex > 5)
                        {
                            intraMenuIndex = 0;
                        }
                        if (intraMenuIndex < 0)
                        {
                            intraMenuIndex = 5;
                        }
                        break;
                    }

                case 4.1f: // Spawn List Menu
                    {
                        DrawMenu.spawnScrollPosition.y = 40 * intraMenuIndex;

                        if (intraMenuIndex > Main.spawnCards.Count - 1)
                        {
                            intraMenuIndex = 0;
                        }
                        if (intraMenuIndex < 0)
                        {
                            intraMenuIndex = Main.spawnCards.Count - 1;
                        }

                        if (DrawMenu.spawnScrollPosition.y > (Main.spawnCards.Count - 1) * 40)
                        {
                            DrawMenu.spawnScrollPosition = Vector2.zero;
                        }
                        if (DrawMenu.spawnScrollPosition.y < 0)
                        {
                            DrawMenu.spawnScrollPosition.y = (Main.spawnCards.Count - 1) * 40;
                        }
                        break;
                    }

                case 5: // Teleporter Menu 0 - 6
                    {
                        if (intraMenuIndex > 6)
                        {
                            intraMenuIndex = 0;
                        }
                        if (intraMenuIndex < 0)
                        {
                            intraMenuIndex = 6;
                        }
                        break;
                    }

                case 6: // Render Menu 0 - 2
                    {
                        if (intraMenuIndex > 2)
                        {
                            intraMenuIndex = 0;
                        }
                        if (intraMenuIndex < 0)
                        {
                            intraMenuIndex = 2;
                        }
                        break;
                    }

                case 7: // Lobby Management Menu 0 - 3
                    {
                        if (Main.numberOfPlayers > 0)
                        {
                            if (intraMenuIndex > Main.numberOfPlayers - 1)
                            {
                                intraMenuIndex = 0;
                            }
                            if (intraMenuIndex < 0)
                            {
                                intraMenuIndex = Main.numberOfPlayers - 1;
                            }
                        }
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }
    }
}
