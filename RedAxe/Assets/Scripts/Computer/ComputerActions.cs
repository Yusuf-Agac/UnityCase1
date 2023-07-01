using Car;
using UnityEngine;

namespace Computer
{
    public class ComputerActions : MonoBehaviour
    {
        public Transform contentTransform; 
        public GameObject computerBoxPrefab;
    
        public void AddCarToSellingBox(CarAttributes carAttributes)
        {
            var computerBox = Instantiate(computerBoxPrefab, contentTransform);
            computerBox.GetComponent<SalePostInfo>().SetInfo(carAttributes);
            computerBox.name = carAttributes.carKey.ToString();
        }
    
        public void RemoveCarFromSellingBox(int carKey)
        {
            var computerBox = contentTransform.Find(carKey.ToString()).gameObject;
            Destroy(computerBox);
        }
    }
}
