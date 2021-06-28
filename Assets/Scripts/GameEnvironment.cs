 using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public sealed class GameEnvironment
{
    private List<GameObject> _checkpoints = new List<GameObject>();
    public List<GameObject> Checkpoints { get { return _checkpoints; } }
    private static GameEnvironment _singleton;
    public static GameEnvironment Singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = new GameEnvironment();
                _singleton.Checkpoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));
                _singleton._checkpoints = _singleton._checkpoints.OrderBy(checkpoint => checkpoint.name).ToList();
            }

            return _singleton;
        }
    }
}
