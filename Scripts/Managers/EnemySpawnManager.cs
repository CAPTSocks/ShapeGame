using Godot;
using System;
using System.Collections.Generic;

public partial class EnemySpawnManager : Node
{
	[Export] private PackedScene baseEnemy;
	[Export] private PackedScene healtWrench;
	[Export] private Godot.Collections.Array<PackedScene> packedLandEnemeies;
	[Export] private Godot.Collections.Array<PackedScene> packedAirEnemeies;

	[Export] private float spawnDelay;
	private Node2D leftSpawner, middleSpawner, rightSpawner, leftAircraftSpawner, rightAircraftSpawner;
	private List<Node2D> landSpawners = new List<Node2D>();
	private List<Node2D> airSpawners = new List<Node2D>();
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

		landSpawners.Add(leftSpawner);
		landSpawners.Add(middleSpawner);
		landSpawners.Add(rightSpawner);	

		airSpawners.Add(leftAircraftSpawner);
		airSpawners.Add(rightAircraftSpawner);
		//Dmitri is gay
	}

	private void HandleSpawning()
	{
		int randomNum = random.RandiRange(0, 100);
		GD.Print("Random health Number " + randomNum);
		if (randomNum <= 1)
		{
			SpawnHealthItem();
			return; 
		}

		SpawnEnemy();
	}

	private void SpawnEnemy()
	{
		int enemyTypeToSpawn = EnemyPicker();

		if (enemyTypeToSpawn == 0)
		{
			int enemyToSpawn = random.RandiRange(0, packedLandEnemeies.Count - 1);
			var spawnedEnemy = packedLandEnemeies[enemyToSpawn].Instantiate<BaseEnemy>();
			spawnedEnemy.GlobalPosition = landSpawners[PickSpawner(true)].GlobalPosition;
			GetParent().AddChild(spawnedEnemy);
		}
		else
		{
			int enemyToSpawn = random.RandiRange(0, packedAirEnemeies.Count - 1);
			var spawnedEnemy = packedAirEnemeies[enemyToSpawn].Instantiate<BaseEnemy>();
			spawnedEnemy.GlobalPosition = airSpawners[PickSpawner(false)].GlobalPosition;
			GetParent().AddChild(spawnedEnemy);
		}
		
		if (spawnDelay >= .5f)
		{
			GD.Print(spawnDelay);
			spawnDelay -= .1f;
			spawnTimer.WaitTime = spawnDelay;
		}
	}

	private int EnemyPicker()
	{
		int type = random.RandiRange(0, 100);
		if (type <= 75)
		{
			//Spawn Land Enemy
			return 0;
		}
		
		//Spawn Air Enemy
		return 1; 
	}

	private int PickSpawner(bool useLandSpawners)
	{
		if (useLandSpawners)
		{
			int spawner = (int)random.RandiRange(0, landSpawners.Count - 1);
			return spawner;
		}
		else
		{
			int airSpawnerInt = (int)random.RandiRange(0, airSpawners.Count - 1);
			return airSpawnerInt;
		}
		
	}

	private void SpawnHealthItem()
	{
		int randomSpawner = PickSpawner(true);
		var spawnedWrench = healtWrench.Instantiate<HealthWrench>();
		spawnedWrench.GlobalPosition = landSpawners[randomSpawner].GlobalPosition;
		GetParent().AddChild(spawnedWrench);

	}

	public override void _Process(double delta)
	{

	}
}
