using UnityEngine;

public class ItemSwiper : MonoBehaviour
{
    public void SwipeItem(SwipeableItem item, Ray ray)
    {

        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out float hitDistance))
        {
            Vector3 point = ray.GetPoint(hitDistance);

            item.transform.position = point;
        }
    }
}
