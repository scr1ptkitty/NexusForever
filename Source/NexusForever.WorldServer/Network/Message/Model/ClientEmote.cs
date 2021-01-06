using NexusForever.Shared.Network;
using NexusForever.Shared.Network.Message;
using NexusForever.WorldServer.Game.Social;


namespace NexusForever.WorldServer.Network.Message.Model
{
    [Message(GameMessageOpcode.ClientEmote)]
    public class ClientEmote : IReadable
    {
        public uint EmoteId { get; set; }
        public uint Seed { get; set; }
        public uint Unknown0 { get; set; }
        public uint TargetUnitId { get; set; }
        public bool Targeted { get; set; }
        public bool Silent { get; set; }

        public void Read(GamePacketReader reader)
        {
            EmoteId = reader.ReadUInt(14);
            Seed = reader.ReadUInt();
            TargetUnitId = reader.ReadUInt();
            Targeted = reader.ReadBit();
            Silent = reader.ReadBit();
            Unknown0 = reader.ReadUInt(32);
        }
    }
}
