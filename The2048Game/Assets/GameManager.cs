using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject board, block;
    public Transform[,] position = new Transform[4, 4];
    public Text scoreboard, gover;
    GameObject[,] blocks = new GameObject[4, 4];
    int[,] nums = new int[4, 4];
    bool[] gocheck = new bool[4];
    int score, max;
    // Start is called before the first frame update
    void Start()
    {
        score = max = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                position[i, j] = board.transform.GetChild(i * 4 + j);
                blocks[i, j] = Instantiate(block, board.transform);
                blocks[i, j].transform.position = position[i, j].position;
                blocks[i, j].name = "Blob_" + i + j;
            }
        }
        Restart();
    }

    public void Restart()
    {
        gover.gameObject.SetActive(false);
        score = 0;
        AddScore(0);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                nums[i, j] = 0;
                blocks[i, j].GetComponent<Blob>().SetValue(nums[i, j]);
            }
        }
        for (int i = 0; i < 2; i++)
        {
            int ax = Random.Range(0, 4), ay = Random.Range(0, 4);
            if (nums[ax, ay] != 0)
            {
                i--;
                continue;
            }
            nums[ax, ay] = 2;
            blocks[ax, ay].GetComponent<Blob>().SetValue(nums[ax, ay]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Moving(0);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            Moving(2);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Moving(3);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Moving(1);
    }

    void Moving(int dir)
    {
        int[,] newnums = new int[4, 4];
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                newnums[i, j] = nums[i, j];
        if (dir == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                Queue<int> q = new Queue<int>();
                for (int j = 0; j < 4; j++)
                {
                    if (newnums[i, j] != 0)
                        q.Enqueue(newnums[i, j]);
                    newnums[i, j] = 0;
                }
                int idx = 0;
                while (q.Count != 0)
                {
                    if (newnums[i, idx] == 0)
                        newnums[i, idx] = q.Dequeue();
                    else if (newnums[i, idx] == q.Peek())
                    {
                        newnums[i, idx] += q.Dequeue();
                        AddScore(newnums[i, idx]);
                        idx++;
                    }
                    else
                    {
                        idx++;
                        newnums[i, idx] = q.Dequeue();
                    }
                }
            }
        }
        else if (dir == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                Queue<int> q = new Queue<int>();
                for (int j = 3; j >= 0; j--)
                {
                    if (newnums[j, i] != 0)
                        q.Enqueue(newnums[j, i]);
                    newnums[j, i] = 0;
                }
                int idx = 3;
                while (q.Count != 0)
                {
                    if (newnums[idx, i] == 0)
                        newnums[idx, i] = q.Dequeue();
                    else if (newnums[idx, i] == q.Peek())
                    {
                        newnums[idx, i] += q.Dequeue();
                        AddScore(newnums[idx, i]);
                        idx--;
                    }
                    else
                    {
                        idx--;
                        newnums[idx, i] = q.Dequeue();
                    }
                }
            }
        }
        else if (dir == 2)
        {
            for (int i = 0; i < 4; i++)
            {
                Queue<int> q = new Queue<int>();
                for (int j = 3; j >= 0; j--)
                {
                    if (newnums[i, j] != 0)
                        q.Enqueue(newnums[i, j]);
                    newnums[i, j] = 0;
                }
                int idx = 3;
                while (q.Count != 0)
                {
                    if (newnums[i, idx] == 0)
                        newnums[i, idx] = q.Dequeue();
                    else if (newnums[i, idx] == q.Peek())
                    {
                        newnums[i, idx] += q.Dequeue();
                        AddScore(newnums[i, idx]);
                        idx--;
                    }
                    else
                    {
                        idx--;
                        newnums[i, idx] = q.Dequeue();
                    }
                }
            }
        }
        else if (dir == 3)
        {
            for (int i = 0; i < 4; i++)
            {
                Queue<int> q = new Queue<int>();
                for (int j = 0; j < 4; j++)
                {
                    if (newnums[j, i] != 0)
                        q.Enqueue(newnums[j, i]);
                    newnums[j, i] = 0;
                }
                int idx = 0;
                while (q.Count != 0)
                {
                    if (newnums[idx, i] == 0)
                        newnums[idx, i] = q.Dequeue();
                    else if (newnums[idx, i] == q.Peek())
                    {
                        newnums[idx, i] += q.Dequeue();
                        AddScore(newnums[idx, i]);
                        idx++;
                    }
                    else
                    {
                        idx++;
                        newnums[idx, i] = q.Dequeue();
                    }
                }
            }
        }
        bool diff = false;
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                if (nums[i, j] != newnums[i, j])
                    diff = true;
        if (diff)
        {
            for (int i = 0; i < 4; i++)
                gocheck[i] = false;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    nums[i, j] = newnums[i, j];
            while (true)
            {
                int ax = Random.Range(0, 4), ay = Random.Range(0, 4);
                if (nums[ax, ay] == 0)
                {
                    nums[ax, ay] = Random.Range(1, 3) * 2;
                    break;
                }
            }
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    blocks[i, j].GetComponent<Blob>().SetValue(nums[i, j]);
        }
        else
        {
            int cnt = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (nums[i, j] == 0)
                        cnt++;
            if (cnt == 0)
                gocheck[dir] = true;
            bool go = true;
            for (int i = 0; i < 4; i++)
                if (!gocheck[i])
                    go = false;
            if (go)
                gover.gameObject.SetActive(true);
        }
    }

    void AddScore(int n)
    {
        score += n;
        if (score > max)
            max = score;
        scoreboard.text = "Score:\t " + score.ToString() + "\nMax:\t " + max.ToString();
    }
}
