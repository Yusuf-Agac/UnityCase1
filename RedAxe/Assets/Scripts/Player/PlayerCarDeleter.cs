using UnityEngine;

namespace Player
{
    public static class PlayerCarDeleter
    {
        public static void DeleteCarAttributes(int carIndex)
        {
            string carKey = "Car" + carIndex.ToString();
            if (PlayerPrefs.HasKey(carKey))
            {
                PlayerPrefs.DeleteKey(carKey);
            }
        }
    }
}