using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
   public int vidaTotal = 100;
    public int vidaAtual;
    public float vibrationDuration = 0.5f; // Duração da vibração
    public ParticleSystem particle;
    public Image BarLifeStatus;
    public Image veias;
    public Image blood;
    public DamageScript damageScript;
    public float fillAmount;
    public VfxColor color;
    
    private bool isVibrating = false; // Para controlar o pulso da vibração

    void Start()
    {
        vidaAtual = vidaTotal;
    }

    void Update()
    {
        fillAmount = (float)vidaAtual / vidaTotal;
        if (BarLifeStatus == null) return;
        BarLifeStatus.fillAmount = fillAmount;
        Infection();
        Respawn();
    }

    public void ReceberDano(int valor, NetworkConnection connection)
    {
        Infection();
        damageScript.SpawnRandomBite();
        particle.Play();
        vidaAtual -= valor;
        color.ChangeColor();
        VerificarMorte();

        // Inicia a vibração se não estiver vibrando
        if (!isVibrating)
        {
            StartCoroutine(VibrateController());
        }
        StartCoroutine(VibrateController2(vibrationDuration));
    }

    void VerificarMorte()
    {
        if (vidaAtual <= 0)
        {
            transform.GetComponent<PlayerMoves>().enabled = false;
            transform.GetComponent<PlayerAttacks>().enabled = false;
            transform.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void Respawn()
    {
        if (LevelManager.instance.currentHour.Value == 7)
        {
            vidaAtual = vidaTotal;
            color.ChangeColor();

            transform.GetComponent<PlayerMoves>().enabled = true;
            transform.GetComponent<PlayerAttacks>().enabled = true;
            transform.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void Infection()
    {
        float transparencia = fillAmount;
        Color cor = veias.color;
        Color corz = blood.color;
        cor.a = 1f - transparencia;
        corz.a = 1f - transparencia;
        veias.color = cor;
        blood.color = corz;
    }

   private IEnumerator VibrateController()
    {
        isVibrating = true; // Marca que estamos vibrando

        float basePulseDuration = 0.3f; // Duração de cada batimento
        float pulseSpeedFactor = 0.6f;   // Fator para ajustar a velocidade do pulso

        while (vidaAtual > 0)
        {
            // Intensidade de vibração baseada na transparência
            float intensity = Mathf.Lerp(1f, 0f, fillAmount); // Intensity varies from max when alive to min when dead
            
            // Ajusta a duração do pulso com base na vida do jogador
            float pulseDuration = basePulseDuration * (1f - fillAmount) * pulseSpeedFactor; // Duração do pulso aumenta conforme a vida diminui
            
            // Aplica a vibração
            Gamepad.current.SetMotorSpeeds(intensity * 0.9f, intensity * 0.8f);

            yield return new WaitForSeconds(pulseDuration);

            // Para a vibração
            Gamepad.current.SetMotorSpeeds(0f, 0f);
            yield return new WaitForSeconds(pulseDuration);
        }

        // Para a vibração ao final
        Gamepad.current.SetMotorSpeeds(0f, 0f);
        isVibrating = false; // Marca que não estamos mais vibrando
    }
     private IEnumerator VibrateController2(float duration)
    {
        // Define a intensidade da vibração
        Gamepad.current.SetMotorSpeeds(0.5f, 0.8f); // Baixa e alta frequência
        
        // Espera pela duração especificada
        yield return new WaitForSeconds(duration);
        
        // Para a vibração após a duração
        Gamepad.current.SetMotorSpeeds(0f, 0f);
    }
}
