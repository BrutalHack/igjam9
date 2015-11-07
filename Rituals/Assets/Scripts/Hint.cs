using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hint
{
	
	public readonly Sprite SymbolASprite;
	public readonly Sprite RelationshipSprite;
	public readonly Sprite SymbolBSprite;
	public readonly int MinigameScene;

	public Hint (Sprite SymbolASprite, Sprite RelationshipSprite, Sprite SymbolBSprite, int MinigameScene)
	{
		this.SymbolASprite = SymbolASprite;
		this.RelationshipSprite = RelationshipSprite;
		this.SymbolBSprite = SymbolBSprite;
		this.MinigameScene = MinigameScene;
	}
}
