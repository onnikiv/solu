using Godot;

public partial class Weapon : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Get the global mouse position
		Vector2 mousePosition = GetGlobalMousePosition();
		// Point the weapon towards the mouse
		LookAt(mousePosition);
	}
}
