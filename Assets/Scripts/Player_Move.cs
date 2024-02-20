using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Move : MonoBehaviour
{
    [SerializeField] Slider staminaSlider;
    [SerializeField] float staminaValue;
    [SerializeField] float minValueStamina;
    [SerializeField] float maxValueStamina;
    [SerializeField] float staminaReturn;
    [SerializeField] float staminaSpent;

    //[SerializeField] float staminaReturn2;
    [Range(0, 10)][SerializeField] private float smoothSpeed;
    private TMP_Text textStamina;

    private bool isSquat;
    public bool AllStaminaSpentResently;
    public float speed_Move;
    public float speed_Run;
    public float speed_Current;
    public float MinStaminaForRun;
    public float jump;
    public float gravity = 1;
    //public float speed_Run2;
    float x_Move;
    float z_Move;
    float speed_Run2;
    float staminaReturn2;
    CharacterController player;
    Vector3 move_Direction;

 
    private bool in_air = false;

    void Start()
    {

        player = GetComponent<CharacterController>();
        textStamina = staminaSlider.transform.GetChild(3).GetComponent<TMP_Text>();
        AllStaminaSpentResently = false;
        isSquat = false;
        speed_Run2 = speed_Run;
        staminaReturn2 = staminaReturn;

    }

    void Update()
    {
        Move();

        Stamina();
    }


    void Move()
    {
        x_Move = Input.GetAxis("Horizontal");
        z_Move = Input.GetAxis("Vertical");
        if (player.isGrounded)
        {

            if (in_air)
            {
                
                in_air = false;
            }
            move_Direction = new Vector3(x_Move, 0f, z_Move);
            if (move_Direction.magnitude > 0.8f)
            {
                
            }
            else
            {
                
            }
            move_Direction = transform.TransformDirection(move_Direction);
            if (Input.GetKey(KeyCode.Space))
            {
                move_Direction.y += jump;
                
            }
            else if (Input.GetKey(KeyCode.LeftControl) && (isSquat == false) && (player.height > 1.4F))
            {

                player.height -= 0.1f;

            }
            else if ((player.height < 1.8f) && (player.height > 1.4f) && (isSquat == false))
            {
                player.height -= 0.1f;

            }
            else if ((player.height <= 1.4f) && (isSquat == false))
            {
                isSquat = true;
                speed_Run = speed_Move;

                staminaReturn = 0;


            }
            else if ((isSquat = true) && (Input.GetKey(KeyCode.LeftControl)))
            {
                player.height = 0.7f;

            }
            else if ((player.height < 1.8f) && (isSquat == true))
            {
                player.height += 0.1f;


            }
            else
            {
                isSquat = false;
                speed_Run = speed_Run2;
                staminaReturn = staminaReturn2;

            }

        }
        else
        {

            
            in_air = true;
        }
        move_Direction.y -= gravity * Time.deltaTime;

        if ((Input.GetKey(KeyCode.LeftShift)) && (staminaValue > staminaReturn) && (AllStaminaSpentResently == false))
        {
            speed_Current = Mathf.Lerp(speed_Current, speed_Run, Time.deltaTime * smoothSpeed);
            staminaValue -= staminaSpent * Time.deltaTime * 10;

        }
        else if ((Input.GetKey(KeyCode.LeftShift)) && (staminaValue <= staminaReturn) && (AllStaminaSpentResently == false))
        {
            AllStaminaSpentResently = true;
            UnityEngine.Debug.Log(AllStaminaSpentResently);
            staminaValue += staminaReturn * Time.deltaTime * 1f;
            speed_Current = Mathf.Lerp(speed_Current, speed_Move, Time.deltaTime * smoothSpeed);

        }
        else if ((Input.GetKey(KeyCode.LeftShift)) && (AllStaminaSpentResently == true))
        {
            speed_Current = Mathf.Lerp(speed_Current, speed_Move, Time.deltaTime * smoothSpeed);
            staminaValue += staminaReturn * Time.deltaTime * 2;
            if (staminaValue >= MinStaminaForRun)
            {
                AllStaminaSpentResently = false;
            }
        }
        else
        {
            speed_Current = Mathf.Lerp(speed_Current, speed_Move, Time.deltaTime * smoothSpeed);
            staminaValue += staminaReturn * Time.deltaTime * 2;
            if (staminaValue > 100)
            {
                staminaValue = 100;
            }
        }
        player.Move(move_Direction * speed_Current * Time.deltaTime);

    }
    private void Stamina()
    {
        if (maxValueStamina >= 100) maxValueStamina = 100f;
        if (maxValueStamina <= 100) maxValueStamina = 0f;
        textStamina.text = staminaSlider.value.ToString();
        staminaSlider.value = staminaValue;
    }
}