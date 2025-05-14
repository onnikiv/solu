using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		switch (direction)
		{
			case (1,0):
			GD.Print("RIGHT");
				break;
			case (-1,0):
				GD.Print("LEFT");
				break;
			case (0,1):
			GD.Print("UP");
				break;

			case (0,-1):
			GD.Print("DOWN");
				break;
		}

    velocity = direction * Speed;
    Velocity = velocity;
    MoveAndSlide();
	}
}
