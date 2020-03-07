using System;

namespace Migraine_v2.DiscordRPC {
    public class RpcControl {
        public static DiscordRpc.RichPresence presence;
        public static DiscordRpc.EventHandlers handlers;

        public static void Initialize(string clientId) {
            RpcControl.handlers = new DiscordRpc.EventHandlers  {
                readyCallback = new DiscordRpc.ReadyCallback(RpcControl.ReadyCallback)
            };
            RpcControl.handlers.disconnectedCallback = (DiscordRpc.DisconnectedCallback)Delegate.Combine(RpcControl.handlers.disconnectedCallback, new DiscordRpc.DisconnectedCallback(RpcControl.DisconnectedCallback));
            RpcControl.handlers.errorCallback = (DiscordRpc.ErrorCallback)Delegate.Combine(RpcControl.handlers.errorCallback, new DiscordRpc.ErrorCallback(RpcControl.ErrorCallback));
            DiscordRpc.Initialize(clientId, ref RpcControl.handlers, true, null);
        }

        public static void UpdatePresence(string details, string state, int one = 0, int two = 0, string imageAsset = "mainimage", string ImageText = "", bool FromCustom = false)
        {
            bool flag = !FromCustom && Globals.CustomRPC;
            if (!flag)  {
                RpcControl.presence.details = details;
                RpcControl.presence.state = state;
                RpcControl.presence.largeImageKey = imageAsset;
                RpcControl.presence.smallImageKey = imageAsset;
                RpcControl.presence.largeImageText = ImageText;
                RpcControl.presence.smallImageText = ImageText;
                long startTimestamp;
                bool flag2 = long.TryParse(RpcControl.DateTimeToTimestamp(DateTime.UtcNow).ToString(), out startTimestamp);
                if (flag2) {
                    RpcControl.presence.startTimestamp = startTimestamp;
                }
                bool flag3 = one != 0;
                if (flag3) {
                    RpcControl.presence.partySize = one;
                    RpcControl.presence.partyMax = two;
                }
                DiscordRpc.UpdatePresence(ref RpcControl.presence);
            }
        }

        public static void RunCallbacks() => DiscordRpc.RunCallbacks();
        public static void Shutdown() => DiscordRpc.Shutdown();
        public static void ReadyCallback() { }
        public static void DisconnectedCallback(int errorCode, string message) { }
        public static void ErrorCallback(int errorCode, string message)  { }
        public static long DateTimeToTimestamp(DateTime dt) {  return (dt.Ticks - 621355968000000000L) / 10000000L; }
    }
}
