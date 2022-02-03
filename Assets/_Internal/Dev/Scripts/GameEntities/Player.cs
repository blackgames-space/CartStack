using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerMove _playerMove;

    [SerializeField]
    Transform _firstJoint;

    [SerializeField]
    List<Cart> _carts;

    [SerializeField]
    string _cartTag = "Cart";

    [SerializeField]
    private float originMaximumDelta = 7.1f;

    [SerializeField]
    private float originMaximumDeltaMultiplier = 2f;

    [SerializeField]
    private float newMaximumDelta;

    [SerializeField]
    private float currentMaximumDelta;

    [SerializeField]
    float _pushOutDuration = 0.8f;

    [SerializeField]
    AnimationCurve _pushOutCurve;

    [SerializeField]
    AnimationCurve _followCoefCurve;

    [SerializeField]
    float _bumpingDelay = 0.2f;

    private void Start()
    {
        currentMaximumDelta = originMaximumDelta;

        //_playerController = GetComponent<Hand>().followingJoint.GetComponent<PlayerController>();
        //_playerMove = GetComponent<Hand>().followingJoint.GetComponent<PlayerMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_carts.Count == 0 && other.tag == _cartTag)
        {
            Catch(other.GetComponent<Cart>());
        }
    }

    public void Catch(Cart cart)
    {
        if (!_carts.Contains(cart))
        {
            ClearMissing(cart);
            //Debug.Log(cart.name + " is being catching ");

            Transform joint = null;

            if (_carts.Count == 0)
            {
                joint = _firstJoint;
                cart.followingJoint = joint;
                cart.transform.parent = joint;
            }
            else
            {
                //Debug.Log(cart.name + " is jointing to " + _carts[_carts.Count - 1].name);

                if (_carts[_carts.Count - 1] != null) joint = _carts[_carts.Count - 1].GetComponent<Cart>().myJoint;
                else joint = _carts[_carts.Count - 2].GetComponent<Cart>().myJoint;

                cart.followingJoint = joint;
                cart.transform.position = joint.position;
            }

            cart.isFollowing = true;

            cart.holder = this;

            if (originMaximumDelta < joint.position.z - _firstJoint.position.z)
            {
                currentMaximumDelta = joint.position.z - _firstJoint.position.z;
            }
            else
            {
                currentMaximumDelta = originMaximumDelta;
            }

            //cart.speedCoefficient = Mathf.Abs(1 - ((joint.position.z - _firstJoint.position.z) / (currentMaximumDelta * originMaximumDeltaMultiplier)));
            cart.speedCoefficient = _followCoefCurve.Evaluate(
                Mathf.Abs(1 - ((joint.position.z - _firstJoint.position.z) / (currentMaximumDelta * originMaximumDeltaMultiplier))));

            //Debug.Log(cart.name + " is connected to " + joint.parent.name);

            _carts.Add(cart);
            //_coneManager.AddPlayersIceCreams(cart.gameObject);
            //Recalculate();
            StartCoroutine(BumpingChain());
        }
    }

    IEnumerator BumpingChain()
    {
        for (int i = _carts.Count - 1; i >= 0; i--)
        {
            //_carts[i].GetComponent<ConeBumping>().Bump();

            yield return new WaitForSeconds(_bumpingDelay);
        }
    }


    public void BreakChain(GameObject target)
    {
        bool isFound = false;

        for (int i = 0; i < _carts.Count; i++)
        {
            if (isFound == true)
            {
                StartCoroutine(PushOutIceCream(_carts[i].transform));
            }

            if (_carts[i].gameObject == target)
            {
                isFound = true;
            }

            if (isFound == true)
            {
                _carts[i].followingJoint = null;
                _carts[i].isFollowing = false;
                _carts.Remove(_carts[i]);
                if (i != _carts.Count) i--;
            }
        }



        //CheckOnLose();
        //Recalculate();

        //Debug.Log("------------------------------------------");
    }

    public void ClearMissing(Cart cart)
    {
        if (_carts.Contains(cart)) _carts.Remove(cart);
    }

    IEnumerator PushOutIceCream(Transform trans)
    {
        Cart cart = trans.GetComponent<Cart>();

        var x = _playerMove.GetOffset();

        var z = Random.Range(trans.position.z + x * 3, trans.position.z + x * 6);

        x = Random.Range(-x, x);

        Vector3 destPos = new Vector3(x, trans.position.y, z);

        Vector3 startPos = trans.position;

        var time = 0f;

        while (time < _pushOutDuration)
        {
            if (cart.isDrowning == true || trans == null) break;

            time += Time.deltaTime;

            trans.position = Vector3.Lerp(startPos, destPos, _pushOutCurve.Evaluate(time / _pushOutDuration));

            yield return null;
        }
    }

    public void SetDistanceMultiplier(float multiplier)
    {
        originMaximumDeltaMultiplier = multiplier;
        ChangeCoef();
    }

    public float GetDistanceMultiplier()
    {
        return originMaximumDeltaMultiplier;
    }

    public void ChangeCoef()
    {
        if (_carts.Count > 0)
        {
            if (originMaximumDelta < _carts[_carts.Count - 1].myJoint.position.z - _firstJoint.position.z)
            {
                currentMaximumDelta = _carts[_carts.Count - 1].myJoint.position.z - _firstJoint.position.z;
            }
            else
            {
                currentMaximumDelta = originMaximumDelta;
            }


            foreach (var ice in _carts)
            {
                if (_carts.Count > 0) break;

                Cart cart = ice.GetComponent<Cart>();
                cart.speedCoefficient = _followCoefCurve.Evaluate(
                Mathf.Abs(1 - ((cart.followingJoint.position.z - _firstJoint.position.z) / (currentMaximumDelta * originMaximumDeltaMultiplier))));
            }
        }
    }

    //public void Recalculate()
    //{
    //    //ClearMissing();
    //    ChangeCoef();
    //    // Делаем подсчёт  всех объектов с тегами и отправляем в MoneyCounter
    //    int addings = 0;
    //    int iceBalls = 0;
    //    int iceCrams = 0;

    //    foreach (var cart in _carts)
    //    {
    //        var buf = cart.GetBalls();
    //        addings += buf[0];
    //        iceBalls += buf[1];
    //        iceCrams++;
    //    }

    //    int[] addingsAndBallsAndCones = new int[] { addings, iceBalls, iceCrams };

    //    _moneyCounter.CalculateLocalMoney(addingsAndBallsAndCones);
    //}

    //public void CheckOnLose()
    //{
    //    //Debug.Log(_coneManager.GetPlayersIceCreamsCount());

    //    if (_coneManager.GetPlayersIceCreamsCount() == 0)
    //    {
    //        _playerController.isRunning = false;

    //        LevelManager.Instance.Finish(false);

    //        Destroy(gameObject);
    //    }
    //}
}

