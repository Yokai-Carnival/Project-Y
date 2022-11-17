using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public System.Action EventEndMiniGame { get; set; }

    private void Awake()
    {
        Instance = this;
    }
}
