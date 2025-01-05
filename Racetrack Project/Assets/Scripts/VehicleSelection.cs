using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VehicleSelection : MonoBehaviour
{
    public GameObject[] vehicles;
    public int selectedVehicle = 0;

    public void NextVehicle()
    {
        vehicles[selectedVehicle].SetActive(false);
        selectedVehicle = (selectedVehicle + 1) % vehicles.Length;
        vehicles[selectedVehicle].SetActive(true);
    }

    public void PreviousVehicle()
    {
        vehicles[selectedVehicle].SetActive(false);
        selectedVehicle = (selectedVehicle - 1 + vehicles.Length) % vehicles.Length;
        vehicles[selectedVehicle].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("SelectedVehicle", selectedVehicle);
        SceneManager.LoadScene("Test");
    }
}
