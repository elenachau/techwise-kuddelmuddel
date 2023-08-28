using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class ExplosionManager : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject explosionPrefab;
    public float timeInterval = 3f; // time in seconds to trigger a random explosion

    private WeedLocationManager wlm;
    private BoundsInt bounds;

    private void Start()
    {
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
        bounds = tilemap.cellBounds;
        InvokeRepeating("TriggerRandomExplosion", timeInterval, timeInterval);
    }

    void TriggerRandomExplosion()
    {
        Vector3Int randomCell = new Vector3Int(
            Random.Range(bounds.xMin, bounds.xMax),
            Random.Range(bounds.yMin, bounds.yMax),
            0
        );

        Vector3 explosionPosition = tilemap.GetCellCenterWorld(randomCell);
        GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
        Destroy(explosion, 2f); // destroy the explosion after 2 seconds, adjust as necessary

        // Check for weed at this location and destroy
        if (wlm.weedLocations.ContainsKey(randomCell))
        {
            GameObject weed = wlm.weedLocations[randomCell];
            if (weed.tag == "StandardWeed")
            {
                Destroy(weed);
                wlm.weedLocations.Remove(randomCell);
            }
        }
    }
}
