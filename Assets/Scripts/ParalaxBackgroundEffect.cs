using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackgroundEffect : MonoBehaviour
{
    [SerializeField]
    BackgroundLevel _farBackgroundLevel;
    [SerializeField]
    BackgroundLevel _nearBackgroundLevel;

    [SerializeField]
    float _backgroundWidth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the far background position is still has room to move, keep moving to the left.
        if (_farBackgroundLevel.ParentTransform.position.x > 13.5f * -1)
            _farBackgroundLevel.ParentTransform.position += -transform.right * _farBackgroundLevel.MoveSpeed * Time.deltaTime;
        // Else, if the background is too far, reset it to the right...
        else
            _farBackgroundLevel.ParentTransform.position = new Vector3(13.5f, _farBackgroundLevel.ParentTransform.position.y, _farBackgroundLevel.ParentTransform.position.z);
        // If the far background position is still has room to move, keep moving to the left.
        if (_nearBackgroundLevel.ParentTransform.position.x > 13.5f * -1)
            _nearBackgroundLevel.ParentTransform.position += -transform.right * _nearBackgroundLevel.MoveSpeed * Time.deltaTime;
        // Else, if the background is too far, reset it to the right...
        else
            _nearBackgroundLevel.ParentTransform.position = new Vector3(13.5f, _nearBackgroundLevel.ParentTransform.position.y, _nearBackgroundLevel.ParentTransform.position.z);
    }

    [System.Serializable]
    struct BackgroundLevel
    {
        public Transform ParentTransform;
        public float MoveSpeed;
    }
}

