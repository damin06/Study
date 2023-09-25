using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{
    private int sum = 0;
    
    private void Start()
    {
        
        if (Thread.CurrentThread.Name == null)
            Thread.CurrentThread.Name = "MainThread";
        Debug.Log(Thread.CurrentThread.Name);

        MyJob();

        for(int i = 0; i < 10000000000; i++)
        {
            sum--;
        }
        Debug.Log("Job complete : main");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(sum);
        }
    }

    private async Task MyJob()
    {
        for(int i = 0; i < 10000000000; i++)
        {
            sum++;
        }
        Debug.Log("Job complete : task");
    }

}
