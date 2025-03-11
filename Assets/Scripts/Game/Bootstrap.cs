using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public static Bootstrap Instance;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private MenuManager _menuManager;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] public ShopManager ShopManager;
    [SerializeField] public ServerTime ServerTime;
    [SerializeField] public VibrationManager VibrationManager;

    private void OnValidate()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _menuManager = FindObjectOfType<MenuManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        ShopManager = FindObjectOfType<ShopManager>();
        ServerTime = FindObjectOfType<ServerTime>();
        VibrationManager = FindObjectOfType<VibrationManager>();
    }

    private void Awake() => Instance = this;

    private void Start()
    {
        _levelManager.Initialize();
        _gameManager.Initialize();
        _menuManager.Initialize();
        ShopManager.Initialize();
        ServerTime.Initialize();
    }
}
