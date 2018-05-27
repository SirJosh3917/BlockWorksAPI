using PlayerIOClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockWorksAPI {
	public delegate void OnMessageHandler(Game sender, IBlockWorksMessage e);

	public partial class Game {
		public event EventHandler Ready;
		public event OnMessageHandler OnMessage;

		private Connection con;
		private Client cli;
		private WorldType worldjoin;
		private string nickname;

		private AvatarFaces lastface;
		private AvatarColors lastcolor;
		private Dictionary<int, Player> players;

		public World World { get; internal set; }
		public Player Bot { get; internal set; }
		public ChatLog ChatLog { get; internal set; }

		public ReadOnlyDictionary<int, Player> Players {
			get {
				return new ReadOnlyDictionary<int,Player>(players);
			}
			private set { }
		}

		internal void Send(string type, params object[] args) {
			con.Send(Message.Create(type, args));
		}

		internal Game(Connection con, Client cli, WorldType w, string nickname) {
			con.OnMessage += con_OnMessage;

			this.con = con;
			this.cli = cli;
			this.worldjoin = w;
			this.nickname = nickname;

			this.lastcolor = AvatarColors.Orange;
			this.lastface = AvatarFaces.Happy;

			players = new Dictionary<int, Player>();
			Bot = null;
			ChatLog = new ChatLog(this);
		}
	}
}