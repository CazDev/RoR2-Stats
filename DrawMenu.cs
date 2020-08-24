using RoR2;
using System;
using UnityEngine;

namespace UmbraRoR
{
    internal class DrawMenu
    {
        public static Vector2 itemSpawnerScrollPosition = Vector2.zero;
        public static Vector2 equipmentSpawnerScrollPosition = Vector2.zero;
        public static Vector2 buffMenuScrollPosition = Vector2.zero;
        public static Vector2 characterScrollPosition = Vector2.zero;
        public static Vector2 spawnScrollPosition = Vector2.zero;

        public static void DrawMainMenu(float x, float y, float widthSize, float mulY, GUIStyle BGstyle, GUIStyle OnStyle, GUIStyle OffStyle, GUIStyle ButtonStyle)
        {
            if (Main.unloadConfirm)
            {
                DrawButton(8, "main", "C O N F I R M ?", ButtonStyle);
            }
            else
            {
                DrawButton(8, "main", "U N L O A D   M E N U", ButtonStyle);
            }
        }

        public static void DrawStatsMenu(float x, float y, float widthSize, int mulY, GUIStyle BGstyle, GUIStyle buttonStyle, GUIStyle LabelStyle)
        {
            GUI.Box(new Rect(x + 0f, y + 0f, widthSize, 50f + 45 * mulY), "", BGstyle);
            GUI.Label(new Rect(x + 5f, y + 5f, widthSize + 5, 50f), "S T A T S", LabelStyle);

            DrawButton(1, "stats", $"DAMAGE : {Main.LocalPlayerBody.damage}", buttonStyle, justText: true);
            DrawButton(2, "stats", $"CRIT : {Main.LocalPlayerBody.crit}", buttonStyle, justText: true);
            DrawButton(3, "stats", $"ATTACK SPEED : {Main.LocalPlayerBody.attackSpeed}", buttonStyle, justText: true);
            DrawButton(4, "stats", $"ARMOR : {Main.LocalPlayerBody.armor}", buttonStyle, justText: true);
            DrawButton(5, "stats", $"REGEN : {Main.LocalPlayerBody.regen}", buttonStyle, justText: true);
            DrawButton(6, "stats", $"MOVE SPEED : {Main.LocalPlayerBody.moveSpeed}", buttonStyle, justText: true);
            DrawButton(7, "stats", $"JUMP COUNT : {Main.LocalPlayerBody.maxJumpCount}", buttonStyle, justText: true);
            DrawButton(8, "stats", $"EXPERIENCE : {Main.LocalPlayerBody.experience}", buttonStyle, justText: true);
            DrawButton(9, "stats", $"KILLS: {Main.LocalPlayerBody.killCount}", buttonStyle, justText: true);

        }

