using System;

[Serializable]
public class InputVote
{
    public string VoterID;
    public ChoosableDirection? DirectionChoice;
    public float Weight;
}

public enum ChoosableDirection
{
    North,
    //Northeast,
    East,
    //Southeast,
    South,
    //Southwest,
    West,
    //Northwest
}