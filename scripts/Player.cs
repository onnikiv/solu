using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public const float Speed = 200.0f;

    // initial direction the player model is facing
    private string playerFacing = "down";
    private AnimatedSprite2D animatedSprite;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Vector2.Zero;
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        if (direction == Vector2.Zero)
        {
            animatedSprite.Play($"idle-{playerFacing}");
        }
        else
        {
            if (direction.X > 0)
            {
                animatedSprite.Play("walk-right");
                playerFacing = "right";
            }
            else if (direction.X < 0)
            {
                animatedSprite.Play("walk-left");
                playerFacing = "left";
            }

            if (direction.Y < 0)
            {
                if (direction.X == 0)
                {
                    animatedSprite.Play("walk-up");
                    playerFacing = "up";
                }
            }
            else if (direction.Y > 0)
            {
                if (direction.X == 0)
                {
                    animatedSprite.Play("walk-down");
                    playerFacing = "down";
                }
            }
        }

        velocity = direction.Normalized() * Speed;
        Velocity = velocity;
        MoveAndSlide();
    }
}
