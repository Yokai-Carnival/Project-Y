using UnityEngine;

namespace SkeeBall
{
    public class RingScore : BaseScore
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            ChangeManagerScore();
        }
    }
}
