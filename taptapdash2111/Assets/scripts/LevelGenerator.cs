using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private List<GameObject> _chunks;
    [SerializeField] private float _triggerDistance;
    [SerializeField] private Transform _spawnPoint;

    public List<GameObject> _spawnedChunks;

    void Start()
    {
        _spawnPoint = SpawnChunk(_spawnPoint);
    }

    void Update()
    {
        if (Vector3.Distance(_spawnPoint.position, _player.position) < _triggerDistance)
        {
            _spawnPoint = SpawnChunk(_spawnPoint);
        }

        if(_spawnedChunks.Count > 4)
        {
            Destroy(_spawnedChunks[0]);
            _spawnedChunks.RemoveAt(0);
        }
    }

    private Transform SpawnChunk(Transform spawnPoint)
    {
        //создать рандомный элемент из списка чанков
        GameObject newChunk = Instantiate(_chunks[Random.Range(0, _chunks.Count - 1)]);

        GameObject startPoint = newChunk.transform.GetChild(0).gameObject;
        GameObject endPoint = newChunk.transform.GetChild(1).gameObject;

        newChunk.transform.position = spawnPoint.position - startPoint.transform.localPosition;

        _spawnedChunks.Add(newChunk);

        spawnPoint = endPoint.transform;
        return spawnPoint;
    }
}
