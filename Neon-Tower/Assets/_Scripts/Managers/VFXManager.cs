using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField]
    private Transform _vfxParent;
    [SerializeField]
    private GameObject _explosionPrefab;

    public static VFXManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void BuildingExplosion(Vector3 position)
    {
        Quaternion rotation = Quaternion.identity;
        Instantiate(_explosionPrefab,position, rotation, _vfxParent);
    }
}
