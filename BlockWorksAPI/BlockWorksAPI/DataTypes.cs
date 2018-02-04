using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockWorksAPI {
	public static class GameVersionConstants {
		public const int GameVersion = 1;
		public const string GameId = "blockworks-frdrlhtjneoipehnx9tmg";
		public const string RoomType = "Simple-1";
	}

	public enum BlockId : uint {
		Erase = 0,
		Gray = 1,
		Red = 2,
		Yellow = 3,
		Green = 4,
		Blue = 5,
		Dirt = 6,
		Grass = 7,
		Metal = 8,
		SwitchPad = 9,
		SwitchDoor = 10
	}

	public enum WorldType : uint {
		Gray = 0,
		Red = 1,
		Yellow = 2,
		Green = 3,
		Blue = 4,
		Test = 5
	}

	public enum AvatarColors : uint {
		Red = 0,
		Orange = 1,
		Yellow = 2,
		Green = 3,
		Blue = 4,
		Purple = 5,
		Pink = 6
	}

	public enum AvatarFaces : uint {
		Happy = 0,
		Grin = 1,
		Neutral = 2,
		Sad = 3,
		Mad = 4,
		Interested = 5
	}

	public enum StaffRank : int {
		None = -1,
		Unknown = 0,
		Moderator = 1,
		Developer = 2,
		Administrator = 3
	}
}
