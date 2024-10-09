using System.Diagnostics;
using Godot;

public partial class PlayerSpawnPoint : Node2D
{
    private CharacterBody2D playerRef;
    private GM gmRef;
    private bool movePlayer;
    private float moveSpeed = 1000;
    public override void _Ready()
    {
        playerRef = GetParent().GetNode<CharacterBody2D>("Player");
        gmRef =  GetTree().Root.GetNode<GM>("Gm");
        gmRef.StartGame += StartGame;
        playerRef.Position = new Vector2(playerRef.Position.X, playerRef.Position.Y + 600);
    }

    private void StartGame()
    {
        movePlayer = true;
    }

    public override void _ExitTree()
    {
        gmRef.StartGame -= StartGame;
    }

    public override void _Process(double delta)
    {
        if (movePlayer)
        {
            playerRef.Position = new Vector2(playerRef.Position.X, Mathf.MoveToward(playerRef.Position.Y, Position.Y, moveSpeed * (float)delta));
        }

        if (Position.DistanceTo(playerRef.Position) < .1f)
        {
            movePlayer = false;
            SetProcess(false);
            QueueFree(); 
        }
    }


}
