using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputVoteCollector : MonoBehaviour
{
    public List<InputVote> Votes;
    public MazeController MazeController;

    [HideInInspector]
    public Dictionary<ChoosableDirection, Vector2> ChoiceUnitVectors = new Dictionary<ChoosableDirection, Vector2>
    {
        { ChoosableDirection.North, Vector2.up },
        { ChoosableDirection.Northeast, Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f) },
        { ChoosableDirection.East, Vector2.right },
        { ChoosableDirection.Southeast, Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f) },
        { ChoosableDirection.South, Vector2.down },
        { ChoosableDirection.Southwest, Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f) },
        { ChoosableDirection.West, Vector2.left },
        { ChoosableDirection.Northwest, Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f) }
    };

    private Dictionary<ChoosableDirection, float> ChoiceTallies = new Dictionary<ChoosableDirection, float>
    {
        { ChoosableDirection.North, 0f },
        { ChoosableDirection.Northeast, 0f },
        { ChoosableDirection.East, 0f },
        { ChoosableDirection.Southeast, 0f },
        { ChoosableDirection.South, 0f },
        { ChoosableDirection.Southwest, 0f },
        { ChoosableDirection.West, 0f },
        { ChoosableDirection.Northwest, 0f }
    };

    public void ApplyVote(InputVote vote)
    {
        var existingVote = Votes.FirstOrDefault(v => v.VoterID == vote.VoterID);
        if (existingVote == null)
        {
            Votes.Add(vote);
        }
        else
        {
            existingVote.DirectionChoice = vote.DirectionChoice;
        }
    }

    public void RemoveVoter(string voterID)
    {
        Votes.RemoveAll(v => v.VoterID == voterID);
    }

    private void Update()
    {
        var inputDirection = CalculateVotedInput_Discrete();
        MazeController.SetTilt(inputDirection);
    }

    private Vector2 CalculateVotedInput_Discrete()
    {
        TallyVotes();

        //Compare tallies to select a direction
        ChoosableDirection? chosenDirection = null;
        var highestTally = 0f;
        foreach (var direction in ChoiceTallies.Keys)
        {
            if (ChoiceTallies[direction] > highestTally)
            {
                chosenDirection = direction;
                highestTally = ChoiceTallies[direction];
            }
        }

        if (!chosenDirection.HasValue) 
            return Vector2.zero;
        else 
            return ChoiceUnitVectors[chosenDirection.Value];
    }

    private void TallyVotes()
    {
        //Clear previous tallies
        foreach (ChoosableDirection key in Enum.GetValues(typeof(ChoosableDirection)))
        {
            ChoiceTallies[key] = 0f;
        }

        //Add up new tallies
        foreach (var vote in Votes)
        {
            if (!vote.DirectionChoice.HasValue) continue;
            ChoiceTallies[vote.DirectionChoice.Value] += vote.Weight;
        }
    }
}
