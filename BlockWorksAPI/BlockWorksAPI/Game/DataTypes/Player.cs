using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockWorksAPI {
	public class Player {
		internal Player() { }

		internal Player(PlayerIOClient.Message e) {
			this.Flying = false;

			if (e.Type != "join" || e.Type != "you")
				return;

			if (MessageVerifier.VerifyMsg(e)) {
				this.PlayerId = e.GetInt(0);
				this.ConnectionId = e.GetString(1);
				this.Nickname = e.GetString(2);
				this.EditRights = e.GetUInt(3) == 1;
				this.AvatarId = (uint)e.GetInt(4);

				this.StaffRank = StaffRank.None;
				if (Enum.IsDefined(typeof(StaffRank), e.GetInt(5)))
					this.StaffRank = (StaffRank)e.GetInt(5);
			}
		}

		public int PlayerId { get; internal set; }
		public string ConnectionId { get; internal set; }
		public string Nickname { get; internal set; }
		public bool EditRights { get; internal set; }
		public StaffRank StaffRank { get; internal set; }
		public bool Flying { get; internal set; }

		public float X { get; internal set; }
		public float Y { get; internal set; }

		#region avatar
		internal uint AvatarId { get; set; }
		public AvatarFaces AvatarFace {
			get {
				return AvatarCalculator.GetFace(AvatarId);
			}
			private set { }
		}

		public AvatarColors AvatarColor {
			get {
				return AvatarCalculator.GetColor(AvatarId);
			}
			private set { }
		}
		#endregion

		public bool Online { get; internal set; }
	}
}
