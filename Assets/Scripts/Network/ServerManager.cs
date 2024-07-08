using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using FishNet.Transporting.Tugboat;
using FishNet.Object;
using TMPro;

public class ServerManager : MonoBehaviour
{
    #region Variables

    Tugboat tugboat;
    string ip;
    public TextMeshProUGUI ipNumber;
    public TMP_InputField ipServer;
    public IPAddress[] ipAddresses;

    [Header("Panels")]
    public GameObject painel;
    public GameObject painelGeral;
    public GameObject painelEntrar;
    public GameObject painelSala;

    #endregion

    #region Initialization

    void Awake()
    {
        tugboat = transform.parent.GetComponent<Tugboat>();
        ipNumber.text = $"IP : {GetIp()}";
    }

    #endregion

    #region Funtions

    /// <summary>
    /// Repassa o Ip do usuário.
    /// </summary>
    string GetIp()
    {
        // Obtém todos os endereços IP associados a este computador
        IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());

        // Procura e retorna o primeiro endereço IPv4 encontrado
        foreach (IPAddress address in addresses)
        {
            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return address.ToString();
            }
        }

        // Retorna uma string vazia caso nenhum endereço IPv4 seja encontrado
        return string.Empty;
    }

    public void CreateServer()
    {
        ip = GetIp();
        InsertClientAdress(ip);
    }

    public void EnterServer()
    {
        ip = ipServer.text;
        InsertClientAdress(ip);
    }

    void InsertClientAdress(string ipServer)
    {
        tugboat._clientAddress = ipServer;
    }

    public void DesativarLogin()
    {
        painelGeral.SetActive(false);
    }

    #endregion
}
