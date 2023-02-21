using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{

    private void Awake()
    {
        int scenePersistCount = FindObjectsOfType<ScenePersist>().Length;
        if (scenePersistCount > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public void EnsureThatNewScenePersistIsCreated()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
