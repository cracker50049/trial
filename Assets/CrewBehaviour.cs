using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewBehaviour : MonoBehaviour
{
    public Transform _target;
    public Transform _player;

    public float _speed = 10.0f;

    private bool _performingCommand = false;

    private void Update()
    {
        //Search and Destroy
        if (Input.GetKey(KeyCode.F1) && !_performingCommand)
        {
            _performingCommand = true;
            this.StartCoroutine(Move(_target.position, _speed * Time.deltaTime, 5f));
        }

        //Form Up & Recall (does the same... only considering the position)
        if ((Input.GetKey(KeyCode.F2) || Input.GetKey(KeyCode.F4)) && !_performingCommand)
        {
            _performingCommand = true;
            this.StartCoroutine(Move(_player.position, _speed * Time.deltaTime, 2f));
        }

        //Secure Position
        if (Input.GetKey(KeyCode.F3) && !_performingCommand)
        {
            _performingCommand = true;
            this.StartCoroutine(Move(_target.position, _speed * Time.deltaTime, 1f));
        }
    }

    IEnumerator Move(Vector3 targetPos, float delta, float offset)
    {
        float distance = (transform.position - targetPos).magnitude;

        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        while (distance >= offset)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, delta);
            yield return wait;

            //check condition
            distance = (transform.position - targetPos).magnitude;
        }

        //make next command possible after movement is done
        _performingCommand = false;
    }
}
