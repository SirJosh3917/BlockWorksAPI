using PlayerIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockWorksAPI
{
    public static class Join {
		public static Game JoinWorld(WorldType w, string nickname) {
			var c = GetClient();
			var con = c.Multiplayer.CreateJoinRoom(GetWorldId(w), GameVersionConstants.RoomType, true, null, new Dictionary<string, string>() { { "Username", nickname } });

			return new Game(con, c, w, nickname);
		}

		private static string GetWorldId(WorldType w) {
			string WorldIdMiddle = "";

			switch (w) {
				case WorldType.Gray: {
					WorldIdMiddle = "Gray";
				} break;
				case WorldType.Red: {
					WorldIdMiddle = "Red";
				} break;
				case WorldType.Yellow: {
					WorldIdMiddle = "Yellow";
				} break;
				case WorldType.Green: {
					WorldIdMiddle = "Greem";
				} break;
				case WorldType.Blue: {
					WorldIdMiddle = "Blue";
				} break;
				case WorldType.Test: {
					WorldIdMiddle = "Test";
				} break;
			}

			return string.Format("OW_{0}-{1}", WorldIdMiddle, GameVersionConstants.GameVersion);
		}

		private static Client GetClient() {
			return PlayerIO.Authenticate(GameVersionConstants.GameId, "public", new Dictionary<string, string>() { { "userId", "test" }, { "password", "no" } }, null);
		}
    }
}
