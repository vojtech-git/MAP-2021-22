using UnityEngine;

public class phoneWallpaper : MonoBehaviour
{
    public GameObject[] wallpapers;
    public GameObject newWallpaper;

    public void ChangeWallpaper()
    {
        foreach (GameObject wallpaper in wallpapers)
        {
            wallpaper.SetActive(false);
        }
        newWallpaper.SetActive(true);
    }
}
