using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

namespace main_hero
{
    public class PlayerShootingController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
        [SerializeField] private CinemachineVirtualCamera _fpvAimVirtualCamera;
        [SerializeField] private ShotgunBehavior shotgunBehavior;
        [SerializeField] private Transform aimBall;
        [SerializeField] private LayerMask aimColliderLayerMask;
        [SerializeField] private float normalSensitivity;
        [SerializeField] private float aimSensitivity;

        private Animator animator;
        private ThirdPersonController thirdPersonController;
        private StarterAssetsInputs starterAssetsInputs;
        private Camera mainCamera;

        private Vector2 screenCenterPoint;
        private Vector3 lookPointPosition;

        private float aimingDistance = 1000f;

        private bool _aimingEnabled = true;
        [SerializeField] private bool _fpv = true;

        private void OnEnable()
        {
            EventBus.Instance.playerDied += DisableAiming;
        }

        private void OnDisable()
        {
            EventBus.Instance.playerDied -= DisableAiming;
        }
        private void DisableAiming()
        {
            _aimingEnabled = false;
        }

        private void Awake()
        {

            animator = GetComponent<Animator>();
            thirdPersonController = GetComponent<ThirdPersonController>();
            starterAssetsInputs = GetComponent<StarterAssetsInputs>();

            screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            mainCamera = Camera.main;
        }
        private void CalculateLookPoint()
        {
            Ray ray1 = mainCamera.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray1, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                Debug.DrawRay(ray1.origin, ray1.direction * 100f, Color.green);
                //aimBall.position = raycastHit.point;
                lookPointPosition = raycastHit.point;
            }
            else
            {
                lookPointPosition = mainCamera.transform.position + mainCamera.transform.forward * aimingDistance;
            }
        }
        private void Update()
        {
            if (_aimingEnabled)
            {
                CalculateLookPoint();

                if (starterAssetsInputs.aim)
                {
                    shotgunBehavior.SetAiming(true);
                    shotgunBehavior.SetDirectionPoint(lookPointPosition);

                    if (!_fpv) 
                    { 
                        aimVirtualCamera.gameObject.SetActive(true);
                    }
                    else
                    {
                        _fpvAimVirtualCamera.gameObject.SetActive(true);
                    }
                    thirdPersonController.SetSensitivityMultiplier(aimSensitivity);
                    //thirdPersonController.SetRotateOnMove(false);
                    thirdPersonController.SetAimingRotation(true);

                    animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

                    Vector3 bodyDirectionTarget = lookPointPosition;
                    bodyDirectionTarget.y = transform.position.y;
                    Vector3 bodyDirection = (bodyDirectionTarget - transform.position).normalized;

                    //transform.forward = Vector3.Lerp(transform.forward, bodyDirection, Time.deltaTime * 20f);

                    if (starterAssetsInputs.shoot)
                    {
                        shotgunBehavior.TryShoot();
                        //starterAssetsInputs.shoot = false;
                    }
                }
                else
                {
                    /*if (starterAssetsInputs.shoot)
                    {
                        starterAssetsInputs.shoot = false;
                    }*/
                    shotgunBehavior.SetAiming(false);

                    if (!_fpv)
                    {
                        aimVirtualCamera.gameObject.SetActive(false);
                    }
                    else
                    {
                        _fpvAimVirtualCamera.gameObject.SetActive(false);
                    }
                    thirdPersonController.SetSensitivityMultiplier(normalSensitivity);
                    //thirdPersonController.SetRotateOnMove(true);
                    thirdPersonController.SetAimingRotation(false);
                    animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
                }
            }
        }
    }
}

