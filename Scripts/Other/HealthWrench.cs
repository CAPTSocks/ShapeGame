using Godot;
using System;

public partial class HealthWrench : CharacterBody2D
{
    [Export] private int healingAmount = 25;
    [Export] private float speed = 300f;

    private void OnBodyEntered(Node body)
    {
        if (body.Name == "Player")
        {
            var playerHealth = body.GetNode<PlayerHealthComponent>("HealthComponent");
            if (playerHealth != null)
            {
                playerHealth.IncrementHealth(healingAmount);
            }
            QueueFree();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = Vector2.Down * speed;
        RotationDegrees += 150 * (float)delta; 
        MoveAndSlide(); 
    }
}
