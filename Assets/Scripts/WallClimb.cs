using UnityEngine;
using UnityEngine.Assertions.Must;

public class WallClimb : MonoBehaviour
{
    public float climbSpeed = 5f; // �������� ���������
    public float distanceToWall = 1f; // ���������� �� �����, �� ������� ���������� ���������
    [SerializeField] private CharacterController controller;
    private bool isClimbing = false;
    private Vector3 offSet = new Vector3(0f, 1f, 0f);

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.transform.position);
        Debug.DrawRay(transform.position - offSet, transform.forward * 8, Color.red);
        if (Input.GetKey(KeyCode.W)) // ������: ��������� ���������� ��� ������� ������� W
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position - offSet, transform.forward, out hit, distanceToWall)) // ��������, ������ �� �������� �����
            {
                if (hit.collider.tag == "Climbable") // ��������, �������� �� ������, � ������� ���������� ��������, ����������
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
            float moveVertical = Input.GetAxis("Vertical"); // ��������� ����� ��� ������������� ��������
            transform.Translate(Vector3.up * moveVertical * climbSpeed * Time.deltaTime); // ����������� �����
        }
        if (!isClimbing)
        {
            controller.enabled = true;
            isClimbing = false;
        }
    }
}