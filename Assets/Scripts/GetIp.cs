using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class GetIp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    // Obtém o nome do host da máquina local
        string hostName = Dns.GetHostName();
        // Obtém todos os endereços IP associados ao host
        IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);

        // Exibe o primeiro endereço IP associado à máquina local
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
