using System;
using UnityEngine;

public class LocalPlayer : MonoBehaviour
{
    public InputVoteCollector InputVoteCollector;
    private InputVote vote = new InputVote
    {
        VoterID = Guid.NewGuid().ToString(),
        DirectionChoice = null,
        Weight = 1f
    };

    private void Update()
    {
        var input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        
        if (Mathf.Approximately(input.magnitude, 0f))
        {
            vote.DirectionChoice = null;
        }
        else
        {
            ChoosableDirection choice = default;
            var smallestAngle = 360f;
            foreach(var direction in InputVoteCollector.ChoiceUnitVectors.Keys)
            {
                var directionVector = InputVoteCollector.ChoiceUnitVectors[direction];
                var angleBetween = Vector2.Angle(directionVector, input);
                if (angleBetween < smallestAngle)
                {
                    choice = direction;
                    smallestAngle = angleBetween;
                }
            }
            vote.DirectionChoice = choice;
        }

        InputVoteCollector.ApplyVote(vote);
    }
}
