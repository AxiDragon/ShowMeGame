using UnityEngine;
using System.Collections;
using System.Linq; // used for Sum of array

namespace Gunbloem
{
    public class TerrainHeightTexturer : MonoBehaviour
    {
        public float[] startingHeights;
        float maxHeight = 0f;

        void Start()
        {
            Terrain terrain = GetComponent<Terrain>();

            TerrainData terrainData = terrain.terrainData;

            // Splatmap data is stored internally as a 3d array of floats, so declare a new empty array ready for your custom splatmap data:
            float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

            for (int y = 0; y < terrainData.alphamapHeight; y++)
            {
                for (int x = 0; x < terrainData.alphamapWidth; x++)
                {
                    // Normalise x/y coordinates to range 0-1 
                    float y_01 = y / (float)terrainData.alphamapHeight;
                    float x_01 = x / (float)terrainData.alphamapWidth;

                    // Sample the height at this location (note GetHeight expects int coordinates corresponding to locations in the heightmap array)
                    float height = terrainData.GetHeight(Mathf.RoundToInt(y_01 * terrainData.heightmapResolution), Mathf.RoundToInt(x_01 * terrainData.heightmapResolution));
                    maxHeight = Mathf.Max(maxHeight, height);

                    int mapId = 0;
                    for (int i = 0; i < startingHeights.Length; i++)
                    {
                        if (height > startingHeights[i])
                            mapId = i;
                    }

                    // Loop through each terrain texture
                    for (int i = 0; i < terrainData.alphamapLayers; i++)
                    {
                        float influence = (i == mapId) ? 1f : 0f;
                        // Assign this point to the splatmap array
                        splatmapData[x, y, i] = influence;
                    }
                }
            }

            print(maxHeight);
            // Finally assign the new splatmap to the terrainData:
            terrainData.SetAlphamaps(0, 0, splatmapData);
        }
    }
}