class Program
{
    /// <summary>
    /// Main entrypoint
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        List<int> billDenominations = new List<int>() { 10, 50, 100 };

        Console.Write("Enter the withdrawal amount: ");

        // Read the user input as a string
        string userInput = Console.ReadLine();

        // Parse the string to an integer
        if (int.TryParse(userInput, out int withdrawalAmount) && withdrawalAmount % 10 == 0)
        {
            List<List<int>> withdrawalCombinations = CalculateWithdrawalCombinations(billDenominations, withdrawalAmount);

            Console.WriteLine($"For €{withdrawalAmount:N2} the available payout denominations would be:");
            Console.WriteLine();
            foreach (var combination in withdrawalCombinations)
            {
                //Group the results to print in a single line
                var output = combination.GroupBy(n => n)
                    .Select(group => $"{group.Count()} x €{group.Key:N2}");

                // Print the results in a single line as requested
                Console.WriteLine("· " + string.Join(" + ", output));
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number and ensure it is a multiple of 10.");
        }

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }

    /// <summary>
    /// Calculates the combinations of bills for a given withdrawal amount.
    /// </summary>
    /// <param name="billDenominations">The available bill denominations</param>
    /// <param name="withdrawalAmount">The desired withdrawal amount</param>
    /// <returns>A list of possible combinations of bills</returns>
    static List<List<int>> CalculateWithdrawalCombinations(List<int> billDenominations, int withdrawalAmount)
    {
        List<List<int>> result = new List<List<int>>();
        ExploreWithdrawalCombinations(new List<int>(), billDenominations, 0, 0, withdrawalAmount, result);
        return result;
    }

    /// <summary>
    /// Recursively explores possible combinations of bills for a given withdrawal amount.
    /// </summary>
    /// <param name="currentCombination">The current combination being explored</param>
    /// <param name="billDenominations">The available bill denominations</param>
    /// <param name="currentDenomination">The current bill denomination being considered</param>
    /// <param name="currentSum">The current sum of bill denominations in the combination</param>
    /// <param name="targetSum">The target sum (withdrawal amount)</param>
    /// <param name="result">The list to store valid combinations</param>
    static void ExploreWithdrawalCombinations(List<int> currentCombination, List<int> billDenominations, int currentDenomination, int currentSum, int targetSum, List<List<int>> result)
    {
        // Check if we have reached the target sum
        if (currentSum == targetSum)
        {
            // If the current sum matches the target sum, add the current combination to the result list
            result.Add(currentCombination.ToList());
            return; // Exit the current exploration branch
        }

        // Check if the current sum exceeds the target sum
        if (currentSum > targetSum)
        {
            return; // Exit the current exploration branch as the sum is already greater than the target
        }

        // Explore combinations with different bill denominations
        foreach (int denomination in billDenominations.Where(denom => denom >= currentDenomination))
        {
            // Create a copy of the current combination and add the current denomination to it
            List<int> updatedCombination = currentCombination.ToList();
            updatedCombination.Add(denomination);

            // Recursively explore combinations with the updated combination
            ExploreWithdrawalCombinations(updatedCombination, billDenominations, denomination, currentSum + denomination, targetSum, result);
        }
    }
}