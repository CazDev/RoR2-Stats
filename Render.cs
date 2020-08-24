using System;
using UnityEngine;
using RoR2;
using System.Collections.Generic;

namespace UmbraRoR
{
    public class Render : MonoBehaviour
    {
        public static void Interactables()
        {
            foreach (PurchaseInteraction purchaseInteraction in Main.purchaseInteractables)
            {
                if (purchaseInteraction.available)
                {
                    float distanceToObject = Vector3.Distance(Camera.main.transform.position, purchaseInteraction.transform.position);
                    Vector3 Position = Camera.main.WorldToScreenPoint(purchaseInteraction.transform.position);
                    var BoundingVector = new Vector3(Position.x, Position.y, Position.z);
                    if (BoundingVector.z > 0.01)
                    {
                        int distance = (int)distanceToObject;
                        String friendlyName = purchaseInteraction.GetDisplayName();
                        int cost = purchaseInteraction.cost;
                        string boxText = $"{friendlyName}\n${cost}\n{distance}m";
                        GUI.Label(new Rect(BoundingVector.x - 50f, (float)Screen.height - BoundingVector.y, 100f, 50f), boxText, Main.renderInteractablesStyle);
                    }
                }
            }

            foreach (TeleporterInteraction teleporterInteraction in Main.teleporterInteractables)
            {
                float distanceToObject = Vector3.Distance(Camera.main.transform.position, teleporterInteraction.transform.position);
                Vector3 Position = Camera.main.WorldToScreenPoint(teleporterInteraction.transform.position);
                var BoundingVector = new Vector3(Position.x, Position.y, Position.z);
                if (BoundingVector.z > 0.01)
                {
                    Main.renderTeleporterStyle.normal.textColor =
                        teleporterInteraction.isIdle ? Color.magenta :
                        teleporterInteraction.isIdleToCharging || teleporterInteraction.isCharging ? Color.yellow :
                        teleporterInteraction.isCharged ? Color.green : Color.yellow;
                    int distance = (int)distanceToObject;
                    String friendlyName = "Teleporter";
                    string status = "" + (
                        teleporterInteraction.isIdle ? "Idle" :
                        teleporterInteraction.isCharging ? "Charging" :
                        teleporterInteraction.isCharged ? "Charged" :
                        teleporterInteraction.isActiveAndEnabled ? "Idle" :
                        teleporterInteraction.isIdleToCharging ? "Idle-Charging" :
                        teleporterInteraction.isInFinalSequence ? "Final-Sequence" :
                        "???");
                    string boxText = $"{friendlyName}\n{status}\n{distance}m";
                    GUI.Label(new Rect(BoundingVector.x - 50f, (float)Screen.height - BoundingVector.y, 100f, 50f), boxText, Main.renderTeleporterStyle);
                }
            }
        }

        // Needs improvement. Causes a lot of lag
        public static void Mobs()
        {
            foreach (var hurtbox in Main.hurtBoxes)
            {
                var mob = HurtBox.FindEntityObject(hurtbox);
                if (mob)
                {
                    Vector3 MobPosition = Camera.main.WorldToScreenPoint(mob.transform.position);
                    var MobBoundingVector = new Vector3(MobPosition.x, MobPosition.y, MobPosition.z);
                    float distanceToMob = Vector3.Distance(Camera.main.transform.position, mob.transform.position);
                    if (MobBoundingVector.z > 0.01)
                    {
                        float width = 100f * (distanceToMob / 100);
                        if (width > 125)
                        {
                            width = 125;
                        }
                        float height = 100f * (distanceToMob / 100);
                        if (height > 125)
                        {
                            height = 125;
                        }
                        string mobName = mob.name.Replace("Body(Clone)", "");
                        int mobDistance = (int)distanceToMob;
                        string mobBoxText = $"{mobName}\n{mobDistance}m";
                        GUI.Label(new Rect(MobBoundingVector.x - 50f, (float)Screen.height - MobBoundingVector.y + 50f, 100f, 50f), mobBoxText, Main.renderMobsStyle);
                    }
                }
            }
        }

        public static void ActiveMods()
        {
            List<string> modsActive = new List<string>();
            Dictionary<string, bool> allMods = new Dictionary<string, bool>()
            {
                { "Aimbot", Main.aimBot },
                { "Always-Sprint", Main.alwaysSprint },
                { "Drop-Items", ItemManager.isDropItemForAll },
                { "Drop-Items-from-Inventory", ItemManager.isDropItemFromInventory },
                { "Flight", Main.FlightToggle },
                { "God-Mode", Main.godToggle },
                { "Jump-Pack", Main.jumpPackToggle },
                { "Keyboard-Navigation", Main.navigationToggle },
                { "Modified-Armor", Main.armorToggle },
                { "Modified-Attack Speed", Main.attackSpeedToggle },
                { "Modified-Crit", Main.critToggle },
                { "Modified-Damage", Main.damageToggle },
                { "Modified-Move-Speed", Main.moveSpeedToggle },
                { "Modified-Regen", Main.regenToggle },
                { "No-Equipment-Cooldown", Main.noEquipmentCooldown },
                { "No-Skill-Cooldowns", Main.skillToggle },
                { "Render-Interactables", Main.renderInteractables },
                { "Render-Mobs", Main.renderMobs }
            };

            string modsBoxText = "";
            Vector2 bottom = new Vector2(0, Screen.height);

            foreach (string modName in allMods.Keys)
            {
                allMods.TryGetValue(modName, out bool active);
                if (active)
                {
                    modsActive.Add(modName);
                }
                else if (modsActive.Contains(modName))
                {
                    modsActive.Remove(modName);
                }
            }

            if (modsActive != null)
            {
                for (int i = 0; i < modsActive.Count; i++)
                {
                    //modsBoxText += $"{modsActive[i]}\n";
                    modsBoxText += $"{modsActive[i]}    ";
                }

                if (modsBoxText != "")
                {
                    GUI.Label(new Rect(Screen.width / 16, bottom.y - 55f, 200, 50f), "Active Mods: ", Main.ActiveModsStyle);
                    GUI.Label(new Rect((Screen.width / 16) + 124, bottom.y - 55f, Screen.width - (Screen.width / 6), 50f), modsBoxText, Main.ActiveModsStyle);
                }
            }
        }
    }
}
