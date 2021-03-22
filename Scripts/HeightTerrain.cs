using Godot;
using System;
[Global("HeightTerrain")]
public class HeightTerrain : Resource {

	[Export]
	public string Name;
	[Export]
	public float Height;

	[Export]
	public Color TerrainColor;

}
