using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadVehicle : MonoBehaviour
{
    public GameObject[] vehiclePrefabs;
    public Transform spawnPoint;
    public GameObject vehicle;

    public Transform GetTransform()
    {
        int selectedVehicle = PlayerPrefs.GetInt("SelectedVehicle");
        GameObject prefab = vehiclePrefabs[selectedVehicle];
        vehicle = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        return vehicle.transform;
    }
}
