using UnityEngine;

using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Weighted Random Brush", menuName = "Brushes/Weighted Random Brush")]
[CustomGridBrush(true, false, false, "Weighted Random Brush")]
public class WeightedRandomBrush : UnityEditor.Tilemaps.GridBrush
{
    [System.Serializable]
    public struct WeightedTile
    {
        public TileBase tile;
        public int weight;
    }

    public List<WeightedTile> tiles = new List<WeightedTile>();

    public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
    {
        if (tiles.Count == 0)
            return;

        int totalWeight = 0;
        foreach (var wt in tiles)
            totalWeight += wt.weight;

        int randomValue = Random.Range(0, totalWeight);
        int accumulated = 0;

        foreach (var wt in tiles)
        {
            accumulated += wt.weight;
            if (randomValue < accumulated)
            {
                var tilemap = brushTarget.GetComponent<Tilemap>();
                tilemap.SetTile(position, wt.tile);
                return;
            }
        }
    }
}
