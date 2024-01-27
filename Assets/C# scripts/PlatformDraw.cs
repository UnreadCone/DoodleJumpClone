using UnityEngine;
public class PlatformDraw : MonoBehaviour
{
    public GameObject platformPrefab; // Префаб платформы    
    private Vector2 mouseDownPos; // Начальная позиция клика мыши
    private GameObject currentPlatform; // Текущая нарисованная платформа    
    private Vector3 platformScaleLimits = new Vector3(2, 1, 2); // Ограничение размеров платформы
    private Plane plane;

    void Start()
    {
        plane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 mousePos = ray.GetPoint(distance);
                mouseDownPos = mousePos; // Запоминаем начальную позицию клика мыши
                currentPlatform = Instantiate(platformPrefab, mouseDownPos, Quaternion.identity); // Создаем платформу в начальной позиции                
            }
            else if (Input.GetMouseButton(0))
            {
                Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
                float distanceFromPlane;
                if (plane.Raycast(rayCast, out distanceFromPlane))
                {
                    Vector3 mousePos = rayCast.GetPoint(distanceFromPlane);
                    Vector3 mouseDelta = mousePos - currentPlatform.transform.position;
                    currentPlatform.transform.localScale = Vector3.Min(platformScaleLimits, new Vector3(Mathf.Abs(mouseDelta.x), 0.1f, Mathf.Abs(mouseDelta.z)));
                    // Ограничиваем размеры платформы 
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                // Кнопка мыши отпущена — можно завершить создание платформы
                currentPlatform = null;
            }
        }
    }
}