using Godot;
using System;

public partial class Turkey : CharacterBody2D
{
    private float moveTimer = 0f;
    private float moveInterval = 2.5f; // Move every 5 seconds

    // initial direction the model is facing
    private string modelFacing = "right";

    private AnimatedSprite2D animatedSprite;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _Process(double delta)
    {
        moveTimer += (float)delta;
        if (moveTimer >= moveInterval)
        {
            moveTurkey();
            moveTimer = 0f;
        }
    }

    public void moveTurkey()
    {
        int randomValue = GD.RandRange(0, 3);
        Vector2 position = Position;

        switch (randomValue)
        {
            case 0:
                position.Y -= 10;
                break;
            case 1:
                position.Y += 10;
                break;
            case 2:
                position.X -= 10;
                modelFacing = "left";
                animatedSprite.Play($"idle-{modelFacing}");
                break;
            case 3:
                position.X += 10;
                modelFacing = "right";
                animatedSprite.Play($"idle-{modelFacing}");
                break;
        }
        Position = position;
    }
}