        // A Wrapper Method For Drawing Buttons
        public static void DrawButton(int position, string id, string text, GUIStyle defaultStyle, bool isMultButton = false, bool justText = false)
        {
            Rect rect;
            Rect menuBg;
            GUIStyle highlighted = Main.HighlightBtnStyle;
            float menuIndex;
            int intraMenuIndex = position - 1;
            int btnY = 5 + 45 * position;
            // Rect for buttons
            // It automatically position buttons based on id and position. There is no need to change it
            switch (id)
            {
                case "main":
                    {
                        menuIndex = 0;
                        menuBg = Main.mainRect;
                        Main.MainMulY = position;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.mainRect.x + 5, Main.mainRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.mainRect.x + 5, Main.mainRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "playermod":
                    {
                        menuIndex = 1;
                        menuBg = Main.playerModRect;
                        Main.PlayerModMulY = position;
                        Main.PlayerModBtnY = btnY;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.playerModRect.x + 5, Main.playerModRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.playerModRect.x + 5, Main.playerModRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "character":
                    {
                        menuIndex = 1.1f;
                        menuBg = Main.characterRect;
                        Main.CharacterMulY = position;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.characterRect.x + 5, Main.characterRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.characterRect.x + 5, Main.characterRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "giveBuff":
                    {
                        menuIndex = 1.2f;
                        menuBg = Main.buffMenuRect;
                        Main.buffMenuMulY = position;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.buffMenuRect.x + 5, Main.buffMenuRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.buffMenuRect.x + 5, Main.buffMenuRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "statsmod":
                    {
                        menuIndex = 1.3f;
                        menuBg = Main.editStatsRect;
                        Main.editStatsMulY = position;
                        Main.editStatsBtnY = btnY;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.editStatsRect.x + 5, Main.editStatsRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.editStatsRect.x + 5, Main.editStatsRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "movement":
                    {
                        menuIndex = 2f;
                        menuBg = Main.movementRect;
                        Main.movementMulY = position;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.movementRect.x + 5, Main.movementRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.movementRect.x + 5, Main.movementRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "itemmanager":
                    {
                        menuIndex = 3f;
                        menuBg = Main.itemManagerRect;
                        Main.ItemManagerMulY = position;
                        Main.ItemManagerBtnY = btnY;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.itemManagerRect.x + 5, Main.itemManagerRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.itemManagerRect.x + 5, Main.itemManagerRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "itemSpawner":
                    {
                        menuIndex = 3.1f;
                        menuBg = Main.itemSpawnerRect;
                        Main.itemSpawnerMulY = position;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.itemSpawnerRect.x + 5, Main.itemSpawnerRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.itemSpawnerRect.x + 5, Main.itemSpawnerRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "equipmentSpawner":
                    {
                        menuIndex = 3.2f;
                        menuBg = Main.equipmentSpawnerRect;
                        Main.equipmentSpawnerMulY = position;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.equipmentSpawnerRect.x + 5, Main.equipmentSpawnerRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.equipmentSpawnerRect.x + 5, Main.equipmentSpawnerRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "spawn":
                    {
                        menuIndex = 4f;
                        menuBg = Main.spawnRect;
                        Main.spawnMulY = position;
                        Main.spawnBtnY = btnY;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.spawnRect.x + 5, Main.spawnRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.spawnRect.x + 5, Main.spawnRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "spawnMob":
                    {
                        menuIndex = 4.1f;
                        menuBg = Main.spawnListRect;
                        Main.spawnListMulY = position;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.spawnListRect.x + 5, Main.spawnListRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.spawnListRect.x + 5, Main.spawnListRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "tele":
                    {
                        menuIndex = 5f;
                        menuBg = Main.teleRect;
                        Main.TeleMulY = position;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.teleRect.x + 5, Main.teleRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.teleRect.x + 5, Main.teleRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "ESP":
                    {
                        menuIndex = 6f;
                        menuBg = Main.ESPRect;
                        Main.ESPMulY = position;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.ESPRect.x + 5, Main.ESPRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.ESPRect.x + 5, Main.ESPRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "lobby":
                    {
                        menuIndex = 7f;
                        menuBg = Main.lobbyRect;
                        Main.LobbyMulY = position;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.lobbyRect.x + 5, Main.lobbyRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.lobbyRect.x + 5, Main.lobbyRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                case "stats":
                    {
                        menuIndex = 99f;
                        menuBg = Main.statRect;
                        Main.StatMulY = position;
                        if (isMultButton)
                        {
                            rect = new Rect(Main.statRect.x + 5, Main.statRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        }
                        else
                        {
                            rect = new Rect(Main.statRect.x + 5, Main.statRect.y + 5 + 45 * position, Main.widthSize, 40);
                        }
                        break;
                    }

                default:
                    {
                        menuBg = Main.mainRect;
                        menuIndex = 0;
                        rect = new Rect(Main.mainRect.x + 5, Main.mainRect.y + 5 + 45 * position, Main.widthSize - 90, 40);
                        break;
                    }
            }

            // Creates the button and its OnClick action based on PressBtn() input
            // Dont want text to be highlighted so remove that from the Button() call
            if (justText && isMultButton)
            {
                throw new Exception($"justText and isMultButton cannot both be true. Thrown in \"{text}\" button.");
            }
            else if (justText)
            {
                GUI.Button(rect, text, defaultStyle);
            }
            else if (isMultButton)
            {
            }
        }
    }
}
