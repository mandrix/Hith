using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{

    [Header("Collector Variables")]
    [SerializeField]
    private GameObject[] items;

    [SerializeField]
    private GameObject Collider;

    private List<GameObject> colliders = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        int maxLength = items.Length;
        while (index != maxLength)
        {
            GameObject item = items[index++];
            LocateColliderObject(item);
        }
    } 

    private void LocateColliderObject(GameObject item) {
        Vector3 position = item.transform.position;
        GameObject newCollider = Instantiate(Collider, position, new Quaternion(), transform);
        newCollider.GetComponent<SpawnItem>().SetParent(item);
        colliders.Add(newCollider);
    }

}
