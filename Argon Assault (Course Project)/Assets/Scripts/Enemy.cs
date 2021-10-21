using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int scoreToIncrease = 50;
    [SerializeField] int hitPoints = 2;

    ScoreBoard scoreBoard;

    void Start(){
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other) {
        if (hitPoints <= 0){
            Destroy(gameObject);
            return;
        }

        scoreBoard.IncreaseScore(scoreToIncrease);
        hitPoints--;
    }
}
