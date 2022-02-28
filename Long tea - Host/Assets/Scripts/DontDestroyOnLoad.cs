// Connected to the Cube and includes a DontDestroyOnLoad()
// LoadScene() is called by the first  script and switches to the second.

using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static bool created = false;

    void Awake()
    {
       
            DontDestroyOnLoad(this.gameObject);

    }

   
}
