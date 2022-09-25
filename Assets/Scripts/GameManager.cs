using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform _player;
    [SerializeField]
    AIController _crowPrefab;
   public List<AIController> Crows = new List<AIController>();
    [SerializeField]
    int _numberOfCrowsToSpawn = 3;

    [SerializeField]
    Transform[] _spawnTransforms = new Transform[2];

    public static GameManager instance;
    public int kills = 0;
    int _crowSpawnIndex = 0;
    public void Load(string savedData)
    {
        JsonUtility.FromJsonOverwrite(savedData, this);
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        // If the number of crows is less than the number of crows to spawn...
        if(Crows.Count < _numberOfCrowsToSpawn)
        {
            // Spawn the crow at the random spawnpoint and add it to the list of crows.
            AIController __newCrow = Instantiate(_crowPrefab, _spawnTransforms[_crowSpawnIndex].position, Quaternion.identity);
            __newCrow.PlayerTransform = _player;
            Crows.Add(__newCrow);

            if ((_crowSpawnIndex + 1) < _spawnTransforms.Length)
                _crowSpawnIndex++;
            else
                _crowSpawnIndex = 0;
        }
    }
}
