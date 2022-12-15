using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectY;
using ScriptableObjectEvents;

public class WhackAMoleMover : Mover
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] Vector3 upPosition; 
    [SerializeField] Vector3 downPosition;
    [SerializeField] bool shouldBeUp = false;
    [SerializeField] FloatVariable _time;
    private WaitForFixedUpdate _wait = new();
    public Rigidbody body;
    public BaseScore score;
    public TargetEvent target;
    private Vector3 UpPosition => upPosition + downPosition;

    private void Start()
    {
        downPosition = transform.position;
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float speed = 0;
        speed = MathHelper.Map( speed ,0f, _time.Value, _speed, _maxSpeed); 
        body.velocity = Vector3.zero;
        if (shouldBeUp == false)
        {
            if(Vector3.Distance(transform.position, UpPosition) >= 0.1)
            {
                transform.position = Vector3.MoveTowards(transform.position, UpPosition, Time.deltaTime * speed);
            }
            if (Vector3.Distance(transform.position, downPosition) <= 0.1)
            {
                shouldBeUp = true;
                score.ChangeManagerScore();
                target.Raise(this);
            }
        }
    }

    public override void Move()
    {
        shouldBeUp = false;
    }

    public override void FlipBack(float speedMultiplier = 1)
    {
        
    }

    //  O que fazer - Manager para voltar a meter as moles para cima, mudar o speed de que eles se movem com o tempo.


    //IEnumerator Move_Co(Vector3 startPos, Vector3 endPos)
    //{
    //    while(Vector3.Distance(transform.position,pos) <= 0.1)
    //    {
    //        transform.position = Vector3.Lerp(pos,);
    //        yield return _wait;
    //    }
        
    //}

}
