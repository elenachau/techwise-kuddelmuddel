using UnityEngine;
using System.Collections;

public class ParticleSystemController : MonoBehaviour
{
    public ParticleSystem acidRainSystem;
    public ParticleSystem blizzardSystem;
    public Delete randomDeletionScript;
    public float chanceToStart = 0.5f;  // 50% chance to start a particle system

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
        StartCoroutine(CheckEveryThreeMinutes());
    }

    private IEnumerator CheckEveryThreeMinutes()
    {
        while (true)
        {
            // Wait for 3 minutes
            yield return new WaitForSeconds(20f);

            // Check if a particle system is already active
            if (isParticleSystemActive)
                continue;  // Skip this iteration if true

            // Randomly decide whether to start a particle system
            if (Random.value < chanceToStart)
            {
                // Decide which particle system to play
                if (Random.value < 0.10f) // Adjust as needed
                {
                    StartParticleSystemAndDeletion(acidRainSystem);
                }
                else
                {
                    StartParticleSystemAndDeletion(blizzardSystem);
                }
            }
        }
    }

    private void StartParticleSystemAndDeletion(ParticleSystem systemToStart)
    {
        // Set the lock
        isParticleSystemActive = true;

        // Stop any previously running particle systems
        acidRainSystem.Stop();
        blizzardSystem.Stop();

        // Start the chosen particle system
        systemToStart.Play();

        // Activate the random deletion process
        randomDeletionScript.enabled = true;

        // Stop the particle system after 2 minutes
        StartCoroutine(StopParticleSystemAfterDelay(systemToStart, 120f));
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
