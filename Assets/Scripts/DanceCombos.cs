using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DanceCombos : MonoBehaviour
{
    [SerializeField]
    AudioSource audio;

    float timer = 0.0f;
    float bpm = 124;
    float beatTime;

    bool poseAllowed = true;
    bool posed = false;
    bool startedPosing = false;

    bool changed = false;
    bool resetted = false;

    int bufferSize = 4;
    List<DancePose> buffer;
    int currentIndex = 0;

    int hitCounter = 0;

    DancingArms arms;

    [SerializeField]
    CirculateParticles successCircle;
    [SerializeField]
    CirculateParticles failCircle;

    DancePose[] currentSpell;
    int currentSlot;

    DancePose[] rain = { new DancePose(270, 90), new DancePose(90, 270), new DancePose(0, 0), new DancePose(180, 180) };
    DancePose[] fire = { new DancePose(180, 180), new DancePose(90, 270), new DancePose(0, 0), new DancePose(180, 180) };
    DancePose[] wind = { new DancePose(0, 0), new DancePose(0, 0), new DancePose(0, 0), new DancePose(0, 0) };

    // Use this for initialization
    void Start()
    {
        buffer = new List<DancePose>();
        beatTime = 60 / bpm;
        InvokeRepeating("PermanentClock", beatTime / 2, beatTime+0.02f);
        arms = GetComponent<DancingArms>();
    }

    // Update is called once per frame
    void Update () 
    {
        
    }

    void PermanentClock()
    {
        if (!audio.isPlaying)
            audio.Play();
        float l, r;
        Vector2 lv, rv;
        if(arms.GetHands(out l, out r, out lv, out rv))
        {
            buffer.Add(new DancePose(l, r));
            if (buffer.Count > 4)
                buffer.RemoveAt(0);
            CheckForCombo(buffer.Count-1);
        }   
        else
        {
            buffer.Clear();
        }
    }

    void CheckForCombo(int pos)
    {
        DancePose[] current = null;
        if (rain[0].IsPoseHit(buffer[0].left, buffer[0].right))
        {
            Debug.Log("rain");
            current = rain;
        }
        else if (fire[0].IsPoseHit(buffer[0].left, buffer[0].right))
        {
            Debug.Log("fire");
            current = fire;
        }
        else if (wind[0].IsPoseHit(buffer[0].left, buffer[0].right))
        {
            Debug.Log("wind");
            current = wind;
        }

        //if the first pose is part of a dance check the other poses
        if (current != null)
        {
            if (pos > 0 && !current[pos].IsPoseHit(buffer[pos].left, buffer[pos].right, true))
            {
                ReorderBuffer(pos);
                return;
            }
            
            hitCounter = pos;
            successCircle.RingEffect();
            if (pos == current.Length-1)
                Debug.Log("finished a combo");
        }
        else if(pos > 1)
        {
            ReorderBuffer(pos);
        }
    }

    void ReorderBuffer(int i)
    {
        hitCounter = 0;
        DancePose tmp = buffer[i];

        buffer.Clear();
        buffer.Add(tmp);
        CheckForCombo(0);
    }
    

    void fail()
    {
        //Debug.Log("missed the beat!");
        CancelInvoke("AllowPose");
        hitCounter = 0;
        buffer.Clear();
        Invoke("restart", beatTime);
        startedPosing = false;
        failCircle.RingEffect();
    }

    void restart()
    {
        poseAllowed = true;
    }


    bool FindSpellbeginning(float l, float r)
    {
        if (rain[0].IsPoseHit(l, r))
        {
            currentSpell = rain;
            return true;
        }
        else if(fire[0].IsPoseHit(l, r))
        {
            currentSpell = fire;
            return true;
        }
        else if(wind[0].IsPoseHit(l, r))
        {
            currentSpell = wind;
            return true;
        }
        return false;
    }

}
