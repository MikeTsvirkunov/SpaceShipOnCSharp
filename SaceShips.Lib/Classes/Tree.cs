abstract class Decision
{
    // Tests the given client 
    public abstract void Evaluate(Client client);
}

class DecisionResult : Decision
{
    public bool Result { get; set; }
    public override void Evaluate(Client client)
    {
        // Print the final result
        Console.WriteLine("OFFER A LOAN: {0}", Result ? "YES" : "NO");
    }
}


// Listing 8.16 Simplified implementation of Template method
class DecisionQuery : Decision
{
    public string Title { get; set; }
    public Decision Positive { get; set; }
    public Decision Negative { get; set; }
    // Primitive operation to be provided by the user
    public Func<Client, bool> Test { get; set; }

    public override void Evaluate(Client client)
    {
        // Test a client using the primitive operation
        bool res = Test(client);
        Console.WriteLine("  - {0}? {1}", Title, res ? "yes" : "no");
        // Select a branch to follow
        if (res) Positive.Evaluate(client);
        else Negative.Evaluate(client);
    }
}

  // Test a client using this tree
  //