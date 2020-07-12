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
    public class Direction
	{
		public ChoosableDirection direction;
		public Vector2 vector;
		public float tally;
		public string display;
		
		public Direction(ChoosableDirection _direction, Vector2 _vector, float _tally, string _display)
		{
			direction = _direction;
			vector = _vector;
			tally = _tally;
			display = _display;
		}
	}

    [HideInInspector]
    public Dictionary<ChoosableDirection, Direction> Choices = new Dictionary<ChoosableDirection, Direction>
    {
        { ChoosableDirection.North, new Direction(ChoosableDirection.North, Vector2.up, 0f, "⭡") },
        //{ ChoosableDirection.Northeast, new Direction(ChoosableDirection.Northeast, Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f), 0f, "⭧" ) },
        { ChoosableDirection.East, new Direction(ChoosableDirection.East, Vector2.right, 0f, "⭢" ) },
        //{ ChoosableDirection.Southeast, new Direction(ChoosableDirection.Southeast, Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f), 0f, "⭨" ) },
        { ChoosableDirection.South, new Direction(ChoosableDirection.South, Vector2.down, 0f, "⭣" ) },
        //{ ChoosableDirection.Southwest, new Direction(ChoosableDirection.Southwest, Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f), 0f, "⭩" ) },
        { ChoosableDirection.West, new Direction(ChoosableDirection.West, Vector2.left, 0f, "⭠" ) },
        //{ ChoosableDirection.Northwest, new Direction(ChoosableDirection.Northwest, Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f), 0f, "⭦" ) }
    };
	
    private bool TalliesHaveChanged = false;

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

            if (existingVote.DirectionChoice.HasValue) Choices[existingVote.DirectionChoice.Value].tally -= existingVote.Weight;
            existingVote.DirectionChoice = vote.DirectionChoice;
        }

        if (existingVote.DirectionChoice.HasValue) Choices[existingVote.DirectionChoice.Value].tally += existingVote.Weight;

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
                Choices[vote.DirectionChoice.Value].tally -= vote.Weight;
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
			Debug.Log(".");
            if (TalliesHaveChanged)
            {
			Debug.Log("TalliesHaveChanged");
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
        foreach (var direction in Choices.Keys)
        {
            sum += Choices[direction].vector * Choices[direction].tally;
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
        foreach (var direction in Choices.Keys)
        {
            if (Choices[direction].tally > highestTally)
            {
                chosenDirection = direction;
                highestTally = Choices[direction].tally;
                tie = false;
            }
            else if (Choices[direction].tally == highestTally)
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
            return Choices[chosenDirection.Value].vector;
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
