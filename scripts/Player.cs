using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public const float Speed = 200.0f;

    private string playerFacing = "south";
    private AnimatedSprite2D animatedSprite;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Vector2.Zero;

        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");


            if (direction == Vector2.Right)
            {
                animatedSprite.Play("animation_walk_east");
                playerFacing = "east";
            }
            else if (direction == Vector2.Left)
            {
                animatedSprite.Play("animation_walk_west");
                playerFacing = "west";
            }
            else if (direction == Vector2.Up)
            {
                animatedSprite.Play("animation_walk_north");
                playerFacing = "north";
            }
            else if (direction == Vector2.Down)
            {
                animatedSprite.Play("animation_walk_south");
                playerFacing = "south";
            }
            else
            {
                animatedSprite.Play($"animation_idle_{playerFacing}");
            }

        velocity = direction * Speed;
        Velocity = velocity;
        MoveAndSlide();
    }
}