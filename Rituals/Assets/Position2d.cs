
public class Position2d
{
	public int x;
	public int y;

	public Position2d (int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public void AddLocal (Position2d add)
	{
		this.x += add.x;
		this.y += add.y;
	}

	public Position2d negateLocal ()
	{
		this.x = -this.x;
		this.y = -this.y;
		return this;
	}
}
