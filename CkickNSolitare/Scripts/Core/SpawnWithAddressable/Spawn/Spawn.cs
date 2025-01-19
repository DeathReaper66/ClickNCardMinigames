using UnityEngine;
using UnityEngine.AddressableAssets;

public class Spawn : MonoBehaviour
{
    [SerializeField] private string _objectName;

    private void Awake()
    {
        Addressables.InstantiateAsync(_objectName);
    }
}
