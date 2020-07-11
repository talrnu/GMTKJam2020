using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class JerkSimulator : MonoBehaviour
{
    public InputVoteCollector InputVoteCollector;

    [Tooltip("How many of these jerks are there?")]
    public int JerkCount = 3;

    [Range(0f, 1f)]
    [Tooltip("How much are these jerks actually worth? (1.0 is 100%)")]
    public float JerkWeight = 0.2f;

    [Tooltip("How many seconds do these jerks take to change their vote?")]
    public float JerkInterval = 3f;
    
    private List<InputVote> _votes = new List<InputVote>();
    private List<ChoosableDirection> _directions = new List<ChoosableDirection>((IEnumerable<ChoosableDirection>)Enum.GetValues(typeof(ChoosableDirection)));

    private void Start()
    {
        Debug.Log("Some jerks showed up:");
        for(int i=0; i<JerkCount; i++)
        {
            var jerkVote = new InputVote
            {
                VoterID = Guid.NewGuid().ToString(),
                DirectionChoice = null,
                Weight = JerkWeight
            };
            Debug.Log($"\t{jerkVote.VoterID} is a jerk");
            _votes.Add(jerkVote);
        }

        StartCoroutine(co_BeAJerk());
    }

    private IEnumerator co_BeAJerk()
    {
        while(true)
        {
            Debug.Log("Those jerks are changing their minds again...");
            foreach (var vote in _votes)
            {
                int optionNumber = UnityEngine.Random.Range(0, _directions.Count + 1);
                if (optionNumber == _directions.Count)
                {
                    Debug.Log($"\tThat jerk {vote.VoterID} couldn't decide on a direction to tilt in...");
                    vote.DirectionChoice = null;
                }
                else
                {
                    Debug.Log($"\tThat jerk {vote.VoterID} is trying to tilt {Enum.GetName(typeof(ChoosableDirection), _directions[optionNumber])}!");
                    vote.DirectionChoice = _directions[optionNumber];
                }
            }

            foreach (var vote in _votes)
            {
                InputVoteCollector.ApplyVote(vote);
            }

            yield return new WaitForSeconds(JerkInterval);
        }
    }
}
