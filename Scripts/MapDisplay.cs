using Godot;
using System;

public static class MapDisplay {
	
	
	public static ImageTexture DrawNoiseMapToTexture(float[,] noiseMap) {
		int width = noiseMap.GetLength(0);
		int height = noiseMap.GetLength(1);
		Image image = new Image();
		image.Create(width, height, false, Image.Format.Rgb8);
		image.Lock();
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Color mapPixelColor;
				if (noiseMap[x,y] > 0.78f) {
					mapPixelColor = new Color(1,1,1,1);
				} else if (noiseMap[x,y] > 0.7f) {
					mapPixelColor = new Color(0.35f,0.35f, 0.35f,1);
				} else if (noiseMap[x,y] > 0.6f) {
					mapPixelColor = new Color(0.6f,0.5f,0.3f,1);
				} else if(noiseMap[x,y] > 0.45f) {
					mapPixelColor = new Color(0.15f,0.7f,0.35f,1);
				} else {
					mapPixelColor = new Color(0.1f,0.3f,1f,1);
				}
				image.SetPixel(x,y,mapPixelColor);
			}
		}
		image.Unlock();
		ImageTexture textureImage = new ImageTexture();
		textureImage.Flags = 8;
		textureImage.CreateFromImage(image);
		return textureImage;
	}

}
