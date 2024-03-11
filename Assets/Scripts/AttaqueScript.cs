using System.Collections.Generic;
using UnityEngine;

public class AttaqueScript : MonoBehaviour
{
    // Définir la séquence d'attaques
    public List<Technique> techniques;

    [System.Serializable]
    public class Technique
    {
        public string name;
        public List<(KeyCode key, string mudra)> sequence;
    }



    public AudioSource soundEffect;
    public AudioClip mudraSound;
    public AudioClip jutsuSound;

    // Temps maximum entre chaque touche de la séquence
    public float maxTimeBetweenKeyPresses = 5.0f;
    private int indexTechniques = 0;
    private int indexSequences = 0;
    private float lastKeyPressTime;

    private void Start()
    {
        soundEffect = GetComponent<AudioSource>();

        techniques = new List<Technique>
        {
            new Technique
            {
                name = "Boule de feu",
                sequence = new List<(KeyCode, string)>
                {
                    (KeyCode.A, "mouton"),
                    (KeyCode.Z, "singe"),
                    (KeyCode.E, "coq")
                }
            },
            new Technique
            {
                name = "Boule de glace",
                sequence = new List<(KeyCode, string)>
                {
                    (KeyCode.A, "mouton"),
                    (KeyCode.R, "serpent"),
                    (KeyCode.E, "coq")
                }
            }
            // Ajoutez d'autres techniques au besoin
        };
    }

    void Update()
    {
        // Vérifier si la séquence spécifique a été entrée
        if (CheckSequence(techniques[indexTechniques].sequence[indexSequences].key))
        {
            // Vérifier le temps entre les presses
            if (Time.time - lastKeyPressTime <= maxTimeBetweenKeyPresses)
            {
                print(techniques[indexTechniques].sequence[indexSequences].mudra);
                PlaySoundEffect(mudraSound);

                // Vérifier si la séquence est complète
                if (indexSequences == techniques[indexTechniques].sequence.Count - 1)
                {
                    // Exécuter la technique
                    ExecuteTechnique(techniques[indexTechniques].name);
                    PlaySoundEffect(jutsuSound);
                    // Passer à la prochaine technique
                    indexTechniques = (indexTechniques + 1) % techniques.Count;
                    indexSequences = 0;
                }
                else
                {
                    indexSequences++;
                }
            }
            else
            {
                indexSequences = 0;
                indexTechniques = 0;
                print("Temps écoulé");
            }

            // Mettre à jour le temps de la dernière pression de touche
            lastKeyPressTime = Time.time;
        }
    }

    // Fonction à appeler lorsque la séquence d'attaque est complète
    void ExecuteTechnique(string techniqueName)
    {
        // Mettez ici le code d'exécution de la technique
        print("Technique exécutée : " + techniqueName);
    }
    bool CheckSequence(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    // Fonction pour jouer l'effet sonore
    void PlaySoundEffect(AudioClip sound)
    {
        if (soundEffect != null)
        {
            soundEffect.clip = sound;

            soundEffect.Play();
        }
    }
}
