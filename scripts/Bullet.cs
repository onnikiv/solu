using Godot;
using System;

public partial class Bullet : Node2D
{
	private const float SPEED = 700f;

	public override void _Ready()
	{
		var notifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
		notifier.ScreenExited += OnScreenExited;
	}

	private void OnScreenExited()
	{
		QueueFree();
	}

	public override void _Process(double delta)
	{
		Position += Transform.X * SPEED * (float)delta;
	}
}
