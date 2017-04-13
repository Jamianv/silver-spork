using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorToPrefab{
	public Color32 color;
	public GameObject prefab;
}

public class LevelLoader : MonoBehaviour {

	public string levelFileName;

	//public Texture2D LevelMap;

	public ColorToPrefab[] colorToPrefab;

	// Use this for initialization
	void Start () {
		LoadMap ();
	}

	void EmptyMap(){
		//find all of our children and ... eliminate them.
		while(transform.childCount > 0){
			Transform c = transform.GetChild(0);
			c.SetParent (null);
			Destroy(c.gameObject);
		}
	}

	void LoadAllLevelNames(){
		//Read the list of files from StreamingAssests/levels/*.png
	    //do some stuff 
	}

	void LoadMap(){
		EmptyMap ();

		//read the image data from file in StreamingAssets
		string filePath = Application.dataPath + "/StreamingAssets/" + levelFileName;
		byte[] bytes = System.IO.File.ReadAllBytes (filePath);
		Texture2D LevelMap = new Texture2D(2,2);
		LevelMap.LoadImage(bytes);

		//Get the raw pixels from the level imagemap
		Color32[] allPixels = LevelMap.GetPixels32();
		int width = LevelMap.width;
		int height = LevelMap.height;

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				SpawnTileAt (allPixels [(y * width) + x], x, y);
			}
		}
	}

	void SpawnTileAt(Color32 c, int x, int y){

		//If this is a transparent pixel, then it's meant to just be empty
		if(c.a <= 0){
			return;
		}

		//find the right color in our map

		//NOTE: This isn't optimized. You should have a dictionary lookup for max speed
		foreach(ColorToPrefab ctp in colorToPrefab){
			if (ctp.color.r == c.r && ctp.color.g == c.g && ctp.color.b == c.b && ctp.color.a == c.a) {
				//spawn the prefab at the right location
				GameObject go = (GameObject)Instantiate(ctp.prefab, new Vector3(x, y, 0), Quaternion.identity);
				//maybe do more stuff to the gameobject here?
				return;
			}	
		}
		//If we got to this point it means we did not find a matching color in our array

		Debug.LogError ("No color to prefab found for: " + c.ToString ());

	}
}
