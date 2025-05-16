using System;
using System.Transactions;
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
                foreach(var farmer in Game1.getOnlineFarmers()) {
                if(farmer != null) {
                    Monitor.Log(farmer.Name, LogLevel.Info);
                }
            }
            }
        }

        public void tpPlayer(object? sender) {
            
        }

        public List<StardewValley.Farmer> getOnlineFarmers(object? sender) {
            return Game1.getOnlineFarmers().ToList();
        }
    }
}