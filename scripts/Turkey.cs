using Godot;
using System;

public partial class Turkey : CharacterBody2D
{
    private float moveTimer = 0f;
    private float moveInterval = 2.5f; // Move every 2.5 seconds

    // initial direction the model is facing
    private string modelFacing = "right";

    private AnimatedSprite2D animatedSprite;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    private Vector2 moveDirection = Vector2.Zero;
    private float moveDistanceLeft = 0f;
    private float moveSpeed = 60f;

    public override void _Process(double delta)
    {
        moveTimer += (float)delta;

        if (moveDistanceLeft > 0f)
        {
            float moveStep = moveSpeed * (float)delta;
            if (moveStep > moveDistanceLeft)
                moveStep = moveDistanceLeft;

            Position += moveDirection * moveStep;
            moveDistanceLeft -= moveStep;

            if (moveDistanceLeft <= 0f)
                {
                    animatedSprite.Play($"idle-{modelFacing}");
                }
        }
        else if (moveTimer >= moveInterval)
        {
            moveTurkey();
            moveTimer = 0f;
        }
    }

    public void moveTurkey()
    {
        int randomValue = GD.RandRange(0, 3);

        switch (randomValue)
        {
            case 0: // Up
                moveDirection = Vector2.Up;
                break;
            case 1: // Down
                moveDirection = Vector2.Down;
                break;
            case 2: // Left
                moveDirection = Vector2.Left;
                modelFacing = "left";
                animatedSprite.Play($"walk-{modelFacing}");
                break;
            case 3: // Right
                moveDirection = Vector2.Right;
                modelFacing = "right";
                animatedSprite.Play($"walk-{modelFacing}");
                break;
        }

        // Only play walk animation for up/down
        if (randomValue == 0 || randomValue == 1)
            animatedSprite.Play($"idle-{modelFacing}");

        moveDistanceLeft = 10f; // Move 10 pixels smoothly
    }
}