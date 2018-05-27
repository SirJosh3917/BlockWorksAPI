using PlayerIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockWorksAPI {
	public partial class Game {
		internal void con_OnMessage(object sender, Message e) {
			Console.WriteLine(e.ToString());

			if (MessageVerifier.VerifyMsg(e)) //this class is very helpful. verifies the message is correct in it's parameters so you won't have to check. very sweet <3
				switch (e.Type) {
					case "world": {
						World = new World(e);

						OnMessage?.Invoke(this, new BlockWorksWorldMessage() {

						});
					} break;

					case "online": {
						var pls = new List<Player>();

						for (uint i = 0; i < e.Count; i += 8) {
							bool playerStaffRankSafelyAdded;
							var p = new Player() {
								AvatarId = e.GetUInt(i + 4),
								ConnectionId = e.GetString(i + 1),
								EditRights = e.GetBoolean(i + 3),
								Nickname = e.GetString(i + 2),
								StaffRank = SEC.StaffRank(e.GetInt(i + 5), out playerStaffRankSafelyAdded),
								PlayerId = e.GetInt(i),
								Online = true
							};

							players.Add(e.GetInt(i), p);

							pls.Add(p);

							if (!playerStaffRankSafelyAdded)
								System.Diagnostics.Debugger.Log(1, "Warning", "Player added to players list, HOWEVER their staff rank was unsafely added.");
						}

						OnMessage?.Invoke(this, new BlockWorksOnlinePlayersMessage() {
							AlreadyOnlinePlayers = pls.ToArray()
						});
					} break;

					case "join": {
						var p = new Player(e);
						players[e.GetInt(0)] = new Player(e);

						OnMessage?.Invoke(this, new BlockWorksJoinMessage() {
							Player = p
						});
					} break;

					case "left": {
						players[e.GetInt(0)].Online = false;

						OnMessage?.Invoke(this, new BlockWorksLeaveMessage() {
							Player = players[e.GetInt(0)]
						});
					} break;

					case "you": {
						Bot = new Player(e);

						players[e.GetInt(0)] = Bot;

						con.Send("ready");

						Ready?.Invoke(this, new EventArgs());

						OnMessage?.Invoke(this, new BlockWorksBotMessage() {
							Player = Bot
						});
					} break;

					case "m": {
						if (players.ContainsKey(e.GetInt(0))) {
							players[e.GetInt(0)].X = e.GetFloat(5);
							players[e.GetInt(0)].Y = e.GetFloat(6);

							OnMessage?.Invoke(this, new BlockWorksMovementMessage() {
								Player = players[e.GetInt(0)],
								X = e.GetFloat(5),
								Y = e.GetFloat(6)
							});
						}
					} break;

					case "avatar": {
						if (players.ContainsKey(e.GetInt(0))) {
							players[e.GetInt(0)].AvatarId = e.GetUInt(1);

							OnMessage?.Invoke(this, new BlockWorksAvatarMessage() {
								Player = players[e.GetInt(0)],
								Color = players[e.GetInt(0)].AvatarColor,
								Face = players[e.GetInt(0)].AvatarFace
							});
						}
					} break;

					case "s": {
						// TODO: use e[0]'s uint to validify if said channel switch exists in spot.
						World.SetChannel(e.GetUInt(1), e.GetBoolean(2));

						OnMessage?.Invoke(this, new BlockWorksChannelMessage() {
							Channel = e.GetUInt(1),
							State = e.GetBoolean(2)
						});
					} break;

					case "fly": {
						if (players.ContainsKey(e.GetInt(0))) {
							players[e.GetInt(0)].Flying = e.GetBoolean(1);

							OnMessage?.Invoke(this, new BlockWorksFlyMessage() {
								Player = players[e.GetInt(0)],
								IsFlying = e.GetBoolean(1)
							});
						}
					} break;

					case "b": {
						var value = e.GetUInt(1);
						var layer = value >> 30 & 3;
						var bx = value >> 15 & 32767;
						var by = value & 32767;

						uint[] args = null;
						uint argamt = 0;

						for(uint i = 3; i < e.Count; i++)
							argamt = i - 2;

						args = new uint[argamt];
						
						for(uint i = 3; i < e.Count; i++)
							args[i - 3] = e.GetUInt(i);

						var b = new Block() {
							Arguments = args,
							Layer = layer,
							X = bx,
							Y = by,
							uId = e.GetUInt(2),
							PlacedBy = e.GetInt(0)
						};
						
						OnMessage?.Invoke(this, new BlockWorksBlockMessage() {
							Player = players[e.GetInt(0)],
							Block = b
						});

						World.Blocks[layer, bx, by] = b;
					}
					break;

					case "say": {
						ChatLog.AddChat(e);

						OnMessage?.Invoke(this, new BlockWorksChatMessage() {
							Player = players[e.GetInt(0)],
							Chat = e.GetString(1)
						});
					} break;

				}
		}
	}
}