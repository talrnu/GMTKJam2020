using System;
using System.Collections;
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
        //{ ChoosableDirection.Northeast, Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f) },
        { ChoosableDirection.East, Vector2.right },
        //{ ChoosableDirection.Southeast, Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f) },
        { ChoosableDirection.South, Vector2.down },
        //{ ChoosableDirection.Southwest, Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f) },
        { ChoosableDirection.West, Vector2.left },
        //{ ChoosableDirection.Northwest, Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f) }
    };

    private bool TalliesHaveChanged = false;
    private Dictionary<ChoosableDirection, float> ChoiceTallies = new Dictionary<ChoosableDirection, float>
    {
        { ChoosableDirection.North, 0f },
        //{ ChoosableDirection.Northeast, 0f },
        { ChoosableDirection.East, 0f },
        //{ ChoosableDirection.Southeast, 0f },
        { ChoosableDirection.South, 0f },
        //{ ChoosableDirection.Southwest, 0f },
        { ChoosableDirection.West, 0f },
        //{ ChoosableDirection.Northwest, 0f }
    };

    private void Start()
    {
        StartCoroutine(co_CheckTallies());
    }

    //private void Update()
    //{
    //    var inputDirection = CalculateVotedInput_Discrete();
    //    MazeController.SetTilt(inputDirection);
    //}

    public void ApplyVote(InputVote vote)
    {
        var existingVote = Votes.FirstOrDefault(v => v.VoterID == vote.VoterID);

        if (existingVote == null)
        {
            var choice = (vote.DirectionChoice.HasValue) ? Enum.GetName(typeof(ChoosableDirection), vote.DirectionChoice) : "Nothing";
            Debug.Log($"New voter registered: {vote.VoterID} with weight {vote.Weight} chose {choice}");
            //Copying the new vote so it can't be modified by reference
            existingVote = new InputVote
            {
                VoterID = vote.VoterID,
                DirectionChoice = vote.DirectionChoice,
                Weight = vote.Weight
            };
            Votes.Add(existingVote);
        }
        else if (existingVote.DirectionChoice == vote.DirectionChoice)
        {
            //Don't waste time if the vote hasn't actually changed
            return;
        }
        else
        {
            var oldChoice = (existingVote.DirectionChoice.HasValue) ? Enum.GetName(typeof(ChoosableDirection), existingVote.DirectionChoice) : "Nothing";
            var newChoice = (vote.DirectionChoice.HasValue) ? Enum.GetName(typeof(ChoosableDirection), vote.DirectionChoice) : "Nothing";
            Debug.Log($"Updating vote {existingVote.VoterID} with weight {existingVote.Weight} from {oldChoice} to {newChoice}");

            if (existingVote.DirectionChoice.HasValue) ChoiceTallies[existingVote.DirectionChoice.Value] -= existingVote.Weight;
            existingVote.DirectionChoice = vote.DirectionChoice;
        }

        if (existingVote.DirectionChoice.HasValue) ChoiceTallies[existingVote.DirectionChoice.Value] += existingVote.Weight;

        //Debug.Log("Tallies have changed");
        TalliesHaveChanged = true;
    }

    public void RemoveVoter(string voterID)
    {
        //Votes.RemoveAll(v => v.VoterID == voterID);
        var toRemove = Votes.Where(v => v.VoterID == voterID).ToList();
        foreach(var vote in toRemove)
        {
            if (vote.DirectionChoice.HasValue)
            {
                ChoiceTallies[vote.DirectionChoice.Value] -= vote.Weight;
            }
            Votes.Remove(vote);
        }
        if (toRemove.Any())
        {
            TalliesHaveChanged = true;
        }
    }

    private IEnumerator co_CheckTallies()
    {
        while (true)
        {
            if (TalliesHaveChanged)
            {
                TalliesHaveChanged = false;
                //var inputDirection = CalculateVotedInput_Discrete();
                var inputDirection = CalculateVotedInput_VectorSum();
                MazeController.SetTilt(inputDirection);
            }

            //Recounting votes at 10Hz:
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Vector2 CalculateVotedInput_VectorSum()
    {
        var sum = Vector2.zero;
        foreach (var direction in ChoiceTallies.Keys)
        {
            sum += ChoiceUnitVectors[direction] * ChoiceTallies[direction];
        }
        sum.Normalize();
        Debug.Log($"New voted input is {sum}");
        return sum;
    }

    private Vector2 CalculateVotedInput_Discrete()
    {
        //TallyVotes();

        //Compare tallies to select a direction
        ChoosableDirection? chosenDirection = null;
        var highestTally = 0f;
        bool tie = true;
        foreach (var direction in ChoiceTallies.Keys)
        {
            if (ChoiceTallies[direction] > highestTally)
            {
                chosenDirection = direction;
                highestTally = ChoiceTallies[direction];
                tie = false;
            }
            else if (ChoiceTallies[direction] == highestTally)
            {
                tie = true;
            }
        }

        if (!chosenDirection.HasValue || tie)
        {
            Debug.Log("Voting couldn't choose a direction");
            return Vector2.zero;
        }
        else
        {
            Debug.Log($"Voting chose a direction: {Enum.GetName(typeof(ChoosableDirection), chosenDirection.Value)}");
            return ChoiceUnitVectors[chosenDirection.Value];
        }
    }

    //private void TallyVotes()
    //{
    //    //Clear previous tallies
    //    foreach (ChoosableDirection key in Enum.GetValues(typeof(ChoosableDirection)))
    //    {
    //        ChoiceTallies[key] = 0f;
    //    }

    //    //Add up new tallies
    //    foreach (var vote in Votes)
    //    {
    //        if (!vote.DirectionChoice.HasValue) continue;
    //        ChoiceTallies[vote.DirectionChoice.Value] += vote.Weight;
    //    }
    //}
}
