using System.Collections.Generic;
using Car;
using UnityEngine;

namespace Player
{
    public class PlayerCarGenerator : MonoBehaviour
    {
        public List<GameObject> carPrefabs;
        public List<string> carModelNames;
    
        void Start()
        {
            int carCount = PlayerPrefs.GetInt("CarCount", 0);
            int createdCarCount = 0;
            for (int i = 0; i <= carCount; i++)
            {
                if (!PlayerCarLoader.HasCar(i)) { continue; }
                CarAttributesData carAttributes = PlayerCarLoader.LoadCarAttributes(i);
                int carIndex = carModelNames.IndexOf(carAttributes.carModelName);
                if (carIndex == -1) { continue; }
                var car = Instantiate(carPrefabs[carIndex], transform.position, transform.rotation);
                car.transform.localPosition += Vector3.right * createdCarCount * 4.5f;
                var carAttributesComponent = car.GetComponent<CarAttributes>();
                carAttributesComponent.FromData(carAttributes);
                carAttributesComponent.StartModification();
                carAttributesComponent.AddCarToSellingBox();
                createdCarCount++;
            }
        }
    }
}
