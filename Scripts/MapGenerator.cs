using Godot;
using System;
public static class MapGenerator {
	public static PlaneMesh GeneratePlaneMesh(int chunkSize) {
		PlaneMesh plane = new PlaneMesh();
		plane.Size = new Vector2(chunkSize, chunkSize);
		plane.SubdivideDepth = chunkSize-2;
		plane.SubdivideWidth = chunkSize-2;
		return plane;
	}
	public static ArrayMesh AddNoiseToMesh(PlaneMesh plane, float[,] noiseMap, int chunkSize, int heightMultiplier, Curve heightCurve) {
		SurfaceTool st = new SurfaceTool();
		st.CreateFrom(plane, 0);
		ArrayMesh mesh = new ArrayMesh();
		MeshDataTool dt = new MeshDataTool();
		mesh = st.Commit();
		dt.CreateFromSurface(mesh, 0);
		for (int y = 0; y < chunkSize; y++)
		{
			for (int x = 0; x < chunkSize; x++)
			{
				int z = y;
				int vertexIndex = z*chunkSize+x;
				Vector3 vertex = dt.GetVertex(vertexIndex);
				vertex.y = heightCurve.Interpolate(noiseMap[chunkSize-x-1,chunkSize-z-1])*heightMultiplier;
				dt.SetVertex(vertexIndex, vertex);
			}
		}
		for (int surface = 0; surface < mesh.GetSurfaceCount(); surface++)
		{
			mesh.SurfaceRemove(surface);
		}
		dt.CommitToSurface(mesh);
		st.Begin(Mesh.PrimitiveType.Triangles);
		st.CreateFrom(mesh, 0);
		st.Index();
		st.GenerateNormals();
		return st.Commit();
	}
}
