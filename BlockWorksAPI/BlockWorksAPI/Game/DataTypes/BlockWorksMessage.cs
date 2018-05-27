using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockWorksAPI {
	public interface IBlockWorksMessage {
		MessageType Type { get; }
	}

	public interface IPlayerIdentifiableMessage {
		Player Player { get; set; }
	}

	public interface IWorldMessage : IBlockWorksMessage {

	}

	public interface IOnlinePlayersMessage : IBlockWorksMessage {
		Player[] AlreadyOnlinePlayers { get; set; }
	}

	public interface IJoinMessage : IBlockWorksMessage, IPlayerIdentifiableMessage {

	}

	public interface ILeaveMessage : IBlockWorksMessage, IPlayerIdentifiableMessage {

	}

	public interface IBotMessage : IBlockWorksMessage, IPlayerIdentifiableMessage {

	}

	public interface IMovementMessage : IBlockWorksMessage, IPlayerIdentifiableMessage {
		float X { get; set; }
		float Y { get; set; }
	}

	public interface IAvatarMessage : IBlockWorksMessage, IPlayerIdentifiableMessage {
		AvatarFaces Face { get; set; }
		AvatarColors Color { get; set; }
	}

	public interface IChannelMessage : IBlockWorksMessage {
		uint Channel { get; set; }
		bool State { get; set; }
	}

	public interface IFlyMessage : IBlockWorksMessage, IPlayerIdentifiableMessage {
		bool IsFlying { get; set; }
	}

	public interface IBlockMessage : IBlockWorksMessage, IPlayerIdentifiableMessage {
		Block Block { get; set; }
	}

	public interface IChatMessage : IBlockWorksMessage, IPlayerIdentifiableMessage {
		string Chat { get; set; }
	}

	public class BlockWorksMessage : IBlockWorksMessage {
		public MessageType Type { get; set; }
	}

	public class BlockWorksWorldMessage : IWorldMessage {
		public MessageType Type { get => MessageType.World; }
	}

	public class BlockWorksOnlinePlayersMessage : IOnlinePlayersMessage {
		internal BlockWorksOnlinePlayersMessage() { }

		public MessageType Type { get => MessageType.Online; }
		public Player[] AlreadyOnlinePlayers { get; set; }
	}

	public class BlockWorksJoinMessage : IJoinMessage {
		internal BlockWorksJoinMessage() { }

		public MessageType Type { get => MessageType.Join; }
		public Player Player { get; set; }
	}

	public class BlockWorksLeaveMessage : ILeaveMessage {
		internal BlockWorksLeaveMessage() { }

		public MessageType Type { get => MessageType.Leave; }
		public Player Player { get; set; }
	}

	public class BlockWorksBotMessage : IBotMessage {
		internal BlockWorksBotMessage() { }

		public MessageType Type { get => MessageType.You; }
		public Player Player { get; set; }
	}

	public class BlockWorksMovementMessage : IMovementMessage {
		internal BlockWorksMovementMessage() { }

		public MessageType Type { get => MessageType.Movement; }
		public Player Player { get; set; }
		public float X { get; set; }
		public float Y { get; set; }
	}

	public class BlockWorksAvatarMessage : IAvatarMessage {
		internal BlockWorksAvatarMessage() { }

		public MessageType Type { get => MessageType.Avatar; }
		public Player Player { get; set; }
		public AvatarFaces Face { get; set; }
		public AvatarColors Color { get; set; }
	}

	public class BlockWorksChannelMessage : IChannelMessage {
		internal BlockWorksChannelMessage() { }

		public MessageType Type { get => MessageType.Channel; }
		public uint Channel { get; set; }
		public bool State { get; set; }
	}

	public class BlockWorksFlyMessage : IFlyMessage {
		internal BlockWorksFlyMessage() { }

		public MessageType Type { get => MessageType.Fly; }
		public Player Player { get; set; }
		public bool IsFlying { get; set; }
	}

	public class BlockWorksBlockMessage : IBlockMessage {
		internal BlockWorksBlockMessage() { }

		public MessageType Type { get => MessageType.Block; }
		public Player Player { get; set; }
		public Block Block { get; set; }
	}

	public class BlockWorksChatMessage : IChatMessage {
		internal BlockWorksChatMessage() { }

		public MessageType Type { get => MessageType.Chat; }
		public Player Player { get; set; }
		public string Chat { get; set; }
	}

	public enum MessageType {
		///<summary>Gets fired when the world is deserialized.</summary>
		World,

		///<summary>Gives you a list of every player online.</summary>
		Online,

		///<summary>Gets fired when a player joins the world.</summary>
		Join,

		///<summary>Gets fired whenever a player leaves the world.</summary>
		Leave,

		///<summary>Gets fired once information about you is recieved.</summary>
		You,

		///<summary>Gets fired whenever you get information about somebody moving.</summary>
		Movement,

		///<summary>Gets fired when you get information about avatar.</summary>
		Avatar,

		///<summary>Gets fired when a channel opens/closes.</summary>
		Channel,

		///<summary>Gets fired whenever somebody starts/stops flying.</summary>
		Fly,

		///<summary>Gets fired whenever a block gets placed.</summary>
		Block,

		///<summary>Gets fired whenever a chat message is sent.</summary>
		Chat
	}
}