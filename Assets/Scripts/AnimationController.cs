using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private static readonly int NaderareTrigger = Animator.StringToHash("naderareTrigger");
    private float keyPressTime = -1f; // キーが押された時間
    private bool isCount = false; // カウント中かどうか
    private const float HoldDuration = 3f; // 3秒の定数

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            OnKeyPress(1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            OnKeyRelease(1);
        }

        

        // キーが押された時間から経過時間を計算
        if (keyPressTime >= 0)
        {
            float elapsedTime = Time.time - keyPressTime;
            Debug.Log("ela" + elapsedTime);
            if (elapsedTime >= HoldDuration)
            {
                TransitionToHappy();
                keyPressTime = -1; // キーが離されたのでリセット
                isCount = false;
            }
        }
    }

    private void OnKeyPress(int keyId)
    {
        switch (keyId)
        {
            case 1:
                    animator.SetBool(NaderareTrigger, true);
                    if (!isCount)
                    {
                        keyPressTime = Time.time; // キーが押された時間を記録
                        Debug.Log(keyPressTime);
                        isCount = true;
                    }
                    break;
        }
    }

    private void OnKeyRelease(int keyId)
    {
        switch (keyId)
        {
            case 1:
                animator.SetBool(NaderareTrigger, false);
                if (keyPressTime >= 0)
                {
                    TransitionToHappy();
                }
                keyPressTime = -1; // キーが離されたのでリセット
                isCount = false;
                break;
        }
    }

    private void TransitionToHappy()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("naderare"))
        {
            Debug.Log("Transition to happy");
            animator.Play("happy");
        }
    }
}