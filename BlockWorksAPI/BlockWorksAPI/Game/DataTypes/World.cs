using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockWorksAPI {
	public class World {
		internal World(PlayerIOClient.Message e) {
			if (e.Type != "world" || !MessageVerifier.VerifyMsg(e))
				throw new Exception("Invalid world message.");

			OwnerId = e.GetString(0);
			Title = e.GetString(1);
			Width = e.GetUInt(2);
			Height = e.GetUInt(3);
			IsCleared = e.GetBoolean(5);
			
			this.blocks = new Block[2, Width, Height];
			channelsopen = new List<uint>();
			uint specialPos = 0;

			//world deserialization
			for (uint i = 6; i < e.Count; i++)
				if (e[i] is string) { //WE
					specialPos = i + 1;
					break;
				}  else
					if (i + 4 < e.Count)
						if (e[i] is int &&
							e[i + 1] is uint &&
							e[i + 2] is byte[] &&
							e[i + 3] is byte[]) {
							var layer = e.GetUInt(i);
							var blockid = e.GetUInt(i + 1);
							var xs = e.GetByteArray(i + 2);
							var ys = e.GetByteArray(i + 3);

							uint iterations = 0;

							if (xs.Length == ys.Length)
								iterations = (uint)xs.Length;
							else
								if (xs.Length > ys.Length)
									iterations = (uint)ys.Length;
								else iterations = (uint)xs.Length;

							uint[] args = null;
							uint argamt = 0;

							for (uint j = 1; j < e.Count - i - 4; j++)
								if (e[j + i] is int &&
									e[j + i + 1] is uint &&
									e[j + i + 2] is byte[] &&
									e[j + i + 3] is byte[]) {
									argamt = j - 4;
									break;
								}

							args = new uint[argamt];

							for (uint j = 0; j < argamt; j++)
								if (e[i + j] is uint)
									args[j] = e.GetUInt(i + j);

							for (uint j = 0; j < iterations; j++)
								if (xs[j] < Width && ys[j] < Height && layer < 2 && layer >= 0)
									blocks[layer, xs[j], ys[j]] = new Block() { X = xs[j], Y = ys[j], Layer = layer, uId = blockid, PlacedBy = -1, Arguments = args };
								else
									Console.WriteLine(string.Format("invalid block: {0}, {1}, {2}, {3}", layer, blockid, xs[j], ys[j]));
						}

			for (uint i = 0; i < e.Count; i++)
				if (e[i] is string)
					return;
				else if (e[i] is uint) {
					channelsopen.Add(e.GetUInt(i));
				}
		}

		public string OwnerId { get; set; }
		public string Title { get; set; }
		public uint Width { get; set; }
		public uint Height { get; set; }
		internal bool IsCleared { get; set; }

		private List<uint> channelsopen;
		public ReadOnlyCollection<uint> ChannelsOpen { get { return channelsopen.AsReadOnly(); } private set { } }

		public bool ChannelIsOpen(uint channel) {
			return channelsopen.Contains(channel);
		}

		internal void SetChannel(uint channel, bool state) {
			if (channelsopen.Contains(channel))
				channelsopen.Remove(channel);
			if(state)
				channelsopen.Add(channel);
		}

		private bool blocksArrayEdited = false;

		private Block[, ,] _blocks;
		private Block[, ,] _blocksCache { get; set; }
		internal Block[, ,] blocks { get { return _blocks; } set { _blocks = value; blocksArrayEdited = true; } }

		public Block[, ,] Blocks { get { if (blocksArrayEdited) { Array.Copy(_blocks, _blocksCache, _blocks.Length); } return _blocksCache; } private set { } }
	}
}
