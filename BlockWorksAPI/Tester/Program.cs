using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockWorksAPI;

namespace Tester {
	class Program {

		static void Main(string[] args) {
			var g = Join.JoinWorld(WorldType.Gray, "apitest");
			g.Ready += g_Ready;
			Console.ReadLine();
		}

		static void g_Ready(object sender, EventArgs e) {
			var g = (Game)sender;
		}
	}
}
