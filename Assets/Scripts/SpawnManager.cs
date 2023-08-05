using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Horse_Coeur;
    public GameObject Horse_Pique;
    public GameObject Horse_Trefle;
    public GameObject Horse_Carreau;

    private List<GameObject> spawnedHorses = new List<GameObject>();

    private float xSpawnPosHorseCoeur = 7.5f;
    private float xSpawnPosHorseTrefle = 2.5f;
    private float xSpawnPosHorseCarreau = -2.5f;
    private float xSpawnPosHorsePique = -7.5f;
    private float zMoveDistance = -4.0f;
    private float zSpawnPos = 20.0f;
    private float zGameOverPos = -20.0f;

    private float moveSpeed = 5.0f;

    private bool isGameRunning = true;

    private void Start()
    {
        Vector3 spawnPosHorseCoeur = new Vector3(xSpawnPosHorseCoeur, 1, zSpawnPos);
        Vector3 spawnPosHorseTrefle = new Vector3(xSpawnPosHorseTrefle, 1, zSpawnPos);
        Vector3 spawnPosHorseCarreau = new Vector3(xSpawnPosHorseCarreau, 1, zSpawnPos);
        Vector3 spawnPosHorsePique = new Vector3(xSpawnPosHorsePique, 1, zSpawnPos);

        GameObject horseCoeur = Instantiate(Horse_Coeur, spawnPosHorseCoeur, Horse_Coeur.transform.rotation);
        spawnedHorses.Add(horseCoeur);

        GameObject horseTrefle = Instantiate(Horse_Trefle, spawnPosHorseTrefle, Horse_Trefle.transform.rotation);
        spawnedHorses.Add(horseTrefle);

        GameObject horseCarreau = Instantiate(Horse_Carreau, spawnPosHorseCarreau, Horse_Carreau.transform.rotation);
        spawnedHorses.Add(horseCarreau);

        GameObject horsePique = Instantiate(Horse_Pique, spawnPosHorsePique, Horse_Pique.transform.rotation);
        spawnedHorses.Add(horsePique);

        StartCoroutine(MoveHorses());
    }

    private IEnumerator MoveHorses()
    {
        while (isGameRunning)
        {
            // S�lectionner un cheval al�atoire
            int horseIndex = Random.Range(0, spawnedHorses.Count);
            GameObject selectedHorse = spawnedHorses[horseIndex];
            Vector3 startPosition = selectedHorse.transform.position;
            Vector3 targetPosition = startPosition + new Vector3(0, 0, zMoveDistance);

            // Calculer la dur�e du mouvement en fonction de la distance � parcourir et de la vitesse de d�placement
            float moveDuration = -zMoveDistance / moveSpeed;

            // D�placer le cheval lin�airement vers la position cible
            float elapsedTime = 0.0f;
            while (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;
                selectedHorse.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Mettre � jour la position finale du cheval pour assurer le mouvement pr�cis de 2 unit�s
            selectedHorse.transform.position = targetPosition;

            // Afficher l'action � l'�cran
            Debug.Log(selectedHorse.name + " moves forward.");

            // V�rifier si le cheval atteint la position de fin du jeu
            if (selectedHorse.transform.position.z <= zGameOverPos)
            {
                EndGame();
                yield break;
            }

            yield return new WaitForSeconds(0f);
        }
    }

    // Fonction pour g�rer l'action du cheval lorsqu'un obstacle est touch�
    // Vous pouvez l'impl�menter en fonction de vos besoins

    // Autres fonctions et logique pour g�rer l'apparition des objets

    // Fonction pour arr�ter le jeu
    public void EndGame()
    {
        isGameRunning = false;
        Debug.Log("Game Over !");
    }
}

