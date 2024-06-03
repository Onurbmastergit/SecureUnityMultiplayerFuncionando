using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using FishNet.Transporting.Tugboat;
using FishNet.Object;
using UnityEngine.UI;
using TMPro;
using FishNet.Managing;

public class GetIp : MonoBehaviour
{
    public static GetIp instaciate;
    public  Tugboat tugboat;
    private NetworkManager networkManager;
    public TextMeshProUGUI ipNumber;
    string ip ;
    public TMP_InputField ipServer;
    public GameObject painel;
    public GameObject painelGeral;
    public GameObject painelEntrar;
    public GameObject painelSala;
    public IPAddress[] ipAddresses;
    void Awake()
    {
        instaciate = this;
        string hostName = Dns.GetHostName();
        ipAddresses = Dns.GetHostAddresses(hostName);
        if(tugboat == null) return;
    }
    void Update()
    {
        tugboat = transform.parent.GetComponent<Tugboat>();
        ipNumber.text= $"IP : {tugboat._clientAddress}";
    }
    public void CretedServer()
    {
        ip = ipAddresses[3].ToString();
        ipNumber.text= $"IP : {ip}";
        InsertClientAdress(ip);
    }
    public void LoginServer()
    {
        ip = ipServer.text;
        InsertClientAdress(ip);
    }
    void InsertClientAdress(string ipServer)
    {
        tugboat = transform.parent.GetComponent<Tugboat>();
        tugboat._clientAddress = ipServer;
    }
    public void DesativarHud()
    {
        painel.SetActive(false);
        if(tugboat._clientAddress == "localhost")
        {
           painelEntrar.SetActive(true); 
        }
        if(tugboat._clientAddress != "localhost")
        {
            painelEntrar.SetActive(false);
            painelSala.SetActive(true);
        }
          
    }
    public void DesativarLogin()
    {
        Destroy(gameObject);
    }
}
