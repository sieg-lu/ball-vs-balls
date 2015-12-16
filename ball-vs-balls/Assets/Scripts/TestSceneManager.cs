using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestSceneManager : MonoBehaviour
{
    // private variables
    private Queue m_detonate_queue;
    private float m_game_time;
    private List<float> m_trigger_times;
    private const float m_start_triggering_time = 2.0f;
    private const float m_end_triggering_time = 5.0f;
    private int m_current_index;

    private void detonate_box()
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

        const int length = 10;
        m_trigger_times = new List<float>();
        for (int i = 0; i < length; i++)
        {
            float current = 0.5f / Mathf.Exp((float) (i + 10) / (float) (length + 10));

            if (i != 0)
            {
                current += m_trigger_times[i - 1];
            }

            m_trigger_times.Add(current);
            Debug.Log(m_trigger_times[i]);
        }

        m_current_index = 0;
//        InvokeRepeating("test_scene_detonate_boxes", 2.0f, 0.3f);
    }
	
	void Update()
    {
        m_game_time += Time.deltaTime;
        if (m_current_index < m_trigger_times.Count && m_game_time > m_trigger_times[m_current_index])
        {
            detonate_box();
            m_current_index++;
        }
    }
}
