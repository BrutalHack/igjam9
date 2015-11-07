using UnityEngine;
using System.Collections;

namespace MainGame
{

	public class Connection
	{

		public Symbol SymbolA;
		public Symbol SymbolB;

		public Connection (Symbol SymbolA, Symbol SymbolB)
		{
			this.SymbolA = SymbolA;
			this.SymbolB = SymbolB;
		}
	}
}
