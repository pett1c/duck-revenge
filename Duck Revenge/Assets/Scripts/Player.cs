using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject aimJoystickGO;
    [SerializeField] private GameObject moveJoystickGO;

    [SerializeField] private GameObject playerCharacter;

    [Header("Parameters")]
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float moveSpeed = 2.0f;

    private Transform playerTransform;
    private const float maxHealth = 100.0f;

    [Header("Inventory")]
    private Inventory inventory;
    private const int numberOfSlots = 4;

    [Header("Joysticks")]
    [SerializeField] private Vector2 aimDirection;
    [SerializeField] private Vector2 moveDirection;

    private Joystick aimJoystick;
    private Joystick moveJoystick;

    [Header("Animations")]
    private Animator playerCharacterAnimator;
    private SpriteRenderer playerCharacterSpriteRenderer;

    private void Start()
    {
        playerTransform = transform;

        inventory = new Inventory(numberOfSlots);

        moveJoystick = moveJoystickGO.GetComponent<Joystick>();
        aimJoystick = aimJoystickGO.GetComponent<Joystick>();

        playerCharacterAnimator = playerCharacter.GetComponent<Animator>();
        playerCharacterSpriteRenderer = playerCharacter.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        aimDirection = aimJoystick.localStickPosition;

        Move();
    }
    
    private void Move() 
    {
        moveDirection = moveJoystick.localStickPosition;

        if (moveDirection.magnitude == 0) { playerCharacterAnimator.SetBool("isMovement", false); return; }

        playerCharacterSpriteRenderer.flipX = moveDirection.x < 0;

        playerCharacterAnimator.SetBool("isMovement", true);
        playerCharacterAnimator.SetFloat("Horizontal", moveDirection.x);
        playerCharacterAnimator.SetFloat("Vertical",   moveDirection.y);

        playerTransform.Translate(moveDirection * Time.deltaTime * moveSpeed);
    }
}
