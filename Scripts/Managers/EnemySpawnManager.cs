using Godot;
using System.Collections.Generic;

public partial class EnemySpawnManager : Node
{
	[Export] private PackedScene baseEnemy;
	[Export] private float spawnDelay;
	private Node2D leftSpawner, middleSpawner, rightSpawner, leftAircraftSpawner, rightAircraftSpawner;
	private List<Node2D> spawners = new List<Node2D>();
	private RandomNumberGenerator random = new RandomNumberGenerator();
	private Timer spawnTimer;

	public override void _Ready()
	{
		leftSpawner = GetNode<Node2D>("Left");
		middleSpawner = GetNode<Node2D>("Middle");
		rightSpawner = GetNode<Node2D>("Right");
		leftAircraftSpawner = GetNode<Node2D>("AircraftLeft");
		rightAircraftSpawner = GetNode<Node2D>("AircraftRight");
		spawnTimer = GetNode<Timer>("SpawnTimer");
		spawnTimer.WaitTime = spawnDelay;
		spawnTimer.Start();

		spawners.Add(leftSpawner);
		spawners.Add(middleSpawner);
		spawners.Add(rightSpawner);	
		//Dmitri is gay
	}

	private void SpawnEnemy()
	{
		var spawnedEnemy = baseEnemy.Instantiate<BaseEnemy>();
		spawnedEnemy.GlobalPosition = spawners[PickSpawner()].GlobalPosition;
		GetParent().AddChild(spawnedEnemy);
		if (spawnDelay >= .5f)
		{
			GD.Print(spawnDelay);
			spawnDelay -= .1f;
			spawnTimer.WaitTime = spawnDelay;
		}
	}

	private int PickSpawner()
	{
		int spawner = (int)random.RandiRange(0, 2);
		//GD.Print(spawner);
		return spawner;
	}

	public override void _Process(double delta)
	{

	}
}
