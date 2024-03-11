using System.Collections.Generic;
using UnityEngine;

public class AttaqueScript : MonoBehaviour
{
    // D�finir la s�quence d'attaques
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

    // Temps maximum entre chaque touche de la s�quence
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
        // V�rifier si la s�quence sp�cifique a �t� entr�e
        if (CheckSequence(techniques[indexTechniques].sequence[indexSequences].key))
        {
            // V�rifier le temps entre les presses
            if (Time.time - lastKeyPressTime <= maxTimeBetweenKeyPresses)
            {
                print(techniques[indexTechniques].sequence[indexSequences].mudra);
                PlaySoundEffect(mudraSound);

                // V�rifier si la s�quence est compl�te
                if (indexSequences == techniques[indexTechniques].sequence.Count - 1)
                {
                    // Ex�cuter la technique
                    ExecuteTechnique(techniques[indexTechniques].name);
                    PlaySoundEffect(jutsuSound);
                    // Passer � la prochaine technique
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
                print("Temps �coul�");
            }

            // Mettre � jour le temps de la derni�re pression de touche
            lastKeyPressTime = Time.time;
        }
    }

    // Fonction � appeler lorsque la s�quence d'attaque est compl�te
    void ExecuteTechnique(string techniqueName)
    {
        // Mettez ici le code d'ex�cution de la technique
        print("Technique ex�cut�e : " + techniqueName);
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
