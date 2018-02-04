using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockWorksAPI {
	public partial class Game {
		#region send
		#region say
		public void Say(string msg) {
			Send("say", msg);
		}
		#endregion

		#region block
		public void PlaceBlock(Block place) {
			//if (World.blocks[place.Layer, place.X, place.Y + 1].Id == 0) //this feature is included serverside, you can't not place switches
			//	return;

			PlayerIOClient.Message m = PlayerIOClient.Message.Create("b", ((place.Layer & 3) << 30 | (place.X & 32767) << 15 | place.Y & 32767), place.uId);

			if (place.Arguments != null) {
				for (uint i = 0; i < place.Arguments.Length; i++)
					m.Add(place.Arguments[i]);
			}

			con.Send(m);
		}

		public void PlaceBlock(uint layer, uint x, uint y, BlockId id, params uint[] arguments) {
			pb(layer, x, y, id, arguments);
		}

		internal void pb(uint layer, uint x, uint y, BlockId id, uint[] arguments = null) {
			if (!Enum.IsDefined(typeof(BlockId), id))
				throw new Exception("Enum id not defined correctly.");

			PlaceBlock(new Block() { Layer = layer, X = x, Y = y, uId = (uint)id, Arguments = arguments });
		}
		#endregion

		#region avatar
		public void SetAvatar(AvatarFaces face, AvatarColors color) {
			Send("avatar", AvatarCalculator.GetAvatarId(face, color));
		}

		#region avatar offspring
		public void SetAvatar(AvatarColors color, AvatarFaces face) {
			SetAvatar(face, color);
		}

		public void SetAvatar(AvatarFaces face) {
			SetAvatar(face, lastcolor);
		}

		public void SetAvatar(AvatarColors color) {
			SetAvatar(lastface, color);
		}
		#endregion
		#endregion
		#endregion
	}
}
