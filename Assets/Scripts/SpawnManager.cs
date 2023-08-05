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
            // Sélectionner un cheval aléatoire
            int horseIndex = Random.Range(0, spawnedHorses.Count);
            GameObject selectedHorse = spawnedHorses[horseIndex];
            Vector3 startPosition = selectedHorse.transform.position;
            Vector3 targetPosition = startPosition + new Vector3(0, 0, zMoveDistance);

            // Calculer la durée du mouvement en fonction de la distance à parcourir et de la vitesse de déplacement
            float moveDuration = -zMoveDistance / moveSpeed;

            // Déplacer le cheval linéairement vers la position cible
            float elapsedTime = 0.0f;
            while (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;
                selectedHorse.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Mettre à jour la position finale du cheval pour assurer le mouvement précis de 2 unités
            selectedHorse.transform.position = targetPosition;

            // Afficher l'action à l'écran
            Debug.Log(selectedHorse.name + " moves forward.");

            // Vérifier si le cheval atteint la position de fin du jeu
            if (selectedHorse.transform.position.z <= zGameOverPos)
            {
                EndGame();
                yield break;
            }

            yield return new WaitForSeconds(0f);
        }
    }

    // Fonction pour gérer l'action du cheval lorsqu'un obstacle est touché
    // Vous pouvez l'implémenter en fonction de vos besoins

    // Autres fonctions et logique pour gérer l'apparition des objets

    // Fonction pour arrêter le jeu
    public void EndGame()
    {
        isGameRunning = false;
        Debug.Log("Game Over !");
    }
}

