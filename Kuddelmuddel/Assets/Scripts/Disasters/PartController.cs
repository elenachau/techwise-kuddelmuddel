using UnityEngine;
using System.Collections;

public class PartController : MonoBehaviour
{
    public ParticleSystem acidRainSystem;
    public ParticleSystem blizzardSystem;
    public DeleteObjects randomDeletionScript;
    [SerializeField] private float chanceToStart;  // chance to start a particle system
    [SerializeField] private float disasterFailureBoost;  // increases start chance when fails to start
    [SerializeField] private float disasterCheckDelay;  // attempts to start disaster every x seconds
    [SerializeField] private float disasterDuration;

    private bool isParticleSystemActive = false;

    private void Start()
    {
        // Validate references
        if (acidRainSystem == null || blizzardSystem == null || randomDeletionScript == null)
        {
            Debug.LogError("Please assign the ParticleSystems and RandomDeletion script references.");
            return;
        }

        // Ensure both particle systems are off at start
        acidRainSystem.Stop();
        blizzardSystem.Stop();

        // Disable the random deletion script at start
        randomDeletionScript.enabled = false;

        // Start the check loop
        StartCoroutine(CheckStartDisaster());
    }

    private IEnumerator CheckStartDisaster()
    {
        while (true)
        {
            // Check every x seconds
            yield return new WaitForSeconds(disasterCheckDelay);

            // Check if a particle system is already active
            if (isParticleSystemActive)
                continue;  // Skip this iteration if true

            // Randomly decide whether to start a particle system
            float startCheck = Random.value;
            print("Attempting disaster: " + startCheck + " < " + chanceToStart);

            if (startCheck < chanceToStart)
            {
                // Decide which particle system to play
                float chooseSystem = Random.value;
                if (chooseSystem < 0.50f) // Adjust as needed
                {
                    StartParticleSystemAndDeletion(acidRainSystem);
                }
                else
                {
                    StartParticleSystemAndDeletion(blizzardSystem);
                }
            }
            else {
                // Increase disaster chance by % every successive failure
                chanceToStart += disasterFailureBoost;
            }
        }
    }

    private void StartParticleSystemAndDeletion(ParticleSystem systemToStart)
    {
        Debug.Log("Started disaster!");
        // Set the lock
        isParticleSystemActive = true;

        // Stop any previously running particle systems
        acidRainSystem.Stop();
        blizzardSystem.Stop();

        // Start the chosen particle system
        systemToStart.Play();

        // Activate the random deletion process
        randomDeletionScript.enabled = true;
        randomDeletionScript.StartCoroutine(randomDeletionScript.StartWeedDestruction());

        // Stop the particle system after x seconds
        StartCoroutine(StopParticleSystemAfterDelay(systemToStart, disasterDuration));
    }

    private IEnumerator StopParticleSystemAfterDelay(ParticleSystem systemToStop, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Stop the particle system
        systemToStop.Stop();

        // Deactivate the random deletion process
        randomDeletionScript.enabled = false;

        // Release the lock
        isParticleSystemActive = false;

    }
}
