using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockWorksAPI {
	public class ChatLog {
		internal ChatLog(Game parent) {
			this.parent = parent;
			chatMessages = new List<ChatMsg>();
		}

		internal void AddChat(ChatMsg m) {
			chatMessages.Add(m);
		}

		internal void AddChat(PlayerIOClient.Message e) {
			if (MessageVerifier.VerifyMsg(e))
				AddChat(new ChatMsg(
					e.GetString(1),
					e.GetInt(0),
					this
					));
		}

		internal Game parent;

		internal List<ChatMsg> chatMessages;

		public ReadOnlyCollection<ChatMsg> ChatMessages { get { return chatMessages.AsReadOnly(); } set { } }
	}

	public class ChatMsg {
		internal ChatMsg(string msg, int id, ChatLog parent) {
			ChatRecievedUtc = DateTime.UtcNow;
			this.parent = parent;

			ChatMessage = msg;
			PlayerId = id;
		}

		private ChatLog parent;

		public int PlayerId { get; private set; }
		public Player Player { get { if (parent != null & parent.parent != null & parent.parent.Players != null) if (parent.parent.Players.ContainsKey(PlayerId)) { return parent.parent.Players[PlayerId]; } return null; } private set { } }
		public string ChatMessage { get; private set; }
		public DateTime ChatRecievedUtc { get; private set; }
	}
}
