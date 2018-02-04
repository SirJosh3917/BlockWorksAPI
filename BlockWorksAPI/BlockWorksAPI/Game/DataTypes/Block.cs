using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockWorksAPI {
	public struct Block {
		public static Block Create(uint layer, uint x, uint y, BlockParams blockparams) {
			return new Block() {
				Layer = layer,
				X = x,
				Y = y,
				uId = blockparams.Id,
				Arguments = blockparams.Args
			};
		}

		/// <summary>The PlayerId of the player responsible for placing the block.</summary>
		public int PlacedBy { get; internal set; }

		/// <summary>X position of the block</summary>
		public uint X { get; internal set; }

		/// <summary>Y position of the block</summary>
		public uint Y { get; internal set; }

		/// <summary>Block ID of the block</summary>
		public BlockId Id { get { return (BlockId)uId; } private set { } }
		internal uint uId { get; set; }

		/// <summary>Extra arguments ( to be named )</summary>
		public uint[] Arguments { get; internal set; }

		/// <summary>Layer of the block</summary>
		public uint Layer { get; internal set; }

		public void Place(Game to) {
			to.PlaceBlock(this);
		}
	}

	public class BlockParams {
		private BlockParams() {

		}

		public static BlockParams CreateSimple(BlockId block) {
			if (!Enum.IsDefined(typeof(BlockId), block))
				throw new Exception("Enum id not defined correctly.");

			return new BlockParams() { Id = (uint)block };
		}

		public static BlockParams CreateSwitchPad(uint Channel) {
			return new BlockParams() { Id = (uint)BlockId.SwitchPad, Args = new uint[] { Channel } };
		}

		public static BlockParams CreateSwitchDoor(uint Channel, bool Inverted) {
			return new BlockParams() { Id = (uint)BlockId.SwitchDoor, Args = new uint[] { Channel, (uint)(Inverted ? 1 : 0) } };
		}

		internal uint Id { get; set; }
		internal uint[] Args { get; set; }

	}
}
