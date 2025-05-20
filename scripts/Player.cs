using Godot;
using System;
using System.Collections.Generic;
public partial class Player : CharacterBody2D
{
	public const float SPEED = 150.0f;

	// Animations - walk, idle
	private string playerFacing = "down";
	private AnimatedSprite2D animatedSprite2D;

	// Weapon
	private Node2D weaponSlot;
	private Node currentWeapon;

	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play($"idle-{playerFacing}");

		weaponSlot = GetNode<Node2D>("WeaponSlot");

	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		MovePlayer(velocity, direction);

		if (Input.IsActionJustPressed("ui_accept"))
		{
			DropWeapon();
		}
	}

	public void MovePlayer(Vector2 velocity, Vector2 direction)
	{
		if (direction != Vector2.Zero)
		{

			if (Math.Abs(direction.X) > Math.Abs(direction.Y))
				playerFacing = direction.X > 0 ? "right" : "left";
			else
				playerFacing = direction.Y > 0 ? "down" : "up";

			animatedSprite2D.Play($"walk-{playerFacing}");
			velocity = direction * SPEED;
			Velocity = velocity;
			MoveAndSlide();
		}
		else
		{
			animatedSprite2D.Play($"idle-{playerFacing}");
		}
	}


	public void EquipWeapon(PackedScene weaponScene)
	{
		currentWeapon?.QueueFree();

		currentWeapon = weaponScene.Instantiate();
		weaponSlot.AddChild(currentWeapon);
	}
	
	public void UnEquipWeapon()
	{
		if (currentWeapon != null)
		{
			currentWeapon.QueueFree();
			currentWeapon = null;
		}
	}

	public void DropWeapon()
	{
		if (currentWeapon != null)
		{

			Node droppedWeapon = currentWeapon;
			currentWeapon = null;
			droppedWeapon.GetParent().RemoveChild(droppedWeapon);

			GetTree().Root.AddChild(droppedWeapon);
			if (droppedWeapon is Node2D node2D)
			{
				node2D.GlobalPosition = GlobalPosition;
			}
		}
	}
}
