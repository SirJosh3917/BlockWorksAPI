using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockWorksAPI {
	/// <summary>
	/// SAFE ENUM CONVERTER
	/// </summary>
	internal static class SEC {
		public static StaffRank StaffRank(int rank, out bool safe) {
			safe = true;
			if(Enum.IsDefined(typeof(StaffRank), rank))
				return (StaffRank)rank;
			safe = false;
			return BlockWorksAPI.StaffRank.None;
		}
	}
}
