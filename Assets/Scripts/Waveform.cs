using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waveform : MonoBehaviour
{
    public GameObject waveformHitPrefab;
    public GameObject t_hit;

    public float moveSpeed;

    public Transform target;// 瞄准的目标
    Vector3 speed = new Vector3(3, 0, 0);// 炮弹本地坐标速度
    Vector3 lastSpeed;// 存储转向前炮弹的本地坐标速度
    int rotateSpeed = 960;// 旋转的速度，单位 度/秒
    Vector3 finalForward;// 目标到自身连线的向量，最终朝向
    float angleOffset;// 自己的forward朝向和mFinalForward之间的夹角
    RaycastHit hit;


    private float lifeTime = 3f;
    private int currentStage = 1;

    void Start()
    {
        CheckCurrentStage();
        //将炮弹的本地坐标速度转换为世界坐标
        speed = transform.TransformDirection(speed);
        if (currentStage == 1)
        {
            target = GameObject.Find("EggHitPoint").GetComponent<Transform>();
        }
        else if (currentStage == 2)
        {
            int currentTail = PlayerPrefs.GetInt("CurrentTail");
            if (currentTail == 0)
            {
                target = GameObject.Find("TailHitPoint1").GetComponent<Transform>();
            }
            else if (currentTail == 1)
            {
                target = GameObject.Find("TailHitPoint2").GetComponent<Transform>();
            }
            else if (currentTail == 2)
            {
                target = GameObject.Find("TailHitPoint3").GetComponent<Transform>();
            }
        }
        else if (currentStage == 3)
        {
            target = GameObject.Find("MedusaHitPoint").GetComponent<Transform>();
        }
        else if (currentStage == 4)
        {
            target = GameObject.Find("HeadHitPoint").GetComponent<Transform>();
        }

    }

    void Update()
    {
        try
        {
            CheckHint();
            UpdateRotation();
            UpdatePosition();

            lifeTime -= Time.deltaTime;
            if (lifeTime < 0)
            {
                Destroy(gameObject);
            }
        }
        catch
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy" || other.tag == "Body" || other.tag == "Tail" || other.tag == "Head")
        {
            t_hit = Instantiate<GameObject>(waveformHitPrefab, transform.position, transform.rotation) as GameObject;

            Destroy(gameObject);
        }
    }

    private void CheckCurrentStage()
    {
        for (int i = 1; i < 5; i++)
        {
            string stage_name = "Stage" + i + "Flag";
            int t_stage;
            t_stage = PlayerPrefs.GetInt(stage_name);
            if (t_stage == 0)
            {
                currentStage = i;
                break;
            }
        }
    }

    //射线检测，如果击中目标点则销毁炮弹
    void CheckHint()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform == target && hit.distance < 1)
            {
                t_hit = Instantiate<GameObject>(waveformHitPrefab, hit.transform.position, hit.transform.rotation) as GameObject;
                Destroy(gameObject);
            }
        }
    }

    //更新位置
    void UpdatePosition()
    {
        transform.position = transform.position + speed * Time.deltaTime;
    }

    //旋转，使其朝向目标点，要改变速度的方向
    void UpdateRotation()
    {
        //先将速度转为本地坐标，旋转之后再变为世界坐标
        lastSpeed = transform.InverseTransformDirection(speed);

        ChangeForward(rotateSpeed * Time.deltaTime);

        speed = transform.TransformDirection(lastSpeed);
    }

    void ChangeForward(float speed)
    {
        //获得目标点到自身的朝向
        finalForward = (target.position - transform.position).normalized;
        if (finalForward != transform.right)
        {
            angleOffset = Vector3.Angle(transform.forward, finalForward);
            if (angleOffset > rotateSpeed)
            {
                angleOffset = rotateSpeed;
            }
            //将自身forward朝向慢慢转向最终朝向
            transform.right = Vector3.Lerp(transform.right, finalForward, speed / angleOffset);
        }
    }


}
