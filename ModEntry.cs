using System;
using System.Transactions;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using xTile.Format;

namespace friendTP
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        IModHelper? hp = null;
        
        public override void Entry(IModHelper helper) {
            hp = helper;
            helper.Events.GameLoop.TimeChanged += this.OnTimeChanged;
        }

        public void OnTimeChanged(object? sender, TimeChangedEventArgs e) {
            Monitor.Log(e.NewTime.ToString(), LogLevel.Info);
            int currentTime = e.NewTime;
            if(currentTime % 600 == 0) {
                Monitor.Log("6 hours have passed", LogLevel.Info);
                tpPlayer(sender);
            }
        }

        public void tpPlayer(object? sender) {
            Random rand = new Random();
            List<StardewValley.Farmer> onlineFarmers = getOnlineFarmers(sender);
            onlineFarmers.Remove(Game1.player);
            StardewValley.Farmer target = onlineFarmers[rand.Next(onlineFarmers.Count)];
            Vector2 targetTile = getTileLocation(target);
            Game1.warpFarmer(target.currentLocation.Name, (int) targetTile.X, (int) targetTile.Y, false);
            Game1.chatBox.addMessage($"Teleported {Game1.player.Name} to {target.Name}", Color.White);
        }

        public List<StardewValley.Farmer> getOnlineFarmers(object? sender) {
            return Game1.getOnlineFarmers().ToList();
        }

        private static Vector2 getTileLocation(StardewValley.Farmer farmer) {
            return farmer.Position / 64f;
        }
    }
}