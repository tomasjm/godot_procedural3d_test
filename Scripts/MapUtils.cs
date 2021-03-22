using System;
using Godot;
[Tool]

public class MapUtils : Spatial {

	[Export]
	public int chunkSize = 10;
	[Export]
	public OpenSimplexNoise noise = new OpenSimplexNoise();
	[Export]
	public float noiseScale = 0.1f;
	
	[Export] 
	public bool update {set { _Ready(); } get{return true;}}
	[Export]
	public int heightMultiplier = 10;
	[Export]
	public Curve heightCurve = new Curve();
	[Export]
	public HeightTerrain heightTerrain;

	public override void _Ready() {
		// ----
		// GD.Print(heightTerrain.Name); // <--- ERROR HERE, PRODUCES A CRASH RUNNING IN EDITOR
		// ----
		for (int i = 0; i < this.GetChildCount(); i++)
		{
			Node child = this.GetChild(i);
			this.RemoveChild(child);
		}
		float [,] noiseMap = Noise.GenerateNoiseMap(noise, chunkSize, noiseScale);
		MeshInstance world = new MeshInstance();
		world.Name = "World";
		PlaneMesh plane = MapGenerator.GeneratePlaneMesh(chunkSize);
		SpatialMaterial mat = new SpatialMaterial();
		
		mat.AlbedoTexture = MapDisplay.DrawNoiseMapToTexture(noiseMap);
		mat.AlbedoTexture.Flags = 1;
		plane.Material = mat;
		ArrayMesh mesh = MapGenerator.AddNoiseToMesh(plane, noiseMap, chunkSize, heightMultiplier, heightCurve);
		world.Mesh = mesh;
		AddChild(world);
		world.Owner = this; 

	}
}
