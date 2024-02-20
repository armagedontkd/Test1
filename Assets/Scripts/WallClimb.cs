using UnityEngine;
using UnityEngine.Assertions.Must;

public class WallClimb : MonoBehaviour
{
    public float climbSpeed = 5f; // Скорость взбирания
    public float distanceToWall = 1f; // Расстояние до стены, на котором начинается взбирание
    [SerializeField] private CharacterController controller;
    private bool isClimbing = false;
    private Vector3 offSet = new Vector3(0f, 1f, 0f);

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.transform.position);
        Debug.DrawRay(transform.position - offSet, transform.forward * 8, Color.red);
        if (Input.GetKey(KeyCode.W)) // Пример: взбирание происходит при нажатии клавиши W
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position - offSet, transform.forward, out hit, distanceToWall)) // Проверка, достиг ли персонаж стены
            {
                if (hit.collider.tag == "Climbable") // Проверка, является ли объект, с которым столкнулся персонаж, взбираемым
                {
                    isClimbing = true;
                }
            }
            else
            {
                isClimbing = false;
            }
        }

        if (isClimbing)
        {
            controller.enabled = false;
            float moveVertical = Input.GetAxis("Vertical"); // Получение ввода для вертикального движения
            transform.Translate(Vector3.up * moveVertical * climbSpeed * Time.deltaTime); // Перемещение вверх
        }
        if (!isClimbing)
        {
            controller.enabled = true;
            isClimbing = false;
        }
    }
}