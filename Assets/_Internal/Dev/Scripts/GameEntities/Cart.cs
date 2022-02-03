using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    public Player holder;

    //public PlayerMove _playerMove;

    public Transform myJoint;

    public Transform followingJoint = null;

    public bool isFollowing;

    public float speedCoefficient;

    [SerializeField]
    List<GameObject> _carts;

    [SerializeField]
    string _cartTag = "Cart";

    //[SerializeField]
    public float _followSpeed = 1f;

    [SerializeField]
    private float minDelta = 0.68f;
    [SerializeField]
    private float maxDelta = 0.75f;

    public bool isDrowning;

    [SerializeField]
    float _iceBallAddCooldown = 1.5f;

    [SerializeField]
    bool _isOnCooldown;


    [SerializeField]
    float _fullScaleTime = 0.2f;

    Coroutine _bumpingRoutine;

    private void Start()
    {

        if (followingJoint == null) isFollowing = false;

        isDrowning = false;

        //SetCone(ConeManager.Instance.GetCurrentCone());

        _isOnCooldown = false;
    }

    //private void Update()
    //{
    //    if (isFollowing)
    //    {
    //        transform.position = new Vector3(transform.position.x, followingJoint.position.y, followingJoint.position.z);

    //        transform.position = Vector3.MoveTowards(transform.position, followingJoint.position, speedCoefficient * Time.deltaTime * _followSpeed);
    //    }
    //}

    private void FixedUpdate()
    {
        if (isFollowing)
        {
            //transform.position = new Vector3(
            //    Mathf.MoveTowards(transform.position.x, followingJoint.position.x, speedCoefficient * Time.fixedDeltaTime * _followSpeed),
            //    followingJoint.position.y, 
            //    followingJoint.position.z);

            transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, followingJoint.position.x, speedCoefficient),
                followingJoint.position.y,
                followingJoint.position.z);
        }

        if (!isFollowing && holder != null && transform.position.z + 1f < holder.transform.position.z)
        {
            //ConeManager.Instance.RemovePlayersIceCreams(gameObject);
            DestroySelf();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFollowing && other.tag == _cartTag)
        {
            Catch(other.GetComponent<Cart>());
        }
    }

    public void Catch(Cart cart)
    {
        holder.Catch(cart);
    }

    public void RemoveSelf()
    {
        holder.BreakChain(gameObject);
    }


    public void DestroySelf()
    {
        holder.ClearMissing(this);

        Destroy(gameObject);
    }

    public void SetCone(int index)
    {
        for (int i = 0; i < _carts.Count; i++)
        {
            if (i != index) _carts[i].SetActive(false);
            else _carts[i].SetActive(true);
        }
    }

    IEnumerator Cooldown()
    {
        _isOnCooldown = true;

        yield return new WaitForSeconds(_iceBallAddCooldown);

        _isOnCooldown = false;
    }

}
