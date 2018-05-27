using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockWorksAPI;

namespace Tester {
	class Program {
		static bool protect { get; set; } = true;

		static void Main(string[] args) {
			var g = Join.JoinWorld(WorldType.Gray, "apitest");
			g.Ready += g_Ready;
			g.OnMessage += G_OnMessage;
			Console.ReadLine();
		}

		private static void G_OnMessage(Game sender, IBlockWorksMessage m) {
			switch (m.Type) {
				case MessageType.Chat: {
					var e = (BlockWorksChatMessage)m;

					if(e.Player.Nickname == "GUEST-NINJA")
						switch(e.Chat) {
							case "!protect": {
								protect = !protect;
								sender.Say($"Protecting: {protect}");
							} break;
						}

					Console.WriteLine($"{e.Player.Nickname}> {e.Chat}");
				} break;
				case MessageType.Block: {
					var e = (BlockWorksBlockMessage)m;

					if(e.Player.Nickname != "GUEST-NINJA" && e.Player != sender.Bot) {
						//replace it bACK
						if(protect)
						sender.PlaceBlock(sender.World.Blocks[e.Block.Layer, e.Block.X, e.Block.Y]);
					} //otherwise we cool m8
				} break;
			}
		}

		static void g_Ready(object sender, EventArgs e) {
			var g = (Game)sender;
		}
	}
}
