using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUI : MonoBehaviour
{
    public GameObject target_obj;
    private RectTransform UITrans;
    private Camera mainCamera;
    private Animator anim;

    private Vector3 distance = new Vector3(-200, 150, 0);

    private int hashPopUp;

    private void Awake()
    {
        TryGetComponent(out UITrans);
        TryGetComponent(out anim);
        mainCamera = Camera.main;
        target_obj = GameObject.FindGameObjectWithTag("Player");

        hashPopUp = Animator.StringToHash("popUp");
        PopDown();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(PopUp_co));
    }

    private void LateUpdate()
    {
        if(target_obj == null)
        {
            return;
        }
        if (!target_obj.activeSelf)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 ScreenPosition = mainCamera.WorldToScreenPoint(target_obj.transform.position);

        UITrans.position = ScreenPosition + distance;
    }

    private IEnumerator PopUp_co()
    {
        anim.SetTrigger(hashPopUp);
        yield return new WaitForSeconds(2f);
        PopDown();
    }
    private void PopDown()
    {
        anim.Rebind();
    }
}
