/*
 *  НИЧЕГО НЕ РЕДАКТИРОВАТЬ, ДОБАВЛЕНИЕ НОВЫХ ЛОКАЦИЙ ПРОИСХОДИТ В Levels.cs ПО ПУТИ Resources/enums
 *  ЗДЕСЬ ПРОПИСАНА ТОЛЬКО ГЕНЕРАЦИЯ САМИХ ЧАНКОВ И ИХ ЗАПОЛНЕНИЕ ТАЙЛАМИ, ПОДРОБНЕЕ В Levels.cs
 */

using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TilemapGeneration : MonoBehaviour
{
    [SerializeField] private Tilemap terrain;
    [HideInInspector] public Levels currentLevelTitle;
    
    
    [SerializeField] private int _chunkWidth = 30;
    [SerializeField] private int _chunkHeight = 30;
    
    private GameObject _targetForGenChunksAround;
    private Tile[] _tiles;

    private void Start()
    {
        _tiles = Resources.LoadAll("tiles/" + currentLevelTitle, typeof(Tile)).Cast<Tile>().ToArray();
        
        _targetForGenChunksAround = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        ChunksAroundTargetGeneration();
    }

    void ChunkFill(Vector3Int chunkPos)
    {
        if (!terrain.HasTile(chunkPos)) // Защита от наложения чанка поверх другого
        {
            for (int x = chunkPos.x - _chunkWidth / 2; x < chunkPos.x + _chunkWidth / 2; x++)
            {
                for (int y = chunkPos.y - _chunkHeight / 2; y < chunkPos.y + _chunkHeight / 2; y++)
                {
                    terrain.SetTile(new Vector3Int(x, y, 0), _tiles[Random.Range(0, _tiles.Length-1)]);
                }
            }
        }
    }
    
    void ChunksAroundTargetGeneration()
    {
        Vector3Int targetPos = Vector3Int.CeilToInt(_targetForGenChunksAround.transform.position);
        
        // Здесь идёт перебор направлений вокруг цели (вспоминаем тригонометрию)
        for (int xDir = -1; xDir <= 1; xDir++)
        {
            for (int yDir = -1; yDir <= 1; yDir++)
            {
                // Вычисляем примерное положение следующего чанка относительно цели
                Vector3Int nextChunkPosition = targetPos + new Vector3Int(
                    _chunkWidth * xDir,
                    _chunkHeight * yDir,
                    0);
                
                // Отсекается погрешность для получения координат центра следующего чанка
                nextChunkPosition = new Vector3Int(
                    Mathf.CeilToInt(nextChunkPosition.x / _chunkWidth) * _chunkWidth,
                    Mathf.CeilToInt(nextChunkPosition.y / _chunkHeight) * _chunkHeight,
                    0);
                
                ChunkFill(nextChunkPosition);
            }
        }
    }
}
