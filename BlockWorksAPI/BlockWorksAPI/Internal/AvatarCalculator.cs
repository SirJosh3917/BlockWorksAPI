using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockWorksAPI{
	internal static class AvatarCalculator {
		public static uint GetAvatarId(AvatarFaces face, AvatarColors color) {
			return (Convert.ToUInt32(face) * 128) + Convert.ToUInt32(color);
		}

		public static uint GetAvatarId(AvatarColors color, AvatarFaces face) {
			return GetAvatarId(face, color);
		}

		public static AvatarColors GetColor(uint avatarid) {
			return (AvatarColors)(avatarid % 128);
		}

		public static AvatarFaces GetFace(uint avatarid) {
			return (AvatarFaces)((avatarid - (avatarid % 128)) / 128);
		}
	}
}
