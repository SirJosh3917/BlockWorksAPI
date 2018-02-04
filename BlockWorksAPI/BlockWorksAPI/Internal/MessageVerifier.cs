using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockWorksAPI {
		internal static class MessageVerifier {
			public static bool VerifyMsg(PlayerIOClient.Message e) {
				var ret = VerifyMsg__TEST__(e);
				if (ret == false) {
					throw new Exception("hey msg shouldn't be dead");
				}

				return ret;
			}
			public static bool VerifyMsg__TEST__(PlayerIOClient.Message e) {
				Console.WriteLine(e.ToString());

				for (uint i = 0; i < e.Count; i++)
					Console.WriteLine(e[i].GetType().ToString());

					switch (e.Type) {
						case "world": {
							if (e.Count > 9)
								if (e[0] is string &&
									e[1] is string &&
									e[2] is uint &&
									e[3] is uint &&
									e[4] is string &&
									e[5] is bool) {
									uint leave = 0;
									for (uint i = 6; i < e.Count; i += 4) {
										if (e.Count <= i)
											return false;

										if (e[i] is string)
											if (e.GetString(i) == "WE") {
												leave = i;
												break;
											}

										while (!(e[i] is int &&
											e[i + 1] is uint &&
											e[i + 2] is byte[] &&
											e[i + 3] is byte[]) && !(e[i] is string))
											i++;

										if (e[i] is string) {
											leave = i;
											break;
										}

										if (!(e[i] is int &&
											e[i + 1] is uint &&
											e[i + 2] is byte[] &&
											e[i + 3] is byte[]))
											return false;
									}
									for (uint i = leave + 2; i < e.Count; i++) {
										if (e.Count <= i)
											return false;
										if (!(e[i] is uint))
											if (e[i] is string) {
												if (e.GetString(i) == "SE")
													return true;
											} else if(!(e[i] is byte[]))
												return false;
									}
								}
							return false;
						}
						case "online": {
							for (uint i = 0; i < e.Count; i+=8)
								if (!(e.Count > i + 7 &&
									e[i] is int &&
									e[i + 1] is string &&
									e[i + 2] is string &&
									e[i + 3] is bool &&
									e[i + 4] is uint &&
									e[i + 5] is int &&
									e[i + 6] is float &&
									e[i + 7] is float))
									return false;
							return true;
						}
						case "you":
						case "join": {
							return e.Count > 6 &&
									e[0] is int &&
									e[1] is string &&
									e[2] is string &&
									e[3] is uint &&
									e[4] is int &&
									e[5] is float &&
									e[6] is float;
						}
						case "left": {
							return e.Count > 0 &&
								e[0] is int;
						}
						case "m": {
							return e.Count > 6 &&
								e[0] is int &&
								e[1] is Single &&
								e[2] is Single &&
								e[3] is int &&
								e[4] is int &&
								e[5] is Single &&
								e[6] is Single;
						}
						case "avatar": {
							return e.Count > 1 &&
								e[0] is int &&
								e[1] is uint;
						}
						case "s": {
							return e.Count > 2 &&
								e[0] is uint &&
								e[1] is uint &&
								e[2] is bool;
						}
						case "fly": {
							return e.Count > 1 &&
								e[0] is int &&
								e[1] is bool;
						}
						case "b": {
							if (e.Count > 2 &&
								e[0] is int &&
								e[1] is uint &&
								e[2] is uint)
								if (e.Count <= 3)
									return true;
								else {
									for (uint i = 3; i < e.Count; i++)
										if (!(e[i] is uint))
											return false;
									return true;
								}
							return false;
						}
						case "say": {
							return e.Count > 1 &&
								e[0] is int &&
								e[1] is string;
						}
						default: {
							return false;
						}
					}
			}
	}
}
