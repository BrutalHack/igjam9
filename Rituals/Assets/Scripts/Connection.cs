using UnityEngine;
using System.Collections;

namespace MainGame
{

	public class Connection
	{

		public readonly int symbolAId;
		public readonly int symbolBId;

		public Symbol symbolA;
		public Symbol symbolB;

		public Connection (int symboleAPosition, int symboleBPosition)
		{
			symbolAId = symboleAPosition;
			symbolBId = symboleBPosition;
		}

		public override bool Equals (System.Object obj)
		{
			if (obj == null) {
				return false;
			}

			if (obj is Connection) {
				return Equals (obj as Connection);
			} else {
				return false;
			}
		}

		public bool Equals (Connection con)
		{
			if (con == null) {
				return false;
			}

			return (symbolAId == con.symbolAId) && (symbolBId == con.symbolBId);
		}

		public override int GetHashCode ()
		{
			return symbolAId ^ symbolBId;
		}

		public void SetSymbolsByIds (Symbol[] symbols)
		{
			symbolA = symbols [symbolAId];
			symbolB = symbols [symbolBId];
		}
	}
}
