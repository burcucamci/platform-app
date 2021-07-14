using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    private float mySpeedX;
    [SerializeField] float speed; // private ama unity de degiştirilebilen 
    private Rigidbody2D myBody;
    private Vector3 defaultLocalScale;
    public bool onground; // oyuncu zemin uzerinde mi degil mi 
    private bool candoublejump;
    [SerializeField] GameObject arrow; // ok objesi
    [SerializeField] bool attacked;
    [SerializeField] float currentAttackTimer;
    [SerializeField] float defaultAttackTimer;

    void Start()    // Start is called before the first frame update
    {
        attacked = false;
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        mySpeedX = Input.GetAxis("Horizontal");
        //GetComponent<Rigidbody2D>().velocity = new Vector2(mySpeedX, GetComponent<Rigidbody2D>().velocity.y);
        // sürekli getcompenents yapmak yerine bir kez cagırıp altta ışlemleri yapmak için mybody i kullanırız 

        myBody.velocity = new Vector2(mySpeedX * speed, myBody.velocity.y);

        #region player in sağ ve sol hareket yönüne doğru yüzünün dönmesi 
        if (mySpeedX > 0) // saga dogru hareket etmesi
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        else if (mySpeedX < 0) // sola doğru hareket etmesi 
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        #endregion
        #region playerin zıplamasını kontrol edilmesi
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onground == true) // oyuncunu zıplarken sadece zemindeyken zıplaması üst üste havada kalmaması ıcın 
            {
                myBody.velocity = new Vector2(myBody.velocity.x, 5f);
                candoublejump = true;
            }
            else
            {
                if (candoublejump == true)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, 5f);
                    candoublejump = false;
                }
            }
        }
        #endregion

        #region playerin ot atmasının kontrolü
        if (Input.GetMouseButtonDown(0))
        {
            if (attacked == false)
            {
                attacked = true;
                Fire();
            }
        }
        #endregion

        if(attacked == true)
        {
            currentAttackTimer -= Time.deltaTime;

        }
        else
        {
            currentAttackTimer = defaultAttackTimer;
        }

        if(currentAttackTimer<0)
        {
            attacked = false;
        }
    } void Fire()
        {
           GameObject okumuz= Instantiate(arrow,transform.position,Quaternion.identity);
            
            if(transform.localScale.x > 0)
            {
                okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);
            }
            else
            {
                Vector3 okumuzScale = okumuz.transform.localScale;
                okumuz.transform.localScale = new Vector3(-okumuzScale.x, okumuzScale.y, okumuzScale.z);
                okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 0f);
            }
        }

        
    

}

