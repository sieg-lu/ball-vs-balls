using UnityEngine;
using System.Collections;

public class TestSceneManager : MonoBehaviour
{
    // private variables
    private Queue m_detonate_queue;

    private void test_scene_detonate_boxes()
    {
        if (m_detonate_queue.Count != 0)
        {
            GameObject current_object = (GameObject)m_detonate_queue.Dequeue();
            Detonator d_temp = (Detonator)detonator.GetComponent("Detonator");
            Vector3 hitPoint = current_object.transform.position;
            GameObject exp = (GameObject)Instantiate(detonator, hitPoint, Quaternion.identity);

            d_temp = (Detonator)exp.GetComponent("Detonator");
            d_temp.detail = 0.5f;

            Destroy(current_object, 0.1f);
            Destroy(exp, 10.0f);
        }
    }

    // public variables
    public GameObject box_1;
    public GameObject box_2;
    public GameObject box_3;
    public GameObject box_4;

    public GameObject detonator;

    void Start()
    {
        m_detonate_queue = new Queue();

        m_detonate_queue.Enqueue(box_1);
        m_detonate_queue.Enqueue(box_2);
        m_detonate_queue.Enqueue(box_3);
        m_detonate_queue.Enqueue(box_4);

        InvokeRepeating("test_scene_detonate_boxes", 2.0f, 0.5f);
    }
	
	void Update()
    {
	    
	}
}
