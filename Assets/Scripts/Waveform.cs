using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waveform : MonoBehaviour
{
    public float moveSpeed;

    public Transform target;// 瞄准的目标
    Vector3 speed = new Vector3(3, 0, 0);// 炮弹本地坐标速度
    Vector3 lastSpeed;// 存储转向前炮弹的本地坐标速度
    int rotateSpeed = 960;// 旋转的速度，单位 度/秒
    Vector3 finalForward;// 目标到自身连线的向量，最终朝向
    float angleOffset;// 自己的forward朝向和mFinalForward之间的夹角
    RaycastHit hit;

    private float lifeTime = 3f;

    void Start()
    {
        //将炮弹的本地坐标速度转换为世界坐标
        speed = transform.TransformDirection(speed);

        target = GameObject.Find("HitPoint").GetComponent<Transform>();
    }

    void Update()
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Medusa")
            Destroy(gameObject);
    }

    //射线检测，如果击中目标点则销毁炮弹
    void CheckHint()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform == target && hit.distance < 1)
            {
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
